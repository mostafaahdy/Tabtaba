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
    public class MoodLogConfig :IEntityTypeConfiguration<MoodLog>
    {
        public void Configure(EntityTypeBuilder<MoodLog> builder)
        {
            builder.Property(m => m.Status)
                .HasConversion<string>();
        }
    }
}
