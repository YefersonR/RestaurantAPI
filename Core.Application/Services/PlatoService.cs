using AutoMapper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.PlatoIngrediente;
using Core.Application.ViewModels.Platos;
using Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class PlatoService : GenericService<PlatoSaveViewModel, PlatoViewModel, Plato>,IPlatoService
    {
        private readonly IPlatoIngredientesService _PlatoIngredientesService;
        private readonly IPlatoRepository _PlatoRepository;
        private readonly IMapper _mapper;

        public PlatoService(IPlatoRepository PlatoRepository, IPlatoIngredientesService PlatoIngredientesService, IMapper mapper) : base(PlatoRepository, mapper)
        {
            _PlatoRepository = PlatoRepository;
            _PlatoIngredientesService = PlatoIngredientesService;
            _mapper = mapper;
        }
        public override async Task<PlatoSaveViewModel> Add(PlatoSaveViewModel vm)
        {
             var plato = await base.Add(vm);
            PlatoIngredientesSaveViewModel PlatoIVM = new();
            foreach(PlatoIngredientesViewModel platoI in vm.Ingredientes)
            {
                PlatoIVM.IngredienteId = platoI.IngredienteId;
                PlatoIVM.PlatoId = plato.Id;
                await _PlatoIngredientesService.Add(PlatoIVM);
            }
            return vm;
 
        }
        public async Task<List<PlatoViewModel>> GetAllViewModelWhitInclude()
        {
            var Platos = await _PlatoRepository.GetAllWhitIncludes(new List<string> { "Ingredientes" });
            var IngrediestesPlatos = await _PlatoIngredientesService.GetAll();
            var PlatoIngrediente = from p in Platos
                                   join i in IngrediestesPlatos
                                   on p.Id equals i.PlatoId
                                select p;

            var PlatoVm =  _mapper.Map<List<PlatoViewModel>>(Platos);
            return PlatoVm;
        }
        public async Task<PlatoViewModel> GetByPlatoId(int Id)
        {
            var platos = await _PlatoIngredientesService.GetAll();
            var platosList = platos.Where(plato => plato.PlatoId == Id).ToList();

            var ListIngrediente = _mapper.Map<List<PlatoIngredientesViewModel>>(platosList);
            PlatoViewModel ordenViewModel = new();
            ordenViewModel.Ingredientes = ListIngrediente;
            return ordenViewModel;
        }
    }
}
