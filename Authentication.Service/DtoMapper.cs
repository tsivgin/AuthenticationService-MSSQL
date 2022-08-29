using Authentication.Core.DTOs;
using Authentication.Core.Model;
using AutoMapper;

namespace Authentication.Service
{
    internal class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<UserAppDto, UserApp>().ReverseMap();
        }
    }
}