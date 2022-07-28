﻿using AutoMapper;
using Core.Application.Enums;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
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
        private readonly IMesaRepository _MesaRepository;
        private readonly IMapper _mapper;

        public OrdenService(IOrdenRepository OrdenRepository, IMesaRepository MesaRepository, IMapper mapper) : base(OrdenRepository, mapper)
        {
            _OrdenRepository = OrdenRepository;
            _mapper = mapper;
            _MesaRepository = MesaRepository;
        }
        public override Task<OrdenSaveViewModel> Add(OrdenSaveViewModel vm)
        {
            vm.Estados =  EstadosMesa.En_Proceso_de_atencion.ToString();
            return base.Add(vm);
        }
        public async Task<List<OrdenViewModel>> GetByMesaId(int Id)
        {
            var ordenes = await _OrdenRepository.GetAllAsync();
            var ordenList = ordenes.Where(orden => orden.MesaId == Id).ToList();
            return _mapper.Map<List<OrdenViewModel>>(ordenList);
        }

        public Task UpdateOrden(OrdenSaveViewModel vm, int ID)
        {
            throw new NotImplementedException();
        }
    }
}
