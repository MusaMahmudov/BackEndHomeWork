using AutoMapper;
using HomeWorkPronia.Areas.Admin.ViewModels.ProductViewModels;
using HomeWorkPronia.Models;
using System.Drawing;

namespace HomeWorkPronia.Mappers
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile() 
        {
            CreateMap<CreateProductViewModel, Product>().ReverseMap();
            CreateMap<Product,DetailProductViewModel>().ReverseMap();
            CreateMap<Product, UpdateProductViewModel>().ForMember(x => x.Image, opt => opt.Ignore()).ReverseMap();
            //CreateMap<UpdateProductViewModel,Product>().ForMember(x => x.Image, opt => opt.Ignore());



        }

    }
}
