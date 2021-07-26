using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookStory.ViewModels.Catalog.Books
{
    public class BookCreateValidator : AbstractValidator<BookCreateRequest>
    {
        public BookCreateValidator()
        {
            RuleFor(x => x.FileName).NotEmpty().WithMessage("FileName is required");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Author).NotEmpty().WithMessage("Author is required");
        }
    }
}