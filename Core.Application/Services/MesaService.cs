using AutoMapper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Mesa;
using Core.Domain.Entities;


namespace Core.Application.Services
{
    public class MesaService : GenericService<MesaSaveViewModel, MesaViewModel, Mesa>,IMesaService
    {
        private readonly IMesaRepository _MesaRepository;
        private readonly IMapper _mapper;

        public MesaService(IMesaRepository MesaRepository, IMapper mapper) : base(MesaRepository, mapper)
        {
            _MesaRepository = MesaRepository;
            _mapper = mapper;
        }
    }
}
