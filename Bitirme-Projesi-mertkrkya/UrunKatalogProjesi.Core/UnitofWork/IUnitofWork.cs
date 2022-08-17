using System.Threading.Tasks;

namespace UrunKatalogProjesi.Core.UnitofWork
{
    public interface IUnitofWork
    {
        Task CommitAsync();
        void Commit();
    }
}