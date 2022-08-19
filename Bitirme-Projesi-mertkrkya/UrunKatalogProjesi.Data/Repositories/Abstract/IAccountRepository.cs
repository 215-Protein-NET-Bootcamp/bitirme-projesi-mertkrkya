using System.Threading.Tasks;
using UrunKatalogProjesi.Data.Entities;
using UrunKatalogProjesi.Data.Models;

namespace UrunKatalogProjesi.Data.Repositories
{
    public interface IAccountRepository : IBaseRepository<AppUser>
    {
        //Task<AppUser> ValidateLoginAsync(LoginRequest loginRequest);
    }
}
