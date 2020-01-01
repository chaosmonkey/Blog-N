using AutoMapper;
using Blogn.Commands;
using Blogn.Models;
using Blogn.ViewModels;

namespace Blogn.Configuration
{
    public class AppMappingProfile: Profile
    {
        public AppMappingProfile()
        {
            CreateMap<SignInModel, Authenticate>();
            CreateMap<RegistrationModel, Register>();
            CreateMap<Account, MyAccountModel>();
            CreateMap<Account, EditMyAccountModel>();
            CreateMap<EditMyAccountModel, EditMyAccount>();
            CreateMap<ChangePasswordModel, ChangePassword>();
            CreateMap<ForgotPasswordModel, ForgotPassword>();
            CreateMap<ResetPasswordModel, ResetPassword>();
        }
    }
}
