using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Entities.UserEntity;

namespace Tabtaba.Persistence.Data.Configurations
{
    public class RelaxLogConfig :IEntityTypeConfiguration<RelaxLog>
    {
        public void Configure(EntityTypeBuilder<RelaxLog> builder)
        {
            builder.HasKey(e => e.Id);

            // Relationship: A Log belongs to one Content
            builder .HasOne(l => l.RelaxContent)
                  .WithMany(c => c.Logs)
                  .HasForeignKey(l => l.RelaxContentId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Relationship: A Log belongs to one Patient (Identity User)
            builder.HasOne<User>()
                  .WithMany()
                  .HasForeignKey(l => l.PatientId);
        }
    }
}
