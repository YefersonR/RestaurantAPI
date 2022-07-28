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
    }

}
