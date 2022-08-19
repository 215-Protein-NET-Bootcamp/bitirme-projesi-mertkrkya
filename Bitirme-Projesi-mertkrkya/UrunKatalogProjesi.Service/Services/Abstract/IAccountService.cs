using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrunKatalogProjesi.Data.Entities;
using UrunKatalogProjesi.Data.Models;
using UrunKatalogProjesi.Data.Dto;

namespace UrunKatalogProjesi.Service.Services
{
    public interface IAccountService : IBaseService<AppUserDto,AppUser>
    {
        Task<ResponseEntity> LoginFailedProcess(AppUser appUser);
    }
}
