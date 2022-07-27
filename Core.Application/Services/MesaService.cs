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
        public override Task<MesaSaveViewModel> Add(MesaSaveViewModel vm)
        {
            if(vm.Estado == null || vm.Estado == "string")
            {
                vm.Estado = EstadosMesa.En_Proceso_de_atencion.ToString();
            }

            return base.Add(vm);
        }
        public async Task<List<OrdenViewModel>> GetAllOrdenesAsync(int id)
        {
            List<Orden> ordenes = await _OrdenRepository.GetAllAsync();
            var mesa = await _MesaRepository.GetAllAsync();
            mesa = mesa.Where(mesaa=>mesaa.Estado == EstadosMesa.En_Proceso_de_atencion.ToString()).ToList();
            List<OrdenViewModel> ordenesMesa = _mapper.Map<List<OrdenViewModel>>(ordenes);
             ordenesMesa= ordenesMesa.Where(orden=>orden.MesaId == id).ToList();
            var result = (from m in mesa
                          join o in ordenesMesa
                          on m.Id equals o.MesaId
                          where (o.MesaId == o.Id)
                          select o).ToList();
            return result;
        }
    }
}
