using ApiStarPare.Dto;
using ApiStarPare.Models;
using AutoMapper;

namespace ApiStarPare.Profiles
{
    public class CarroProfile : Profile
    {
        public CarroProfile()
        {
            CreateMap<CarroDTO, Carro>();
            CreateMap<EstacionamentoDTO, Estacionamento>();
        }
    }
}
