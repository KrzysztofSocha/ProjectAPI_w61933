using FluentValidation;
using KrzysztofSochaAPI.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.User.Dto.Validators
{
    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleFor(x => x.ConfrimPassword).Equal(e => e.NewPassword)
                .WithMessage("Podane hasła różnią się od siebie"); ;
            RuleFor(x => x.NewPassword).MinimumLength(7);
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
