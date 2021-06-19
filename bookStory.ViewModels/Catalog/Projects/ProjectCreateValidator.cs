using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Projects
{
    public class ProjectCreateValidator : AbstractValidator<ProjectCreateRequest>
    {
        public ProjectCreateValidator()
        {
            RuleFor(x => x.IdBook).NotEmpty().WithMessage("IdBook is required");
            RuleFor(x => x.IdLanguage).NotEmpty().WithMessage("IdLanguage is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
        }
    }
}