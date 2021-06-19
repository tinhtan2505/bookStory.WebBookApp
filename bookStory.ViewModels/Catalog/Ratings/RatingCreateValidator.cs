using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Ratings
{
    public class RatingCreateValidator : AbstractValidator<RatingCreateRequest>
    {
        public RatingCreateValidator()
        {
            RuleFor(x => x.IdTranslation).NotEmpty().WithMessage("IdTranslation is required");
            RuleFor(x => x.Vote).NotEmpty().WithMessage("Vote is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
        }
    }
}