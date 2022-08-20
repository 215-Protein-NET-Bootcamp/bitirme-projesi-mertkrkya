using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrunKatalogProjesi.Data.Entities;

namespace UrunKatalogProjesi.Data.Dto
{
    public class OfferDto //Gösterimlerde bu kullanılacak.
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string OfferUserId { get; set; }
        public OfferStatuses OfferStatus { get; set; }
    }
    public class InsertOfferDto //Insert ve Update işlemlerinde bu kullanılacak.
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public OfferStatuses OfferStatus { get; set; }
    }
}
