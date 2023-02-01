using Core.Application.ViewModels.Mesa;
using Core.Application.ViewModels.Ordenes;
using Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface IMesaService : IGenericService<MesaSaveViewModel, MesaViewModel, Mesa>
    {
        Task<List<OrdenViewModel>> GetAllOrdenesAsync(int id);
    }
}
