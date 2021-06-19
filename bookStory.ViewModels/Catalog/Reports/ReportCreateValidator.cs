using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Reports
{
    public class CommentCreateValidator : AbstractValidator<ReportCreateRequest>
    {
        public CommentCreateValidator()
        {
            RuleFor(x => x.IdParagraph).NotEmpty().WithMessage("IdParagraph không thể bỏ trống");
            RuleFor(x => x.Reason).NotEmpty().WithMessage("Reason không thể bỏ trống");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId không thể bỏ trống");
        }
    }
}