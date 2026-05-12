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
    public class ChatLogConfig :IEntityTypeConfiguration<ChatLog>
    {
        public void Configure(EntityTypeBuilder<ChatLog> builder)
        {
            
            
                builder.HasKey(e => e.Id);
                builder.Property(e => e.UserMessage).HasMaxLength(4000);
                builder.Property(e => e.BotResponse).HasMaxLength(4000);
                // MessageType (Text, Image, Audio)
                builder.Property(e => e.MessageType).HasMaxLength(20).IsRequired();

                builder.HasOne<Patient>()
                      .WithMany()
                      .HasForeignKey(e => e.PatientId)
                      .OnDelete(DeleteBehavior.NoAction); 
           
        }
    }
}
