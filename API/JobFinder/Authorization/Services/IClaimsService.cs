using Authorization.Models;
using Database.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Services
{
    public interface IClaimsService<T> where T : IdentityClaim<int>
    {
        Task<ClaimResult> CreateClaimAsync(string key, string value, IdentityClaimTypes type);
        Task<ClaimResult> UpdateClaimAsync(string claimId, T claim);
        Task<T> FindClaimAsync(IdentityClaimTypes type, string claimId = null, string claimKey = null);
        Task<ClaimResult> RemoveClaimAsync(string claimId = null, T claim = null);
        Task<ClaimResult> AssignClaimTo<TEntity>(TEntity entity, IEnumerable<T> claim) where TEntity : class;
        Task<ClaimResult> RemoveClaimFrom<TEntity>(TEntity entity, T claim) where TEntity : class;
        Task<IEnumerable<T>> GetListOfUsersClaimsAsync(int skip, int take);
        Task<IEnumerable<T>> GetListOfRolesClaimsAsync(int skip, int take);
    }
}
