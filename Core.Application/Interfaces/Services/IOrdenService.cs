using Core.Application.ViewModels.Orden;
using Core.Application.ViewModels.Ordenes;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface IOrdenService : IGenericService<OrdenSaveViewModel, OrdenViewModel, Orden>
    {
    }
}
