using FluentValidation;
using Grs.BioRestock.Transfer.DataModels.Document;
using Grs.BioRestock.Transfer.Validators.Requests.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grs.BioRestock.Transfer.Validators.Requests.Demande
{
    public class FichierRequestValidator : AbstractValidator<DocumentDto>
    {
        public FichierRequestValidator(IStringLocalizer<FichierRequestValidator> localizer)
        {
            RuleFor(request => request.Designation)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Designation is required"]);

            RuleFor(request => request.NomClient)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Nom Client is required"]);
        }
    }
}
