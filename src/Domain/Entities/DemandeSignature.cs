using Grs.BioRestock.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grs.BioRestock.Domain.Entities
{
    public class DemandeSignature : AuditableEntity<int>
    {
        public string Titre { get; set; }       
        public DateTime DateAnnulation { get; set; }
        public DateTime DateEtablissement { get; set; }
        //public DirectoryInfo Directory { get; set; }
        public List<DocumentSignature> DocumentSignatures { get; set; }
    }
}
