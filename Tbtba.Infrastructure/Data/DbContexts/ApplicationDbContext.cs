using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppointmentConfig).Assembly);
        }
        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            var successfulPayments = ChangeTracker.Entries<Payment>()
             .Where(e => (e.State == EntityState.Added || e.State == EntityState.Modified)
                          && e.Property(p => p.Status).CurrentValue?.ToString() == "Success")
             .Select(e => e.Entity)
             .ToList();

            foreach( var payment in successfulPayments )
            {
                var patient = Patients.Local.FirstOrDefault(p => p.Id == payment.PatientId)
                  ?? await Patients.FindAsync(new object[] { payment.PatientId },cancellationToken);

                if( patient != null )
                {
                    patient.RemainingSessions += 4;
                }
            }
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
        public DbSet<KnowledgeLibrary> KnowledgeLibraries { get; set; }
        public DbSet<DailyMessages> DailyMessages { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
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