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
    public class DoctorConfig : IEntityTypeConfiguration<Therapist>
    {
        public void Configure(EntityTypeBuilder<Therapist> builder)
        {
            builder.HasOne(d => d.User)
                .WithOne(u => u.Therapist)
                .HasForeignKey<Therapist>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // builder.HasQueryFilter(d => d.IsActive);
        }
    }
}