using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Domain.Entities.UserEntity
{
    public class ChatLog
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string? UserMessage { get; set; }
        public string? BotResponse { get; set; }
        public string? ImagePath { get; set; }
        public string? AudioPath { get; set; }
        public string MessageType { get; set; } = "Text";
        public DateTime Date { get; set; } = DateTime.UtcNow;

        // Navigation Property 
         public virtual Patient Patient { get; set; } = default!;
    }
}


