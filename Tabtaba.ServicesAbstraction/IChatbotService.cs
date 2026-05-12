using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Shared.Chatbot;

namespace Tabtaba.ServicesAbstraction
{
    public interface IChatbotService
    {
        Task<string> ProcessUserMessageAsync(int patientId,ChatRequestDTO request);
    }
}
