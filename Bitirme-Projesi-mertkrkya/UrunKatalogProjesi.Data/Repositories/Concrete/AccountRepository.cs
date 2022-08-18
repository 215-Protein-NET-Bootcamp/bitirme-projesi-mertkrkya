using System.Linq;
using System.Threading.Tasks;
using UrunKatalogProjesi.Core.Entities;
using UrunKatalogProjesi.Core.Models;
using UrunKatalogProjesi.Data.Context;

namespace UrunKatalogProjesi.Data.Repositories
{
    public class AccountRepository : BaseRepository<AppUser>, IAccountRepository
    {
        protected readonly AppDbContext _appDbContext;
        public AccountRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
