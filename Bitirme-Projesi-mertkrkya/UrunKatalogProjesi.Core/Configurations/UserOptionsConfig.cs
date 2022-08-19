using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrunKatalogProjesi.Data.Configurations
{
    public class UserOptionsConfig
    {
        public int BlockAccessFailedCount { get; set; }
        public string EmailAccount { get; set; }
        public string EmailPassword { get; set; }
    }
}
