using Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Repositories
{
    public interface IOrdenRepository : IGenericRepository<Orden>
    {
        Task<List<Orden>> GetAllWhitIncludes();
        Task UpdateAsync(Orden entity);

    }
}
