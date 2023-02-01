using AutoMapper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Mesa;
using Core.Application.ViewModels.Ordenes;
using Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Core.Application.Enums;

namespace Core.Application.Services
{
    public class MesaService : GenericService<MesaSaveViewModel, MesaViewModel, Mesa>,IMesaService
    {
        private readonly IMesaRepository _MesaRepository;
        private readonly IOrdenRepository _OrdenRepository;
        private readonly IMapper _mapper;

        public MesaService(IMesaRepository MesaRepository,IOrdenRepository ordenRepository, IMapper mapper) : base(MesaRepository, mapper)
        {
            _MesaRepository = MesaRepository;
            _OrdenRepository = ordenRepository;
            _mapper = mapper;
        }
        public override async Task<MesaSaveViewModel> Add(MesaSaveViewModel vm)
        {
            vm.Estado = EstadosMesa.Disponible.ToString();
            return await base.Add(vm);
        }
        public override async Task Update(MesaSaveViewModel vm, int ID)
        {
            var mesas = await _MesaRepository.GetAllWhitIncludes(new List<string> { "Ordenes" });
            var mesa = mesas.FirstOrDefault(m=>m.Id == ID);
            if(vm.Estado is null || vm.Estado == "string")
            {
                vm.Estado = mesa.Estado;
            }
            if(vm.Estado == EstadosMesa.Atendida.ToString())
            {
                List<Orden> ordenes = await _OrdenRepository.GetAllWhitIncludes();
                List<Orden> ordenesByMesaId = ordenes.Where(o=>o.MesaId== mesa.Id).ToList();
                if(ordenesByMesaId is not null || ordenesByMesaId.Count != 0)
                {
                    foreach (Orden orden in ordenesByMesaId)
                    {
                        orden.Estados = EstadoOrden.Completada.ToString();
                        await _OrdenRepository.UpdateAsync(orden,orden.Id);
                    }
                }
            }
            await base.Update(vm, ID);
        }
        public async Task<List<OrdenViewModel>> GetAllOrdenesAsync(int id)
        {
            var mesa = await _MesaRepository.GetAllWhitIncludes(new List<string> { "Ordenes" });
            mesa = mesa.Where(mesaa=>mesaa.Estado == EstadosMesa.En_Proceso_de_atencion.ToString()).ToList();
            var ordenesMesa = mesa.Select(x => x.Ordenes);
            List<OrdenViewModel> result = _mapper.Map<List<OrdenViewModel>>(ordenesMesa);
      
            return result;
        }
    }
}
