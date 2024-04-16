using Talabat.Core.Entities;

namespace Talabat.Core.Repositories.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();
    }
}
