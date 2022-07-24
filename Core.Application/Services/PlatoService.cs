﻿using AutoMapper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Platos;
using Core.Domain.Entities;

namespace Core.Application.Services
{
    public class PlatoService : GenericService<PlatoSaveViewModel, PlatoViewModel, Plato>,IPlatoService
    {
        private readonly IPlatoRepository _PlatoRepository;
        private readonly IMapper _mapper;

        public PlatoService(IPlatoRepository PlatoRepository, IMapper mapper) : base(PlatoRepository, mapper)
        {
            _PlatoRepository = PlatoRepository;
            _mapper = mapper;
        }
    }
}