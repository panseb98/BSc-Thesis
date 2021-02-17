using Authorization.Models;
using Database.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Services
{
    public interface IRoleService
    {
        Task<AuthResult> CreateRoleAsync(RoleModel model);
        Task<AuthResult> EditRoleAsync<T>(T roleId, RoleModel model) where T : IEquatable<T>;
        Task<AuthResult> DeleteRoleAsync<T>(T roleId, Role role = null) where T : IEquatable<T>;
        Task<AuthResult> AssignRoleToUser(int userId, int roleId);
        Task<AuthResult> ChangeUserRole(User user, string role, string oldRole = null);
        Task<Role> FindRoleAsync<T>(T roleId, string roleName = null) where T : IEquatable<T>;
        Task<IEnumerable<Role>> GetListOfRolesAsync(int skip, int take);
    }
}
