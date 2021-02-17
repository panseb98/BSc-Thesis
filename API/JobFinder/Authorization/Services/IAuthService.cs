using Authorization.Models;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Services
{
    public interface IAuthService
    {
        Task<Either<Exception, LoggedUser>> RegisterNewUser(RegisterUser model);
        Task<Either<Exception, LoggedUser>> SingInUser(LoginModel model);


    }
}
