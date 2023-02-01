using Core.Application.ViewModels.Ingrediente;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
    public interface IIngredienteService : IGenericService<IngredienteSaveViewModel, IngredienteViewModel,Ingrediente>
    {
    }
}
