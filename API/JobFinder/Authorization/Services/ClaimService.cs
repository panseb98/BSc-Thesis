using Authorization.Models;
using Database.Models.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Services
{
    public class ClaimsService<T> : IClaimsService<T>
        where T : IdentityClaim<int>, new()
    {
        private readonly IClaimsManager<T, int> claimsManager;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public ClaimsService(
            IClaimsManager<T, int> claimsManager,
            UserManager<User> userManager,
            RoleManager<Role> roleManager
            )
        {
            this.claimsManager = claimsManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<ClaimResult> AssignClaimTo<TEntity>(TEntity entity, IEnumerable<T> claims) where TEntity : class
        {
            switch (entity.GetType().Name)
            {
                case "User":
                    var user = entity as User;
                    if (user is null) goto default;
                    return await claimsManager.AssignClaimsToUserAsync(user, claims);
                case "Role":
                    var role = entity as Role;
                    if (role is null) goto default;
                    return await claimsManager.AssignClaimsToRoleAsync(role, claims);
                default:
                    return new ClaimResult() { Succeed = false };
            }
        }

        public async Task<ClaimResult> CreateClaimAsync(string key, string value, IdentityClaimTypes type)
        {
            switch (type)
            {
                case IdentityClaimTypes.UserClaim:
                    return await claimsManager.CreateUserClaimAsync(new T()
                    {
                        Key = key,
                        Value = value,
                        Type = IdentityClaimTypes.UserClaim.ToString()
                    });
                case IdentityClaimTypes.RoleClaim:
                    return await claimsManager.CreateRoleClaimAsync(new T()
                    {
                        Key = key,
                        Value = value,
                        Type = IdentityClaimTypes.RoleClaim.ToString()
                    });
                default:
                    return new ClaimResult() { Succeed = false };
            }
        }

        public async Task<T> FindClaimAsync(IdentityClaimTypes type, string claimId = null, string claimKey = null)
        {
            return await claimsManager.FindClaimAsync(claimId, type, claimKey);
        }

        public async Task<IEnumerable<T>> GetListOfRolesClaimsAsync(int skip, int take)
        {
            return claimsManager.Claims.GetAll(skip, take, x => x.Id, y => y.IsActive).Where(x => x.Type == IdentityClaimTypes.UserClaim.ToString());
        }

        public async Task<IEnumerable<T>> GetListOfUsersClaimsAsync(int skip, int take)
        {
            return claimsManager.Claims.GetAll(skip, take, x => x.Id, y => y.IsActive).Where(x => x.Type == IdentityClaimTypes.RoleClaim.ToString());
        }

        public async Task<ClaimResult> RemoveClaimAsync(string claimId, T claim = null)
        {
            return await claimsManager.DeleteClaimAsync(claimId, claim);
        }

        public async Task<ClaimResult> RemoveClaimFrom<TEntity>(TEntity entity, T claim) where TEntity : class
        {
            switch ((entity, claim))
            {
                case (null, _): return new ClaimResult() { Succeed = false };
                case (_, null): return new ClaimResult() { Succeed = false };
                case (User user, T cl):
                    return await claimsManager.RemoveClaimFromUserAsync(user, cl);
                case (Role role, T cl):
                    return await claimsManager.RemoveClaimFromRoleAsync(role, cl);
                default:
                    //entity is nethier a User or Role
                    return new ClaimResult() { Succeed = false };
            }
            /* if entity is null then this code will crash
            switch (entity.GetType().Name)
            {
                case "User":
                    if (entity is null || claim is null) goto default;
                    return await claimsManager.RemoveClaimFromUserAsync(entity as User, claim);
                case "Role":
                    if (entity is null || claim is null) goto default;
                    return await claimsManager.RemoveClaimFromRoleAsync(entity as Role, claim);
                default:
                    return new ClaimResult() { Succeed = false };
            }*/
        }

        public async Task<ClaimResult> UpdateClaimAsync(string claimId, T claim)
        {
            return await claimsManager.EditClaimAsync(claimId, claim);
        }
    }
}
