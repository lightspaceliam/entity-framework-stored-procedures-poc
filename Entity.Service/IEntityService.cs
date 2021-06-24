using System.Collections.Generic;
using System.Threading.Tasks;

namespace Entity.Service
{
    public interface IEntityService<T>
    {
        Task<T> InsertAsync(T entity);
        Task<List<T>> FindAsync();
        Task<T> FindByIdAsync(int id);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
