using AutoMapper;
using WizCo.Core.Application.UseCases.Pedidos.DTOs;
using WizCo.Core.Domain.Entities;

namespace WizCo.Core.Application.Mappings;

public class PedidoMappingProfile : Profile
{
    public PedidoMappingProfile()
    {
        CreateMap<Pedido, PedidoDTO>()
            .ForMember(
                dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<ItemPedido, ItemPedidoDTO>();
    }
}