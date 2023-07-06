using Grs.BioRestock.Server.Services.Demande;
using Grs.BioRestock.Shared.Wrapper;
using Grs.BioRestock.Transfer.DataModels.Demande;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grs.BioRestock.Server.Controllers.DemandeService
{
    public class DemandeController : BaseApiController<DemandeController>
    {
        private readonly IDemandeSignatureService _demande;

        public DemandeController(IDemandeSignatureService demande)
        {
            _demande = demande;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<Result<List<DemandeSignatureDto>>> GetAll()
        {
            return await _demande.GetDemandeSignature();
        }
        [HttpPost(nameof(AddEdit))]
        public async Task<Result<string>> AddEdit(DemandeSignatureDto request, int? parentId = null)
        {
            return await _demande.AddDemandeSignature(request, parentId);
        }
       
        [HttpDelete(nameof(Delete) + "/{id}")]
        public async Task<Result<string>> Delete(int id)
        {
            return await _demande.DeleteDemandeSignature(id);
        }

    }
}
