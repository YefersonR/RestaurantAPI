using Core.Application.ViewModels.MesaOrdenes;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface IMesaOrdenesService : IGenericService<MesaOrdenSaveViewModel, MesaOrdenViewModel, MesaOrdenes>
    {
    }
}
