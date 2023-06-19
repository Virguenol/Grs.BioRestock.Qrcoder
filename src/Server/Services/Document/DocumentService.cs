using Grs.BioRestock.Application.Interfaces.Services;
using Grs.BioRestock.Domain.Entities;
using Grs.BioRestock.Infrastructure.Contexts;
using Grs.BioRestock.Server.Services.Demande;
using Grs.BioRestock.Shared.Wrapper;
using Grs.BioRestock.Transfer.DataModels.Document;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Grs.BioRestock.Server.Services.Document
{
    public interface IDocumentService
    {
        Task<Result<List<DocumentDto>>> GetListeDocument(int idDemande);
        Task<Result<DocumentDto>> GetByIdDocument(int id);
        Task<Result<string>> AddDocument(DocumentDto demandeSignature);
        Task<Result<string>> DeleteDocument(int id);
        Task<Result<string>> SignerDemande(int id);
    }
    public class DocumentService : IDocumentService
    {
        private readonly UniContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IUploadService _uploadService;
        public DocumentService(UniContext context, IUploadService uploadService, IWebHostEnvironment env,
            IConfiguration configuration, IDemandeSignatureService demandeSignatureService)
        {
            _context = context;
            _env = env;
            _configuration = configuration;
            _uploadService = uploadService;
        }

        public async Task<Result<string>> AddDocument(DocumentDto demandeSignature)
        {
            var uploadRequest = demandeSignature.UploadRequest;
            if (uploadRequest != null)
            {
                demandeSignature.FileName = uploadRequest.FileName = $"D-{Guid.NewGuid()}{uploadRequest.Extension}";
            }

            if (demandeSignature.Id == 0)
            {
                var document = demandeSignature.Adapt<Domain.Entities.DocumentSignature>();
                if (uploadRequest != null)
                {
                    demandeSignature.FileUrl = _uploadService.UploadAsync(uploadRequest);
                }
                document.IdDemandeSignature = demandeSignature.IdDemandeSignature;
                document.FileUrl = demandeSignature.FileUrl;
                document.FileName = demandeSignature?.FileName;
                document.CreatedOn = DateTime.Now;
                await _context.DocumentSignatures.AddAsync(document);
                await _context.SaveChangesAsync();
                return await Result<string>.SuccessAsync("Document Ajouter");
            }
            else
            {
                var document = await _context.DocumentSignatures.SingleOrDefaultAsync(x => x.Id == demandeSignature.Id);
                if (document == null)
                {
                    return await Result<string>.SuccessAsync("le dcument n'existe pas");
                }
                if (uploadRequest != null)
                {
                    demandeSignature.FileUrl = _uploadService.UploadAsync(uploadRequest);
                }
                document.Title = demandeSignature.Title;
                document.FileUrl = demandeSignature.FileUrl;
                document.FileName = demandeSignature?.FileName;
                document.LastModifiedOn = DateTime.Now;
                _context.DocumentSignatures.Update(document);
                await _context.SaveChangesAsync();
                return await Result<string>.SuccessAsync("le document a été modifier");
            }
        }

        public async Task<Result<string>> DeleteDocument(int id)
        {
            var existingDemande = await _context.DocumentSignatures.FirstOrDefaultAsync(x => x.Id == id);
            if (existingDemande != null)
            {
                _context.DocumentSignatures.Remove(existingDemande);
                await _context.SaveChangesAsync();
                return await Result<string>.SuccessAsync("le document a été supprimer");
            }
            else
            {
                return await Result<string>.FailAsync("le document n'existe pas.");
            }
        }

        public async Task<Result<DocumentDto>> GetByIdDocument(int id)
        {
            var document = await _context.DocumentSignatures
                 .SingleOrDefaultAsync(x => x.Id == id);
            var documentResponse = document.Adapt<DocumentDto>();
            return await Result<DocumentDto>.SuccessAsync(documentResponse);
        }

        public async Task<Result<List<DocumentDto>>> GetListeDocument(int idDemande)
        {
            var document = await _context.DocumentSignatures.OrderByDescending(x => x.Id).Where(x => x.IdDemandeSignature == idDemande).ToListAsync();
            var demandeResponse = document.Adapt<List<DocumentDto>>();
            return await Result<List<DocumentDto>>.SuccessAsync(demandeResponse);
        }

        public async Task<Result<string>> SignerDemande(int id)
        {
            var document = await _context.DocumentSignatures.FirstOrDefaultAsync(x => x.Id == id);

            var fileStorage = "Files\\Documents\\";
            var rawDocument = Path.Combine(fileStorage, document.FileName);

            var code_url = Guid.NewGuid().ToString("N");


            throw new System.NotImplementedException();
        }
    }
}
