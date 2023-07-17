using Grs.BioRestock.Domain.Contracts;
using Grs.BioRestock.Shared.Enums.Demande;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grs.BioRestock.Domain.Entities
{
    public class DocumentSignature : AuditableEntity<int>
    {
        public string Designation { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string FileUrlsSigne { get; set; }
        public string NomClient { get; set; }
        public string IdUser { get; set; }
        public string NomSignateur { get; set; }
        public string PrenomSignateur { get; set; }
        public decimal Valeur { get; set; }
        public string NumberDoc { get; set; }
        public string CodeSignature { get; set; }
        public DateTime DateSignature { get; set; }
        public DateTime DateEtablissement { get; set; }
        public DateTime DateAnnulation { get; set; }
        public DateTime CreatedCreation { get; set; }
        public int IdDemandeSignature { get; set; }
        public DemandeStatut demandeStatut { get; set; } = DemandeStatut.Nouveau;
        public virtual DemandeSignature DemandeSignature { get; set; }
    }
}
