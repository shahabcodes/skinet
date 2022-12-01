using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Product, Product>()
            .ForMember (d=> d.PictureUrl, o=> o.MapFrom<ProductUrlResolver>());
        }
    }
}