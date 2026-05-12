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
    public class CommentConfig :IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {

            {
                builder.HasKey(c => c.Id);
                builder.Property(c => c.Content).IsRequired().HasMaxLength(1000);


                builder.HasOne(c => c.Knowledge)
                      .WithMany(k => k.Comments)
                      .HasForeignKey(c => c.KnowledgeId)
                      .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(c => c.User)
                      .WithMany()
                      .HasForeignKey(c => c.UserId);
            }
        }
    }
}
