using AutoMapper;
using Core.Application.Enums;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Ingrediente;
using Core.Application.ViewModels.Platos;
using Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class PlatoService : GenericService<PlatoSaveViewModel, PlatoViewModel, Plato>,IPlatoService
    {
        private readonly IPlatoRepository _PlatoRepository;
        private readonly IIngredienteRepository _IngredienteRepository;
        private readonly IMapper _mapper;

        public PlatoService(IPlatoRepository PlatoRepository, IIngredienteRepository IngredienteRepository, IMapper mapper) : base(PlatoRepository, mapper)
        {
            _PlatoRepository = PlatoRepository;
            _IngredienteRepository = IngredienteRepository;
            _mapper = mapper;
        }
        public async Task<PlatoSaveViewModel> AddPlato(PlatoSaveViewModel vm)
        {
            Plato plato = _mapper.Map<Plato>(vm);
            plato.Categoria = vm.Categoria switch
            {
                1 => CategoriaPlato.Bebida.ToString(),
                2 => CategoriaPlato.Bebida.ToString(),
                3 => CategoriaPlato.Bebida.ToString(),
                4 => CategoriaPlato.Bebida.ToString(),
                _ => "Invalid Category"
            };
            foreach (int platoI in vm.Ingredientes)
            {
                Ingrediente ingrediente = await _IngredienteRepository.GetByIdAsync(platoI);
                
                plato.Ingredientes!.Add(ingrediente);
            }
            
            await _PlatoRepository.AddAsync(plato);
            return vm;
        }

        public async Task<PlatoSaveViewModel> UpdatePlato(PlatoSaveViewModel platoSaveViewModel,int Id)
        {
            var platos = await _PlatoRepository.GetAllWhitIncludes(new List<string> { "Ingredientes" });
            var plato = platos.FirstOrDefault(plato => plato.Id == Id);
            
            plato.Nombre = platoSaveViewModel.Nombre;
            plato.Precio = platoSaveViewModel.Precio;
            plato.Categoria = platoSaveViewModel.Categoria switch
            {
                1 => CategoriaPlato.Bebida.ToString(),
                2 => CategoriaPlato.Bebida.ToString(),
                3 => CategoriaPlato.Bebida.ToString(),
                4 => CategoriaPlato.Bebida.ToString(),
                _ => "Invalid Category"
            };

            plato.Ingredientes.Clear();
            foreach (int platoI in platoSaveViewModel.Ingredientes)
            {
                Ingrediente ingrediente = await _IngredienteRepository.GetByIdAsync(platoI);

                plato.Ingredientes!.Add(ingrediente);
            }
            await _PlatoRepository.UpdateAsync(plato, Id);

            return platoSaveViewModel;
        }

        public async Task<List<PlatoViewModel>> GetAllViewModelWhitInclude()
        {
            var Platos = await _PlatoRepository.GetAllWhitIncludes(new List<string> { "Ingredientes" });
            var PlatoVm =  _mapper.Map<List<PlatoViewModel>>(Platos);
            foreach(PlatoViewModel plato in PlatoVm)
            {
                var platoIngrediente = Platos.FirstOrDefault(x => x.Id == plato.Id);
                List<IngredienteViewModel> ingrediente = _mapper.Map<List<IngredienteViewModel>>(platoIngrediente.Ingredientes);
                plato.Ingredientes = ingrediente;
            }
            return PlatoVm;
        }
        public async Task<PlatoViewModel> GetByPlatoId(int Id)
        {
            var platos = await _PlatoRepository.GetAllWhitIncludes(new List<string> { "Ingredientes" });
            var plato = platos.FirstOrDefault(plato => plato.Id == Id);
            var platoViewModels = _mapper.Map<PlatoViewModel>(plato);
            platoViewModels.Ingredientes = (_mapper.Map<List<IngredienteViewModel>>(plato.Ingredientes));
            
            return platoViewModels;
        }
    }
}
