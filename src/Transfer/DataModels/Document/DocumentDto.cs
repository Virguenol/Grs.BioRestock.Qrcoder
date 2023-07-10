using Grs.BioRestock.Shared.Enums.Demande;
using Grs.BioRestock.Transfer.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grs.BioRestock.Transfer.DataModels.Document
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public string Designation { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string FileUrlsSigne { get; set; }
        public string NomClient { get; set; }
        public string NomSignateur { get; set; }
        public string PrenomSignateur { get; set; }
        public decimal Valeur { get; set; }
        public string NumberDoc { get; set; }
        public string CodeSignature { get; set; }
        public DateTime DateSignature { get; set; }
        public DateTime DateEtablissement { get; set; }
        public DateTime DateAnnulation { get; set; }
        public int IdDemandeSignature { get; set; }
        public DemandeStatut demandeStatut { get; set; } = DemandeStatut.Nouveau;
        public UploadRequest UploadRequest { get; set; }
        public byte[] FileData { get; set; }
    }
}
