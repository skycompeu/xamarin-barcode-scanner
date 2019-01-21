using FluentValidation;
using OmniReader.Data.Database.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniReader.Data.Database.Validator
{
    public class ScanDataServiceValidator : AbstractValidator<ScanData>
    {

        public ScanDataServiceValidator()
        {
            RuleFor(x => x.IdScanner).NotEmpty();
            RuleFor(x => x.IdType).NotEmpty();
            RuleFor(x => x.IdUser).NotEmpty();
            RuleFor(x => x.ScannedAt).NotEmpty();
            RuleFor(x => x.DataValue).NotEmpty().MaximumLength(255).MinimumLength(1);
        }
    }
}
