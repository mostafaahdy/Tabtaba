using Microsoft.EntityFrameworkCore;
using Tabtaba.Entites;


namespace Tabtba.Persistence.Data.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<CommonCondition> CommonConditions { get; set; }
        public DbSet<CommonConditions_Diagnosis> CommonConditions_Diagnoses { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<DailyMessages> DailyMessages { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<KidsZone> KidsZones { get; set; }
        public DbSet<KnowledgeZone> KnowledgeZones { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Patient_Appointment_Doctor> Patient_Appointment_Doctors { get; set; }
        public DbSet<ProgressTracker> ProgressTrackers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            // Other entities (junction table)
            modelBuilder.Entity<Patient_Appointment_Doctor>()
                .HasKey(pad => new { pad.PatientId, pad.AppointmentId, pad.DoctorId });

            modelBuilder.Entity<Patient_Appointment_Doctor>()
                .HasOne(pad => pad.Patient)
                .WithMany()
                .HasForeignKey(pad => pad.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Patient_Appointment_Doctor>()
                .HasOne(pad => pad.Appointment)
                .WithMany()
                .HasForeignKey(pad => pad.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Patient_Appointment_Doctor>()
                .HasOne(pad => pad.Doctor)
                .WithMany()
                .HasForeignKey(pad => pad.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Any other one-to-one relationships also restrict deletes
            modelBuilder.Entity<Doctors>()
                .HasOne(d => d.User)
                .WithOne(u => u.Doctor)
                .HasForeignKey<User>(u => u.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Patient>()
            .HasOne(p => p.MedicalRecord)
            .WithOne(m => m.Patient)
            .HasForeignKey<MedicalRecord>(m => m.PatientId)
            .OnDelete(DeleteBehavior.NoAction);

            // Keyless entities
            modelBuilder.Entity<Achievement>()
                .HasOne<Patient>()
                .WithMany()
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
