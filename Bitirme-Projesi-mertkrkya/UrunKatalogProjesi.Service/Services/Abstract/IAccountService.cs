using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrunKatalogProjesi.Core.Entities;
using UrunKatalogProjesi.Core.Models;
using UrunKatalogProjesi.Data.Dto;

namespace UrunKatalogProjesi.Service.Services
{
    public interface IAccountService : IBaseService<AppUserDto,AppUser>
    {
        //Burada kullanıcının get, getAll, Update işlemleri olacak.
    }
}
