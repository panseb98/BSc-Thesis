using Authorization.Models;
using Database.IUnitOfWork.Interfaces;
using Database.Models.Authorization;
using Database.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Services
{
    public interface IUsersService
    {
        IUnitOfWork<User, int> userUnitOfWork { get; }
        IRepository<Role, int> roleRepository { get; }
        IRepository<IdentityUserRole<int>, int> userRoleRepository { get; }
        Task<User> FindUserAsync<T>(T userId, string username = null, string email = null) where T : IEquatable<T>;
        Task<AuthResult<string>> UpdateUserAsync(User user, LoggedUser userModel = null);
        Task DeleteUserAsync<T>(T Id) where T : IEquatable<T>;
        Task<int> GetUsersCountAsync(bool isAdmin, string company, int? companyId);
        Task<AuthResult<IEnumerable<LoggedUser>>> GetUsersListAsync(int skip, int take, bool isAdmin, string company, int? companyId);
        Task<string> GetUserRole<T>(T userId) where T : IEquatable<T>;
        Task<LoggedUser> GetUserData(string email);
    }
}
