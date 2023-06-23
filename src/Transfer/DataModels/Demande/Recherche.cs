using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grs.BioRestock.Transfer.DataModels.Demande
{
    public class Recherche
    {
            public string Keyword { get; set; }
            public string Category { get; set; }
            public DateTime? DateFrom { get; set; }
            public DateTime? DateTo { get; set; }
    }

    
}
