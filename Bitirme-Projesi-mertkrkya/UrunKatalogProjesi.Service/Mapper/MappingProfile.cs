using AutoMapper;
using UrunKatalogProjesi.Data.Models;
using UrunKatalogProjesi.Data.Dto;
using System.Collections.Generic;

namespace UrunKatalogProjesi.Service.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, AppUserDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, DetailCategoryDto>().ReverseMap();
            CreateMap<Offer, OfferDto>().ReverseMap();
            CreateMap<List<Offer>, List<OfferDto>>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, DetailProductDto>().ReverseMap();
            CreateMap<List<Product>, List<ProductDto>>().ReverseMap();

        }
    }
}
