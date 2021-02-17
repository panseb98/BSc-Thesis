using Authorization.Models;
using Database.Models.Authorization;
using Database.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Services
{
    public interface IClaimsManager
           <TEntity, T>
           where TEntity : class
           where T : IEquatable<T>
    {
        IRepository<TEntity, T> Claims { get; }
        Task<ClaimResult> CreateUserClaimAsync(TEntity claim);
        Task<ClaimResult> CreateRoleClaimAsync(TEntity claim);
        Task<ClaimResult> EditClaimAsync(string claimId, TEntity claim = null);
        Task<ClaimResult> DeleteClaimAsync(string claimId, TEntity claim = null);
        Task<ClaimResult> AssignClaimToUserAsync(User user, TEntity claim);
        Task<ClaimResult> AssignClaimsToUserAsync(User user, IEnumerable<TEntity> claims);
        Task<ClaimResult> AssignClaimToRoleAsync(Role role, TEntity claim);
        Task<ClaimResult> AssignClaimsToRoleAsync(Role role, IEnumerable<TEntity> claim);
        Task<bool> DoesTheUserHaveAClaim(User user, string[] claimKey);
        Task<bool> DoesTheRoleHaveAClaim(Role role, string claimKey);
        Task<ClaimResult> RemoveClaimFromRoleAsync(Role role, TEntity claim);
        Task<ClaimResult> RemoveClaimFromUserAsync(User user, TEntity claim);
        Task<TEntity> FindClaimAsync(string id, IdentityClaimTypes type, string key = null);
    }
}
