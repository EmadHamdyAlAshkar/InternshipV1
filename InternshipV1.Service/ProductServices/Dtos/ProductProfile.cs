using AutoMapper;
using InternshipV1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipV1.Service.ProductServices.Dtos
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDetailsDto>();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
        }
    }
}
