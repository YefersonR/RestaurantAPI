using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<Entity> AddAsync(Entity entity);
        Task UpdateAsync(Entity entity, int ID);
        Task DeleteAsync(Entity entity);
        Task<List<Entity>> GetAllAsync();
        Task<Entity> GetByIdAsync(int Id);
        Task<List<Entity>> GetAllWhitIncludes(List<String> properties);
    }
}
