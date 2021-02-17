using Database.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Services
{
    public interface IJWTService
    {
        Task<string> GenerateJWTToken(User user);
    }
}
