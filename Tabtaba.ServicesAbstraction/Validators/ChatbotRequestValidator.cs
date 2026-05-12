using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.ServicesAbstraction.Commands.UserCommands;
using Tabtaba.Shared.Chatbot;

namespace Tabtaba.ServicesAbstraction.Validators
{
    public class ChatbotRequestValidator :AbstractValidator<ChatRequestDTO>
    {
        public ChatbotRequestValidator()
        {
            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("لا يمكن إرسال رسالة فارغة")
                .MaximumLength(2000).WithMessage("الرسالة طويلة جداً");

            RuleFor(x => x.MessageType)
                .NotEmpty().WithMessage("يجب تحديد نوع الرسالة");

            When(x => !string.IsNullOrEmpty(x.AttachmentData),() => {
                RuleFor(x => x.MimeType)
                    .NotEmpty().WithMessage("يجب تحديد نوع الملف المرفق (MimeType)");
            });
        }
    }
}

