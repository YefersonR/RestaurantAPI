using AutoMapper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.MesaOrdenes;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class OrdenesPlatosService : GenericService<OrdenesPlatosSaveViewModel, OrdenesPlatosViewModel, OrdenesPlatos>, IOrdenesPlatosService
    {
        private readonly IOrdenesPlatosRepository _OrdenesPlatosRepository;
        private readonly IMapper _mapper;

        public OrdenesPlatosService(IOrdenesPlatosRepository OrdenesPlatosRepository, IMapper mapper) : base(OrdenesPlatosRepository, mapper)
        {
            _OrdenesPlatosRepository = OrdenesPlatosRepository;
            _mapper = mapper;
        }
        public async Task DeleteByOrdenId(int Id)
        {
            var ordenes = await _OrdenesPlatosRepository.GetAllAsync();
            var ordenesList = ordenes.Where(orden => orden.OrdenId== Id).ToList();
            foreach (var Orden in ordenesList)
            {
                await _OrdenesPlatosRepository.DeleteAsync(Orden);
            }
        }
        public async Task<List<OrdenesPlatosSaveViewModel>> GetAll()
        {
             var OrdenesPlatos =   await _OrdenesPlatosRepository.GetAllAsync();
            return _mapper.Map<List<OrdenesPlatosSaveViewModel>>(OrdenesPlatos);
        }
    }
}
