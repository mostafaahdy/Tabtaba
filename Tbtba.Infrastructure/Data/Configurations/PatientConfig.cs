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
    public class PatientConfig : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {

            builder
            .HasOne (p => p.MedicalRecord)
            .WithOne (m => m.Patient)
            .HasForeignKey<MedicalRecord> (m => m.PatientId)
            .OnDelete (DeleteBehavior.NoAction);

            builder.HasOne (p => p.User)
             .WithOne (u => u.Patient)
             .HasForeignKey<Patient> (u => u.UserId)
             .OnDelete (DeleteBehavior.Restrict);

        }


    }
}
