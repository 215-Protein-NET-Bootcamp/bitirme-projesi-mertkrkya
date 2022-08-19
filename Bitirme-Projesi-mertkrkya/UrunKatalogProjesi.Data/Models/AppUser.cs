﻿using Microsoft.AspNetCore.Identity;
using System;
using UrunKatalogProjesi.Data.Entities;

namespace UrunKatalogProjesi.Data.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TCKimlikNo { get; set; }
        public UserStatuses UserStatus { get; set; } = UserStatuses.Active;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }
    }
}
