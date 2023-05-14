using AutoMapper;
using NukeShop.API.Models.DTOs;
using NukeShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeShop.API.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryResultDto>()
                 .ReverseMap();
            CreateMap<Category, CategoryAddDto>().ReverseMap();
            CreateMap<Category, CategoryEditDto>().ReverseMap();

            CreateMap<Manufacturer, ManufacturerResultDto>().ReverseMap();
            CreateMap<Manufacturer, ManufacturerAddDto>().ReverseMap();
            CreateMap<Manufacturer, ManufacturerEditDto>().ReverseMap();

            CreateMap<Product, ProductEditDto>().ReverseMap();
            CreateMap<Product, ProductAddDto>().ReverseMap();                  
            CreateMap<Product, ProductResultDto>()
                .ForMember(dest => dest.ManufacturerName, y => y.MapFrom(src => src.Manufacturer.Name))
                .ReverseMap();
        }
    }
}
