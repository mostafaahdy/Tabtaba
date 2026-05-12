using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities.BaymentgatewayEntity;
using Tabtaba.Domain.Entities.Settings;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.Presentation.Controllers;
using Tabtaba.ServicesAbstraction;
using Tabtaba.Shared.Chatbot;

namespace Tabtaba.Services.Services
{
    public class ChatbotService :IChatbotService
    {
        private readonly HttpClient _httpClient;
        private readonly DeepSeekSettings _settings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatbotService(
            HttpClient httpClient,
            IOptions<DeepSeekSettings> settings,
            IUnitOfWork unitOfWork,
            IHubContext<ChatHub> hubContext)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }

        public async Task<string> ProcessUserMessageAsync(int patientId,ChatRequestDTO request)
        {

            string? savedImagePath = null;
            string? savedAudioPath = null;

            if( !string.IsNullOrEmpty(request.AttachmentData) )
            {
                var filePath = await SaveFileAsync(request.AttachmentData,request.MessageType);

                if( request.MessageType == "Image" ) savedImagePath = filePath;
                else if( request.MessageType == "Voice" ) savedAudioPath = filePath;
            }

            //HttpClient
            var url = _settings.ModelUrl;

            var payload = new
            {
                model = "inclusionai/ring-2.6-1t:free",
                messages = new[]
                {
            new { role = "system", content = @"أنت (طبطبة)، مساعد نفسي رزين ومهذب. 
                       تتحدث العامية المصرية بأسلوب هادئ وداعم. 
                       ممنوع استخدام كلمات (يا حبيبي، ). 
                         ردودك ذكية وتفاعلية، 
                        إذا اشتكى المستخدم من الزهق، تفاعل معه واقترح خدماتنا   : 
                         (حجز جلسات، تمارين تأمل، تتبع مزاج)." },
            new { role = "user", content = request.Message ?? "المستخدم أرسل مرفقاً" }

                },
                temperature = 0.7,
                max_tokens = 1000
            };

            // Headers OpenRouter
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",_settings.ApiKey);
            _httpClient.DefaultRequestHeaders.Add("HTTP-Referer","http://localhost");
            _httpClient.DefaultRequestHeaders.Add("X-Title","Tabtaba_App");

            var response = await _httpClient.PostAsJsonAsync(url,payload);

            if( response.IsSuccessStatusCode )
            {
                var result = await response.Content.ReadFromJsonAsync<OpenRouterResponse>();
                var botReply = result?.choices?.FirstOrDefault()?.message?.content ?? "سامعك كمل";

                var chatLog = new ChatLog
                {
                    PatientId = patientId,
                    UserMessage = request.Message,
                    BotResponse = botReply,
                    MessageType = request.MessageType, // "Image" "Voice"  "Text"
                    ImagePath = savedImagePath,        // Image path
                    AudioPath = savedAudioPath,        // Audio path
                    Date = DateTime.UtcNow
                };

                await _unitOfWork.GetRepository<ChatLog>().AddAsync(chatLog);

                await _unitOfWork.SaveChangesAsync();

                await _hubContext.Clients.User(patientId.ToString()).SendAsync("ReceiveMessage",botReply);

                var appointments = await _unitOfWork.GetRepository<Appointment>().GetAllAsync();
                var session = appointments.FirstOrDefault(s => s.PatientId == patientId
                                            && s.Status.ToString() == "Completed"
                                            && s.Date_Time.Date == DateTime.UtcNow.Date);

                if( session != null )
                {
                    await _hubContext.Clients.User(session.DoctorId.ToString())
                        .SendAsync("UpdatePatientChat",new
                        {
                            PatientId = patientId,
                            Message = request.Message,
                            Response = botReply,
                            Time = DateTime.UtcNow
                        });
                }
                return botReply;
            }

            else
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                return "أنا هنا معاك حابب تحكي لي أكتر؟";
            }
        }

        #region Helper Method
        private async Task<string> SaveFileAsync(string base64Data,string Type)
        {
            var folderName = Type == "Image" ? "Images" : "Audio";
            var extension = Type == "Image" ? ".jpg" : ".wav"; // MimeType

            var fileName = $"{Guid.NewGuid()}{extension}";
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","uploads",folderName);

            if( !Directory.Exists(folderPath) ) Directory.CreateDirectory(folderPath);

            var fullPath = Path.Combine(folderPath,fileName);
            var bytes = Convert.FromBase64String(base64Data);
            await File.WriteAllBytesAsync(fullPath,bytes);

            return $"/uploads/{folderName}/{fileName}";
        } 
        #endregion
    }
}


