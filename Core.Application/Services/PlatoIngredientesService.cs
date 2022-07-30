using AutoMapper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.MesaOrdenes;
using Core.Application.ViewModels.PlatoIngrediente;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class PlatoIngredientesService : GenericService<PlatoIngredientesSaveViewModel, PlatoIngredientesViewModel, PlatoIngredientes>, IPlatoIngredientesService
    {
        private readonly IPlatoIngredienteRepository _PlatoIngredientesRepository;
        private readonly IMapper _mapper;

        public PlatoIngredientesService(IPlatoIngredienteRepository PlatoIngredientesRepository, IMapper mapper) : base(PlatoIngredientesRepository, mapper)
        {
            _PlatoIngredientesRepository = PlatoIngredientesRepository;
            _mapper = mapper;
        }
        public async Task DeleteAllByPlatoId(int Id)
        {
            var ingredientes = await _PlatoIngredientesRepository.GetAllAsync();
            var ingredientesList = ingredientes.Where(i => i.PlatoId == Id).ToList();
            foreach (var i in ingredientesList)
            {
                await _PlatoIngredientesRepository.DeleteAsync(i);
            }
        }
    }

}
