using AutoMapper;
using Basket.Application.Dtos;
using Basket.Domain.Entities.ValueObjects;

namespace Basket.Application.AutoMapper
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<ProductInfoDto, ProductInfo>()
                .ForMember(dest => dest.Price.Currency, 
                    opt =>
                        opt.MapFrom(src => src.Price.Currency))
                .ForMember(dest => dest.Price.Amount, 
                    opt =>
                        opt.MapFrom(src => src.Price.Amount)) 
                .ForMember(dest => dest.Product.Id, 
                    opt =>
                        opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.Product.Name, 
                    opt =>
                        opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Product.Name, 
                    opt =>
                        opt.MapFrom(src => src.Product.Name)) 
                .ReverseMap();
        }
    }
}