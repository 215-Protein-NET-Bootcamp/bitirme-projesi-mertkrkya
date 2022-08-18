using System.Threading.Tasks;
using UrunKatalogProjesi.Core.Entities;
using UrunKatalogProjesi.Core.Models;

namespace UrunKatalogProjesi.Data.Repositories
{
    public interface IAccountRepository : IBaseRepository<AppUser>
    {
        //Task<AppUser> ValidateLoginAsync(LoginRequest loginRequest);
    }
}
