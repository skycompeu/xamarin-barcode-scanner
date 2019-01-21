using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using FluentValidation;
using OmniReader.Data.Database.Model;

namespace OmniReader.Data.Database.Validator
{
    public class AppUserValidator : AbstractValidator<AppUser>
    {
        public AppUserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(255).MinimumLength(1);
            RuleFor(x => x.Surname).NotEmpty().MaximumLength(255).MinimumLength(1);
            RuleFor(x => x.Pin).NotEmpty().MaximumLength(4);
        }
    }
}