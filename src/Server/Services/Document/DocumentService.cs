using Grs.BioRestock.Application.Interfaces.Services;
using Grs.BioRestock.Client.Infrastructure.Authentication;
using Grs.BioRestock.Infrastructure.Contexts;
using Grs.BioRestock.Server.Services.Demande;
using Grs.BioRestock.Server.Services.QrCode;
using Grs.BioRestock.Shared.Enums.Demande;
using Grs.BioRestock.Shared.Wrapper;
using Grs.BioRestock.Transfer.DataModels.Document;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Grs.BioRestock.Server.Services.Document
{
    public interface IDocumentService
    {
        Task<Result<List<DocumentDto>>> GetListeDocument(int idDemande);
        Task<Result<List<DocumentDto>>> GetListeDocument();
        Task<Result<string>> AddDocument(DocumentDto demandeSignature);
        Task<Result<DocumentDto>> VerifierSingature(string valeurCode);
        Task<Result<DocumentDto>> GetByIdDocument(int id);
        Task<Result<string>> DeleteDocument(int id);
        Task<Result<string>> SignerDemande(int id, string nom, string prenom);
        Task<Result<string>> AnnuleDemande(int id);
    }
    public class DocumentService : IDocumentService
    {
        private readonly UniContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IUploadService _uploadService;
        public IQrCodeGenerateur _qrCodeGenerateur;

        public DocumentService(UniContext context, IUploadService uploadService, IWebHostEnvironment env,
            IConfiguration configuration, IDemandeSignatureService demandeSignatureService, IQrCodeGenerateur qrCodeGenerateur)
        {
            _context = context;
            _env = env;
            _configuration = configuration;
            _qrCodeGenerateur = qrCodeGenerateur;
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
                demandeSignature.NumberDoc = Guid.NewGuid().ToString("N");
                var document = demandeSignature.Adapt<Domain.Entities.DocumentSignature>();
                if (uploadRequest != null)
                {
                    demandeSignature.FileUrl = _uploadService.UploadAsync(uploadRequest);
                }
                document.IdDemandeSignature = demandeSignature.IdDemandeSignature;
                document.FileUrl = demandeSignature.FileUrl;
                document.FileName = demandeSignature?.FileName;
                document.CreatedOn = DateTime.Now;
                document.DateEtablissement = DateTime.Now;
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
                document.NomClient = demandeSignature.NomClient;
                document.Designation = demandeSignature.Designation;
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

        public async Task<Result<List<DocumentDto>>> GetListeDocument()
        {
            var document = await _context.DocumentSignatures.ToListAsync();
            var demandeResponse = document.Adapt<List<DocumentDto>>();
            return await Result<List<DocumentDto>>.SuccessAsync(demandeResponse);
        }

        public async Task<Result<string>> SignerDemande(int id, string nom, string prenom)
        {
            var code_url = Guid.NewGuid().ToString("N");
            var signed = $"https://localhost:3601/Validation_document/{code_url}";

            // Mise en forme du code QR
            BarcodeQRCode qr = new BarcodeQRCode(signed, 70, 70, null);
            var imgQR = qr.GetImage();

            var document = await _context.DocumentSignatures.FirstOrDefaultAsync(x => x.Id == id);
            
            var folderName = "Files\\Documents\\";
            var fileStorage = "Files\\Documents\\";

            var filePath = System.IO.Path.Combine(folderName, document.FileName);
            var filePathS = System.IO.Path.Combine(fileStorage, $"{code_url}.pdf");

            PdfReader reader = new PdfReader(filePath);
            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
                string text = PdfTextExtractor.GetTextFromPage(reader, i, new SimpleTextExtractionStrategy());
                string[] lines = text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                //foreach (string line in lines)
                //{
                //    Console.WriteLine(line);
                //}
                if (i == reader.NumberOfPages)
                {
                    FileStream fs = new FileStream(filePathS, FileMode.Create, FileAccess.Write, FileShare.None);
                    PdfStamper stamper = new PdfStamper(reader, fs);
                    PdfContentByte cb = stamper.GetOverContent(i);
                    
                    Phrase phrase = new Phrase(signed);

                    Rectangle pageSize = reader.GetPageSize(i);
                    float textX = pageSize.Left + 65;
                    float textY = pageSize.Bottom + 30;
                    // Positionnement du code QR
                    imgQR.SetAbsolutePosition(0, 0);
                    cb.AddImage(imgQR);


                    ColumnText.ShowTextAligned(cb, Element.ALIGN_LEFT, phrase, textX, textY, 0);
                    // Enregistrement du fichier modifié
                    stamper.Close();
                }
            }
            document.NomSignateur = nom;
            document.PrenomSignateur = prenom;
            document.FileUrlsSigne = filePathS;
            document.DateSignature = DateTime.Now;
            document.CodeSignature = code_url;
            document.demandeStatut = DemandeStatut.Signé;
            _context.DocumentSignatures.Update(document);
            await _context.SaveChangesAsync();
            return await Result<string>.SuccessAsync("le document a été signé");
        }

        public async Task<Result<DocumentDto>> VerifierSingature(string code)
        {
            var document = await _context.DocumentSignatures.SingleOrDefaultAsync(x => x.CodeSignature == code);
            var reponse = document.Adapt<DocumentDto>();
            return await Result<DocumentDto>.SuccessAsync(reponse);
        }

        public async Task<Result<string>> AnnuleDemande(int id)
        {
            var document = await _context.DocumentSignatures.SingleOrDefaultAsync(x => x.Id == id);
            if (document == null)
            {
                return await Result<string>.SuccessAsync("le document n'existe pas");
            }
            document.demandeStatut = DemandeStatut.Annulé;
            document.DateAnnulation = DateTime.Now;
            _context.DocumentSignatures.Update(document);
            await _context.SaveChangesAsync();
            return await Result<string>.SuccessAsync("Document Annulé");
        }

    }


}
