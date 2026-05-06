using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Tabtaba.Domain.Entities;
using Tabtaba.Entities;
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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppointmentConfig).Assembly);

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
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            var auditLogs = new List<AuditLog>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog ||
                    entry.State is EntityState.Detached or EntityState.Unchanged)
                    continue;

                var audit = new AuditLog
                {
                    EntityName = entry.Entity.GetType().Name,
                    Action = entry.State.ToString(),
                    CreatedAt = DateTime.UtcNow,
                    NewValues = entry.State != EntityState.Deleted
                        ? JsonSerializer.Serialize(entry.CurrentValues.ToObject())
                        : null,
                    OldValues = entry.State != EntityState.Added
                        ? JsonSerializer.Serialize(entry.OriginalValues.ToObject())
                        : null
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
        public DbSet<DailyMessages> DailyMessages { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<KidsZone> KidsZones { get; set; }
        public DbSet<KnowledgeZone> KnowledgeZones { get; set; }
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
        #endregion
    }
}