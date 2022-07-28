using Core.Application.ViewModels.Platos;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface IPlatoService : IGenericService<PlatoSaveViewModel, PlatoViewModel, Plato>
    {
        Task<List<PlatoViewModel>> GetAllViewModelWhitInclude();
    }
}
