using AutoMapper;
using DAL.Entities;
using Shop_web.ViewModels;

namespace Shop_web.Profiles
{
    public class ProductCardProfile : Profile
    {
        public ProductCardProfile()
        {
            CreateMap<Product, ProductCardViewModel>().ForMember(x => x.ImgName, v => v.MapFrom(x => x.Img)).ReverseMap();
        }
    }
}
