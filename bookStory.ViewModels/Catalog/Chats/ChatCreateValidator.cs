using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Chats
{
    public class ChatCreateValidator : AbstractValidator<ChatCreateRequest>
    {
        public ChatCreateValidator()
        {
            RuleFor(x => x.UserIdReceiver).NotEmpty().WithMessage("UserIdReceiver is required");
            RuleFor(x => x.Message).NotEmpty().WithMessage("Message is required");
            RuleFor(x => x.UserIdSender).NotEmpty().WithMessage("UserIdSender is required");
        }
    }
}