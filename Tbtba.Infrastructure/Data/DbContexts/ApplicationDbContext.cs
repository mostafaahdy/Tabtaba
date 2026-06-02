using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Tabtaba.Domain.Entities;
using Tabtaba.Domain.Entities.BaymentgatewayEntity;
using Tabtaba.Domain.Entities.TherapistEntity;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.Persistence.Data.Configurations;

namespace Tabtba.Persistence.Data.DbContexts
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // تحديد الـ Schema الافتراضية لسوبابيز
            modelBuilder.HasDefaultSchema("public");

            // تحميل الـ Configurations التلقائية من الـ Assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppointmentConfig).Assembly);

            #region Identity Tables Mapping (Snake Case)
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<IdentityRole>().ToTable("roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("user_roles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("user_claims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("user_logins");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("user_tokens");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("role_claims");
            #endregion

            #region User Property Mapping
            modelBuilder.Entity<User>(builder =>
            {
                builder.Property(u => u.FullName).HasMaxLength(150).IsRequired();
                builder.Property(u => u.LastName).HasMaxLength(150).IsRequired();
                builder.Property(u => u.Gender).HasMaxLength(50).IsRequired();
                builder.Property(u => u.UserType).HasMaxLength(50).IsRequired();
                builder.Property(u => u.RefreshToken).HasMaxLength(500);
                builder.Property(u => u.RefreshTokenExpiryTime);
            });
            #endregion

            #region Patient Property Mapping
            modelBuilder.Entity<Patient>(builder =>
            {
                builder.ToTable("patients");
                builder.Property(p => p.DateOfBirth).IsRequired();
                builder.Property(p => p.MaritalStatus).HasMaxLength(50).IsRequired();
                builder.Property(p => p.ClientRole).HasMaxLength(50).IsRequired();
                builder.Property(p => p.MedicalHistorySummary).HasMaxLength(2000);

                // علاقة One-to-One مع الـ User
                builder.HasOne(p => p.User)
                       .WithOne(u => u.Patient)
                       .HasForeignKey<Patient>(p => p.UserId)
                       .OnDelete(DeleteBehavior.Cascade);
            });
            #endregion

            #region Therapist Property Mapping
            modelBuilder.Entity<Therapist>(builder =>
            {
                builder.ToTable("therapists");
                builder.Property(t => t.Specialization).HasMaxLength(150);
                builder.Property(t => t.Bio).HasMaxLength(1000);
                builder.Property(t => t.Username).HasMaxLength(100);

                // علاقة One-to-One مع الـ User
                builder.HasOne(t => t.User)
                       .WithOne(u => u.Therapist)
                       .HasForeignKey<Therapist>(t => t.UserId)
                       .OnDelete(DeleteBehavior.Restrict);
            });
            #endregion

            #region Global Message & Logging Compatibility
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany()
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction);

            // ضبط توافق التواريخ مع PostgreSQL (Timestamps with Timezone)
            modelBuilder.Entity<Message>()
                .Property(m => m.SentAt)
                .HasColumnType("timestamp with time zone");

            modelBuilder.Entity<AuditLog>()
                .Property(a => a.CreatedAt)
                .HasColumnType("timestamp with time zone");
            #endregion
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // الحفاظ على منطق زيادة الجلسات للمريض عند نجاح الدفع
            var successfulPayments = ChangeTracker.Entries<Payment>()
                 .Where(e => (e.State == EntityState.Added || e.State == EntityState.Modified)
                              && e.Property(p => p.Status).CurrentValue?.ToString() == "Success")
                 .Select(e => e.Entity)
                 .ToList();

            foreach (var payment in successfulPayments)
            {
                var patient = Patients.Local.FirstOrDefault(p => p.Id == payment.PatientId)
                  ?? await Patients.FindAsync(new object[] { payment.PatientId }, cancellationToken);

                if (patient != null)
                {
                    patient.RemainingSessions += 4;
                }
            }

            // منطق الـ Audit Logs
            var auditLogs = new List<AuditLog>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog || entry.State is EntityState.Detached or EntityState.Unchanged)
                    continue;

                var audit = new AuditLog
                {
                    UserId = "system",
                    EntityName = entry.Entity.GetType().Name,
                    Action = entry.State.ToString(),
                    CreatedAt = DateTimeOffset.UtcNow.DateTime,
                    NewValues = entry.State != EntityState.Deleted ? JsonSerializer.Serialize(entry.CurrentValues.ToObject()) : null,
                    OldValues = entry.State != EntityState.Added ? JsonSerializer.Serialize(entry.OriginalValues.ToObject()) : null
                };

                auditLogs.Add(audit);
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            if (auditLogs.Any())
            {
                await AuditLogs.AddRangeAsync(auditLogs, cancellationToken);
                await base.SaveChangesAsync(cancellationToken);
            }

            return result;
        }

        #region DbSets
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<CommonCondition> CommonConditions { get; set; }
        public DbSet<CommonConditions_Diagnosis> CommonConditions_Diagnoses { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<TherapistAvailability> TherapistAvailabilities { get; set; }
        public DbSet<KnowledgeLibrary> KnowledgeLibraries { get; set; }
        public DbSet<DailyMessages> DailyMessages { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<KidsZone> KidsZones { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Patient_Appointment_Doctor> Patient_Appointment_Doctors { get; set; }
        public DbSet<ProgressTracker> ProgressTrackers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<TherapistProfessionalInfo> TherapistProfessionalInfos { get; set; }
        public DbSet<Therapist> Therapists { get; set; }
        public DbSet<TherapistEducation> TherapistEducations { get; set; }
        public DbSet<TherapistDocument> TherapistDocuments { get; set; }
        public DbSet<TherapistLanguage> TherapistLanguages { get; set; }
        public DbSet<SessionNote> SessionNotes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Withdrawal> Withdrawals { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<PaymentCard> PaymentCards { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<MoodLog> MoodLogs { get; set; }
        public DbSet<UserSavedContent> UserSavedContents { get; set; }
        public DbSet<Comment> KnowledgeComments { get; set; }
        public DbSet<KnowledgeLike> KnowledgeLikes { get; set; }
        public DbSet<VideoChapter> VideoChapters { get; set; }
        public DbSet<RelaxContent> relaxContents { get; set; }
        public DbSet<RelaxLog> relaxLogs { get; set; }
        public DbSet<ChatLog> ChatLogs { get; set; }
        #endregion
    }
}