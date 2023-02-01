using Core.Application.ViewModels.Platos;
using Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface IPlatoService : IGenericService<PlatoSaveViewModel, PlatoViewModel, Plato>
    {
        Task<List<PlatoViewModel>> GetAllViewModelWhitInclude();
        Task<PlatoViewModel> GetByPlatoId(int Id);
        Task<PlatoSaveViewModel> AddPlato(PlatoSaveViewModel vm);
        Task<PlatoSaveViewModel> UpdatePlato(PlatoSaveViewModel platoSaveViewModel, int id);

    }
}
