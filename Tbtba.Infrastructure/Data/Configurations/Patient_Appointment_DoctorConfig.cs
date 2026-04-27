using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Entities;

namespace Tabtaba.Persistence.Data.Configurations
{
    public class Patient_Appointment_DoctorConfig : IEntityTypeConfiguration<Patient_Appointment_Doctor>
    {
        public void Configure(EntityTypeBuilder<Patient_Appointment_Doctor> builder)
        {
            // Other entities (junction table)
            builder
                .HasKey(pad => new { pad.PatientId, pad.AppointmentId, pad.DoctorId });

            builder         
                .HasOne(pad => pad.Patient)
                .WithMany()
                .HasForeignKey(pad => pad.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(pad => pad.Appointment)
                .WithMany()
                .HasForeignKey(pad => pad.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(pad => pad.Doctor)
                .WithMany()
                .HasForeignKey(pad => pad.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
