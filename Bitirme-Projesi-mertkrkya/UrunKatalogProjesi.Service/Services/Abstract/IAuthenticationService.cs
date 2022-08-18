using System.Threading.Tasks;
using UrunKatalogProjesi.Core.Entities;
using UrunKatalogProjesi.Core.Models;
using UrunKatalogProjesi.Data.Dto;

namespace UrunKatalogProjesi.Service.Services
{
    public interface IAuthenticationService
    {
        Task<ResponseEntity> CreateTokenAsync(AppUser appUser); //TokenDto döneceğim. Login işlemleri için.
        Task<ResponseEntity> CreateTokenByRefreshTokenAsync(string refreshToken); //Refresh Token ile Access Token oluşturma.
    }
}
