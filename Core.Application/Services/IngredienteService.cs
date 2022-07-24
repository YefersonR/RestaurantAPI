using AutoMapper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Ingrediente;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class IngredienteService : GenericService<IngredienteSaveViewModel,IngredienteViewModel,Ingrediente>, IIngredienteService
    {
        private readonly IIngredienteRepository _ingredienteRepository;
        private readonly IMapper _mapper;

        public IngredienteService(IIngredienteRepository ingredienteRepository,IMapper mapper) :base(ingredienteRepository,mapper)
        {
            _ingredienteRepository = ingredienteRepository;
            _mapper = mapper;
        }
    }
}
