using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using InGame.Business.Concrete.DTO.Concrete.User;

namespace InGame.Business.Tools.Validations
{
    public class UserLoginValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginValidator()
        {
            RuleFor(m => m.Email).NotEmpty().EmailAddress().MaximumLength(50).WithMessage("email is null or max length");
            RuleFor(m => m.Password).NotEmpty().MinimumLength(5).MaximumLength(50)
                .WithMessage("password is null or max length");
        }
    }
}
