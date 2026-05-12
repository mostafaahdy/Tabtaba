using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Chatbot
{
    public class ChatRequestDTO
    {
        public string? Message { get; set; }
        public string? AttachmentData { get; set; }
        public string? MimeType { get; set; }
        public string MessageType { get; set; } = "Text";
    }
}
