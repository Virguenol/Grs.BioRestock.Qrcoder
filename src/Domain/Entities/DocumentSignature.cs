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
        public string Title { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string FileUrlsSigne { get; set; }
        public string CodeSignature { get; set; }
        public DateTime DateSignature { get; set; }
        public DateTime DateEtablissement { get; set; }
        public DateTime DateAnnulation { get; set; }
        public int IdDemandeSignature { get; set; }
        public DemandeStatut demandeStatut { get; set; } = DemandeStatut.Nouveau;
        public virtual DemandeSignature DemandeSignature { get; set; }
    }
}
