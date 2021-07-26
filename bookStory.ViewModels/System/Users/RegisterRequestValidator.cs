using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace bookStory.ViewModels.System.Users
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name không thể bỏ trống")
                .MaximumLength(200).WithMessage("First name không được quá 200 ký tự");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name không thể bỏ trống")
                .MaximumLength(200).WithMessage("Last name không được quá 200 ký tự");

            RuleFor(x => x.Dob).GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Birthday không thể lớn hơn 100 năm");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email không thể bỏ trống")
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
                .WithMessage("Email định dạng không khớp");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number không thể bỏ trống");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name không thể bỏ trống");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password không thể bỏ trống")
                .MinimumLength(6).WithMessage("Password có ít nhất 6 ký tự");

            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.Password != request.ConfirmPassword)
                {
                    context.AddFailure("Confirm password không trùng khớp");
                }
            });
        }
    }
}