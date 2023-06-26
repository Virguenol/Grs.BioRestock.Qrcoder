using FluentValidation;
using Grs.BioRestock.Transfer.DataModels.Demande;
using Grs.BioRestock.Transfer.Validators.Requests.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grs.BioRestock.Transfer.Validators.Requests.Demande
{
    public class DossierRequestValidator : AbstractValidator<DemandeSignatureDto>
    {
        public DossierRequestValidator(IStringLocalizer<DossierRequestValidator> localizer)
        {
            RuleFor(request => request.Titre)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Name is required"]);
        }
    }
}
