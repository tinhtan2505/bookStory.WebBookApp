using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Comments
{
    public class CommentCreateValidator : AbstractValidator<CommentCreateRequest>
    {
        public CommentCreateValidator()
        {
            RuleFor(x => x.IdTranslation).NotEmpty().WithMessage("IdTranslation is required");
            RuleFor(x => x.Message).NotEmpty().WithMessage("Message is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
        }
    }
}