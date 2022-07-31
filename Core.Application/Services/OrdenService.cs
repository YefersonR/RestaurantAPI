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
        private readonly IIngredienteRepository _ingredienteRepository;
        private readonly IMesaRepository _MesaRepository;
        public readonly IOrdenesPlatosService _OrdenesPlatoService;
        private readonly IMapper _mapper;

        public OrdenService(IOrdenRepository OrdenRepository, IIngredienteRepository ingredienteRepository, IOrdenesPlatosService OrdenesPlatoService, IOrdenesPlatosRepository MesaOrdenRepository, IMesaRepository MesaRepository, IMapper mapper) : base(OrdenRepository, mapper)
        {
            _OrdenesPlatoService = OrdenesPlatoService;
            _ingredienteRepository = ingredienteRepository;
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
                await _OrdenesPlatoService.Add(ordenMesaVm);
            }
            return vm;
        }
        public async Task<List<OrdenViewModel>> GetAllViewModelWhitInclude()
        {
            var PlatoOrden = await _OrdenRepository.GetAllWhitIncludes(new List<string> { "Platos" });
            var ordenes = await _OrdenesPlatoService.GetAll();
            var PlatoPorOrden = from i in PlatoOrden
                                join o in ordenes
                                on i.Id equals o.Ordenid
                                select i;


            var PlatoVm = _mapper.Map<List<OrdenViewModel>>(PlatoPorOrden);
            return PlatoVm;
        }
        public async Task<OrdenViewModel> GetByMesaId(int Id)
        {
            var ordenes = await _OrdenesPlatoService.GetAll();
            var ordenList = ordenes.Where(orden => orden.Ordenid == Id).ToList();

            var ListOrdenPlato =  _mapper.Map<List<OrdenesPlatosSaveViewModel>>(ordenList);
            OrdenViewModel ordenViewModel = new();
            ordenViewModel.MesaId = Id;
        
            ordenViewModel.Platos = ListOrdenPlato;
            return ordenViewModel;
        }

  
    }
}
