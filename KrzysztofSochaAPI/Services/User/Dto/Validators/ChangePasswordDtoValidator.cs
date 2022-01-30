using FluentValidation;
using KrzysztofSochaAPI.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Services.User.Dto.Validators
{
    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator()
        {
           
            RuleFor(x => x.ConfrimPassword).Equal(e => e.NewPassword)
                .WithMessage("Podane hasła różnią się od siebie"); ;
            RuleFor(x => x.NewPassword).MinimumLength(7);
            
        }
    }
}
