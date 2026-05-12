using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Entities.TherapistEntity;
using Tabtaba.Domain.Entities.UserEntity;

namespace Tabtaba.Persistence.Data.Configurations
{
    public class DoctorConfig : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            // Any other one-to-one relationships also restrict deletes
           builder
                .HasOne(d => d.User)
                .WithOne(u => u.Doctor)
                .HasForeignKey<User>(u => u.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasQueryFilter(d => d.IsActive);
        }
    }
}
