using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Shared.Chatbot
{
    public class OpenRouterResponse
    {
        public List<Choice> choices { get; set; } = new List<Choice>();
    }
    public class Choice
    {
        public MessageContent message { get; set; } = new MessageContent();
    }
    public class MessageContent
    {
        public string content { get; set; } = default!;
    }
}
