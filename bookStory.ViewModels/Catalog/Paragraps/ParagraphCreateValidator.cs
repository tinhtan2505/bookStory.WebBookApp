using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Paragraps
{
    public class ParagraphCreateValidator : AbstractValidator<ParagraphCreateRequest>
    {
        public ParagraphCreateValidator()
        {
            RuleFor(x => x.IdBook).NotEmpty().WithMessage("IdBook is required");
            RuleFor(x => x.Order).NotEmpty().WithMessage("Order is required");
            RuleFor(x => x.Type).NotEmpty().WithMessage("Type is required");
        }
    }
}