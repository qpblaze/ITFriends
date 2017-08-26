using AutoMapper;

namespace ITFriends_v2
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