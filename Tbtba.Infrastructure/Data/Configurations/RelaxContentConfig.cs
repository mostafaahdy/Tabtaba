using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Entities.UserEntity;

namespace Tabtaba.Persistence.Data.Configurations
{
    public class RelaxContentConfig :IEntityTypeConfiguration<RelaxContent>
    {
        public void Configure(EntityTypeBuilder<RelaxContent> builder)
        {
           builder.HasKey(e => e.Id);
           builder.Property(e => e.Title).IsRequired().HasMaxLength(200);
        }
    }
}
