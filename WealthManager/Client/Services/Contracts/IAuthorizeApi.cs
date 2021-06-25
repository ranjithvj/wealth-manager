using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WealthManager.Shared.ViewModels;

namespace WealthManager.Client.Services.Contracts
{
    public interface IAuthorizeApi
    {
        Task Login(UserLoginVM loginParameters);
        Task Register(UserRegisterVM registerParameters);
        Task Logout();
        Task<UserAuthInfoVM> GetUserInfo();
    }
}
