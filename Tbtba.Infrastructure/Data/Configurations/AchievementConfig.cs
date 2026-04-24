using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Entites;

namespace Tabtaba.Persistence.Data.Configurations
{
    public class AchievementConfig : IEntityTypeConfiguration<Achievement>
    {
        // Keyless entities
        public void Configure(EntityTypeBuilder<Achievement> builder)
        {
            builder
               .HasOne<Patient>()
               .WithMany()
               .HasForeignKey (a => a.PatientId)
               .OnDelete (DeleteBehavior.NoAction);
        }
    }
}
