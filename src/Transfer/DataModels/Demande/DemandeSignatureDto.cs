﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grs.BioRestock.Transfer.DataModels.Demande
{
    public class DemandeSignatureDto
    {
        public int Id { get; set; }
        public int? DossierParentId { get; set; }
        public string Titre { get; set; }
        public string UserId { get; set; }
        public DateTime DateAnnulation { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime DateEtablissement { get; set; }
        public List<DemandeSignatureDto> SousDossiers { get; set; }

    }
}
