using AutoMapper;
using ITFriends.Library.Models;
using ITFriends.Web.Models.AccountViewModels;

namespace ITFriends.Web.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // SignInViewModel - Account
            CreateMap<SignInViewModel, Account>();
            CreateMap<Account, SignInViewModel>();

            // SignUpViewModel - Account
            CreateMap<SignUpViewModel, Account>();
            CreateMap<Account, SignUpViewModel>();
        }
    }
}