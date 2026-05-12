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
    public class UserSavedContentConfig :IEntityTypeConfiguration<UserSavedContent>
    {
        public void Configure(EntityTypeBuilder<UserSavedContent> builder)
        {
             
           builder.HasKey(sc => new { sc.UserId,sc.KnowledgeLibraryId });
           builder.HasOne<User>()
           .WithMany()

           .HasForeignKey(sc => sc.UserId);
           builder.HasOne(sc => sc.KnowledgeLibrary)
           .WithMany()
           .HasForeignKey(sc => sc.KnowledgeLibraryId);
        }
    }
}
