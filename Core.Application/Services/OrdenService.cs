using AutoMapper;
using Core.Application.Enums;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Ingrediente;
using Core.Application.ViewModels.Mesa;
using Core.Application.ViewModels.Orden;
using Core.Application.ViewModels.Ordenes;
using Core.Application.ViewModels.Platos;
using Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class OrdenService : GenericService<OrdenSaveViewModel, OrdenViewModel, Orden>,IOrdenService
    {
        private readonly IOrdenRepository _OrdenRepository;
        private readonly IPlatoRepository _PlatoRepository;
        private readonly IMesaRepository _MesaRepository;
        private readonly IMapper _mapper;

        public OrdenService(IOrdenRepository OrdenRepository, IPlatoRepository platoRepository, IMesaRepository MesaRepository, IMapper mapper) : base(OrdenRepository, mapper)
        {
            _PlatoRepository = platoRepository;
            _OrdenRepository = OrdenRepository;
            _mapper = mapper;
            _MesaRepository = MesaRepository;
        }
        public override async Task<OrdenSaveViewModel> Add(OrdenSaveViewModel vm)
        {
            vm.Estados = EstadoOrden.En_Proceso.ToString();
            Orden orden = _mapper.Map<Orden>(vm);
            foreach (int PlatoId in vm.Platos)
            {
                Plato plato = await _PlatoRepository.GetByIdAsync(PlatoId);
                orden.Platos.Add(plato);
                orden.Subtotal += plato.Precio;
            }
            Mesa mesa =  await _MesaRepository.GetByIdAsync(vm.MesaId);
            mesa.Estado = EstadosMesa.En_Proceso_de_atencion.ToString();
            orden.Mesa = mesa;
            await _MesaRepository.UpdateAsync(mesa, mesa.Id);
            await _OrdenRepository.AddAsync(orden);
            return vm;
        }
        public async Task UpdateOrden(EditOrdenViewModel vm, int ID)
        {
            List<Orden> ordenes = await _OrdenRepository.GetAllWhitIncludes();
            Orden orden = ordenes.FirstOrDefault(o=>o.Id == ID);
            orden.Mesa = await _MesaRepository.GetByIdAsync(orden.MesaId);
            orden.Subtotal = 0;
            orden.Platos.Clear();
            foreach (int PlatoId in vm.Platos)
            {
                Plato plato = await _PlatoRepository.GetByIdAsync(PlatoId);
                orden.Platos.Add(plato);
                orden.Subtotal += plato.Precio;
            }
            await _OrdenRepository.UpdateAsync(orden);
        }
        public async Task<List<OrdenViewModel>> GetAllViewModelWhitInclude()
        {
            List<Orden> ordenes = await _OrdenRepository.GetAllWhitIncludes();
            ordenes.ForEach(async orden =>
            {
                orden.Mesa = await _MesaRepository.GetByIdAsync(orden.MesaId);
            });
            List<OrdenViewModel> ordenesViewModel = _mapper.Map<List<OrdenViewModel>>(ordenes);

            return ordenesViewModel;
        }
        public async Task<OrdenViewModel> GetByIdOrden(int Id)
        {
            List<Orden> ordenes = await _OrdenRepository.GetAllWhitIncludes();
            
            Orden orden = ordenes.FirstOrDefault(o=>o.Id == Id);
            orden.Mesa = await _MesaRepository.GetByIdAsync(orden.MesaId);

            OrdenViewModel ordenViewModel = _mapper.Map<OrdenViewModel>(orden);
            
            return ordenViewModel;
        }

  
    }
}
