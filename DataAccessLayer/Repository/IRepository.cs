using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public interface IRepository<T> where T: BaseEntity
    {
        IRepository<T> Include(string navigationPropertyName);
        Task<int> GetCount();
        Task<IEnumerable<T>> GetAllAsync(int pageNumber, int pageSize);
        Task<T> GetByIdAsync(int id);
        Task<int> InsertAsync(T entity, bool unitOfWork = false);
        Task UpdateAsync(T entity, bool unitOfWork = false);
        Task DeleteAsync(int id, bool unitOfWork = false);
        Task SaveChanges();
    }
}
