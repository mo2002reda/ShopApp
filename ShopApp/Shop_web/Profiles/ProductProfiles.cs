using AutoMapper;
using DAL.Entities;
using Shop_web.ViewModels;

namespace Shop_web.Profiles
{
    public class ProductProfiles : Profile
    {
        public ProductProfiles()
        {
            CreateMap<Product,ProductViewModel>().ForMember(x=>x.ImgName,v=>v.MapFrom(x=>x.Img)).ReverseMap();
        }
    }
}
