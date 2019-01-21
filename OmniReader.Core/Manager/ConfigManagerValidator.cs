using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniReader.Core.Manager
{
    public class ConfigManagerValidator: AbstractValidator<ConfigManager>
    {


        public ConfigManagerValidator()
        {
            RuleFor(x => x.ApiEndpoint).NotEmpty().MaximumLength(64).MinimumLength(1);
            RuleFor(x => x.ApiPassword).NotEmpty().MaximumLength(64).MinimumLength(1);

            RuleFor(x => x.DeviceId).NotEmpty().MaximumLength(64).MinimumLength(1);
            RuleFor(x => x.UniqueParcles).NotEmpty().MaximumLength(1).MinimumLength(1);

        }
    }
}
