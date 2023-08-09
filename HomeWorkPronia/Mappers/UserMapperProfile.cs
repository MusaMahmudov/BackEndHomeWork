using AutoMapper;
using HomeWorkPronia.Models.Identity;
using HomeWorkPronia.ViewModels.AccountViewModels;

namespace HomeWorkPronia.Mappers
{
    public class UserMapperProfile  : Profile
    {
        public UserMapperProfile() 
        {
            CreateMap<CreateAccountViewModel, AppUser>().ReverseMap();
        }

    }
}
