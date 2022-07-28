using AutoMapper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.MesaOrdenes;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class MesaOrdenesService : GenericService<MesaOrdenSaveViewModel, MesaOrdenViewModel, MesaOrdenes>, IMesaOrdenesService
    {
        private readonly IMesaOrdenesRepository _MesaOrdenRepository;
        private readonly IMapper _mapper;

        public MesaOrdenesService(IMesaOrdenesRepository MesaOrdenRepository, IMapper mapper) : base(MesaOrdenRepository, mapper)
        {
            _MesaOrdenRepository = MesaOrdenRepository;
            _mapper = mapper;
        }
    }
}
