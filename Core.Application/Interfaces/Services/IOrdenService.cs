using Core.Application.ViewModels.Orden;
using Core.Application.ViewModels.Ordenes;
using Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface IOrdenService : IGenericService<OrdenSaveViewModel, OrdenViewModel, Orden>
    {
        Task<List<OrdenViewModel>> GetAllViewModelWhitInclude();
        Task<OrdenViewModel> GetByIdOrden(int Id);
        new Task<OrdenSaveViewModel> Add(OrdenSaveViewModel vm);
        Task UpdateOrden(EditOrdenViewModel vm, int ID);
    }
}
