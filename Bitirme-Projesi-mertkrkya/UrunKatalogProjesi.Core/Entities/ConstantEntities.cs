using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrunKatalogProjesi.Data.Entities
{
    public enum UserStatuses : int
    {
        Active = 1,
        Block = 2
    }
    public enum EmailTypes : int
    {
        Welcome = 1,
        Block = 2,
        Sold = 3,
        UnOffer = 4
    }
}
