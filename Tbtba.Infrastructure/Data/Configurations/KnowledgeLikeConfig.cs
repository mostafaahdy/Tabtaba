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
    public class KnowledgeLikeConfig :IEntityTypeConfiguration<KnowledgeLike>
    {
        public void Configure(EntityTypeBuilder<KnowledgeLike> builder)
        {
           
              builder.HasKey(l => new { l.UserId,l.KnowledgeId });

            
              builder.HasOne(l => l.Knowledge)
              .WithMany(k => k.Likes)
              .HasForeignKey(l => l.KnowledgeId);
        }
    }
}
