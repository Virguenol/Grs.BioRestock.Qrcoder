﻿using Grs.BioRestock.Server.Services.Document;
using Grs.BioRestock.Shared.Wrapper;
using Grs.BioRestock.Transfer.DataModels.Demande;
using Grs.BioRestock.Transfer.DataModels.Document;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grs.BioRestock.Server.Controllers.DemandeService
{

    public class DocumentController : BaseApiController<DocumentController>
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet(nameof(GetAll) + "/{idDemande}")]
        public async Task<Result<List<DocumentDto>>> GetAll(int idDemande)
        {
            return await _documentService.GetListeDocument(idDemande);
        }

        [HttpGet(nameof(GetAll))]
        public async Task<Result<List<DocumentDto>>> GetAll()
        {
            return await _documentService.GetListeDocument();
        }

        [HttpPost(nameof(AddEdit))]
        public async Task<Result<string>> AddEdit(DocumentDto request)
        {
            return await _documentService.AddDocument(request);
        }

        [HttpGet(nameof(GetById) + "/{id}")]
        public async Task<Result<DocumentDto>> GetById(int id)
        {
            return await _documentService.GetByIdDocument(id);
        }

        [HttpDelete(nameof(Delete) + "/{id}")]
        public async Task<Result<string>> Delete(int id)
        {
            return await _documentService.DeleteDocument(id);
        }

        [HttpPost(nameof(SignerDocument) + "/{id}")]
        public async Task<Result<string>> SignerDocument(int id, string nom, string prenom)
        {
            return await _documentService.SignerDemande(id, nom, prenom);
        }

        [HttpGet(nameof(VerifierSingature) + "/{valeurCode}")]
        public async Task<Result<DocumentDto>> VerifierSingature(string valeurCode)
        {
            return await _documentService.VerifierSingature(valeurCode);
        }

        [HttpPost(nameof(AnnuleDemande) + "/{id}")]
        public async Task<Result<string>> AnnuleDemande(int id)
        {
            return await _documentService.AnnuleDemande(id);
        }

    }
}
