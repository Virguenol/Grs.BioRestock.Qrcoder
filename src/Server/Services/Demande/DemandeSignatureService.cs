using Grs.BioRestock.Application.Interfaces.Services;
using Grs.BioRestock.Infrastructure.Contexts;
using Grs.BioRestock.Shared.Enums.Demande;
using Grs.BioRestock.Shared.Wrapper;
using Grs.BioRestock.Transfer.DataModels.Demande;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grs.BioRestock.Server.Services.Demande
{
    public interface IDemandeSignatureService
    {
        Task<Result<List<DemandeSignatureDto>>> GetDemandeSignature();
        Task<Result<string>> AddDemandeSignature(DemandeSignatureDto request);
        Task<Result<string>> DeleteDemandeSignature(int id);
    }
    public class DemandeSignatureService : IDemandeSignatureService
    {
        private readonly IStringLocalizer<DemandeSignatureService> _localizer;
        private readonly ICurrentUserService _currentUserService;
        private readonly UniContext _context;
        public DemandeSignatureService(IStringLocalizer<DemandeSignatureService> localizer,
            ICurrentUserService currentUserService,
            UniContext context)
        {
            _localizer = localizer;
            _currentUserService = currentUserService;
            _context = context;
        }

        public async Task<Result<string>> AddDemandeSignature(DemandeSignatureDto request)
        {
            if (request.Id == 0)
            {
                var demande = request.Adapt<Domain.Entities.DemandeSignature>();
                demande.CreatedOn = DateTime.Now;
                await _context.DemandeSignatures.AddAsync(demande);
                await _context.SaveChangesAsync();
                return await Result<string>.SuccessAsync("la demande a été céer");
            }
            else
            {
                var existingdemande =
                    await _context.DemandeSignatures.SingleOrDefaultAsync(x => x.Id == request.Id);
                if (existingdemande == null)
                {
                    return await Result<string>.SuccessAsync("la demande n'existe pas");
                }
                else
                {
                    existingdemande.Titre = request.Titre;
                    existingdemande.LastModifiedOn = DateTime.Now;
                    _context.DemandeSignatures.Update(existingdemande);
                    await _context.SaveChangesAsync();
                    return await Result<string>.SuccessAsync("la demande modifier");
                }
            }
        }

        public async Task<Result<string>> DeleteDemandeSignature(int id)
        {
            var existingDemande = await _context.DemandeSignatures.FirstOrDefaultAsync(x => x.Id == id);
            if (existingDemande != null)
            {
                _context.DemandeSignatures.Remove(existingDemande);
                await _context.SaveChangesAsync();
                return await Result<string>.SuccessAsync("la demande a été supprimer");
            }
            else
            {
                return await Result<string>.FailAsync("la demande n'existe pas.");
            }
        }

        public async Task<Result<List<DemandeSignatureDto>>> GetDemandeSignature()
        {
            var demande = await _context.DemandeSignatures.OrderByDescending(x => x.Id).ToListAsync();
            var demandeResponse = demande.Adapt<List<DemandeSignatureDto>>();
            return await Result<List<DemandeSignatureDto>>.SuccessAsync(demandeResponse);
        }

    }
}
