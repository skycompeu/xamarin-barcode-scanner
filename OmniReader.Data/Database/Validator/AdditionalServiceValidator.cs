using FluentValidation;
using OmniReader.Data.Database.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniReader.Data.Database.Validator
{
    public class AdditionalServiceValidator : AbstractValidator<AdditionalService>
    {
        public AdditionalServiceValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(64).MinimumLength(1);
        }
    }
}
