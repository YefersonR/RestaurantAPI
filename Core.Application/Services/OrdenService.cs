using AutoMapper;
using Core.Application.Enums;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.MesaOrdenes;
using Core.Application.ViewModels.Orden;
using Core.Application.ViewModels.Ordenes;
using Core.Application.ViewModels.Platos;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class OrdenService : GenericService<OrdenSaveViewModel, OrdenViewModel, Orden>,IOrdenService
    {
        private readonly IOrdenRepository _OrdenRepository;
        private readonly IMesaRepository _MesaRepository;
        public readonly IOrdenesPlatosService _MesaOrdenesService;
        private readonly IMapper _mapper;

        public OrdenService(IOrdenRepository OrdenRepository, IOrdenesPlatosService MesaOrdenesService, IOrdenesPlatosRepository MesaOrdenRepository, IMesaRepository MesaRepository, IMapper mapper) : base(OrdenRepository, mapper)
        {
            _MesaOrdenesService = MesaOrdenesService;
            _OrdenRepository = OrdenRepository;
            _mapper = mapper;
            _MesaRepository = MesaRepository;
        }
        public override async Task<OrdenSaveViewModel> Add(OrdenSaveViewModel vm)
        {
            vm.Estados =  EstadosMesa.En_Proceso_de_atencion.ToString();

            OrdenesPlatosSaveViewModel ordenMesaVm = new();
            var Orden = await base.Add(vm);
            foreach (OrdenesPlatosViewModel orden in vm.Platos)
            {
                ordenMesaVm.Platoid = orden.Platoid;
                ordenMesaVm.Ordenid = Orden.Id;
                await _MesaOrdenesService.Add(ordenMesaVm);
            }
            return vm;
        }
        public async Task<List<OrdenViewModel>> GetAllViewModelWhitInclude()
        {
            var IngrediestesPlatos = await _OrdenRepository.GetAllWhitIncludes(new List<string> { "Platos" });
            var PlatoVm = _mapper.Map<List<OrdenViewModel>>(IngrediestesPlatos);
            return PlatoVm;
        }
        public async Task<List<OrdenViewModel>> GetByMesaId(int Id)
        {
            var ordenes = await _OrdenRepository.GetAllWhitIncludes(new List<string> { "Platos" });
            var ordenList = ordenes.Where(orden => orden.Id == Id).ToList();
            return _mapper.Map<List<OrdenViewModel>>(ordenList);
        }

  
    }
}
