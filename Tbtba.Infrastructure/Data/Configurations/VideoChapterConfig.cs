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
    public class VideoChapterConfig :IEntityTypeConfiguration<VideoChapter>
    {
        public void Configure(EntityTypeBuilder<VideoChapter> builder)
        {
           
            {
               builder.HasKey(vc => vc.Id);

               builder.Property(vc => vc.Title)
                      .IsRequired()
                      .HasMaxLength(150);

                builder.Property(vc => vc.Timestamp)
                      .IsRequired()
                      .HasMaxLength(10);

                builder.HasOne(vc => vc.Knowledge)
                      .WithMany(k => k.Chapters)
                      .HasForeignKey(vc => vc.KnowledgeId)
                      .OnDelete(DeleteBehavior.Cascade);
            };
        }
    }
}
