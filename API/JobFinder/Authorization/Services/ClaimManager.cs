using Authorization.Models;
using Database.IUnitOfWork.Interfaces;
using Database.Models.Authorization;
using Database.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Services
{
    public class ClaimManager<TEntity, T> : IClaimsManager<TEntity, T>, IDisposable
          where TEntity : IdentityClaim<int>, new()
          where T : IEquatable<T>
    {

        private readonly IUnitOfWork<TEntity, T> unitOfWork;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private bool _disposed = false;

        public IRepository<TEntity, T> Claims { get; }

        public ClaimManager(
            IUnitOfWork<TEntity, T> unitOfWork,
            UserManager<User> userManager,
            RoleManager<Role> roleManager
            )
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.roleManager = roleManager;
            Claims = unitOfWork.Repository;
        }

        public virtual T ConvertIdFromString(string id)
        {
            if (id == null)
            {
                return default(T);
            }
            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(id);
        }

        public void Dispose() => _disposed = true;

        // Protected implementation of Dispose pattern.

        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        public async Task<ClaimResult> AssignClaimsToRoleAsync(Role role, IEnumerable<TEntity> claim)
        {
            ThrowIfDisposed();
            try
            {
                foreach (var item in claim)
                {
                    var c = new IdentityRoleClaim<int>()
                    {
                        ClaimType = item.Type,
                        ClaimValue = item.Value
                    };
                    unitOfWork.Database.Set<IdentityRoleClaim<int>>().Add(c);
                    await unitOfWork.Commit();
                }
                return new ClaimResult()
                {
                    Succeed = true
                };
            }
            catch (Exception ex)
            {
                return new ClaimResult()
                {
                    Succeed = false,
                    Exceptions = ex
                };
            }
        }

        public async Task<ClaimResult> AssignClaimsToUserAsync(User user, IEnumerable<TEntity> claims)
        {
            ThrowIfDisposed();
            try
            {
                foreach (var item in claims)
                {
                    var claim = new IdentityUserClaim<int>()
                    {
                        UserId = user.Id,
                        ClaimType = item.Type,
                        ClaimValue = item.Key
                    };
                    unitOfWork.Database.Set<IdentityUserClaim<int>>().Add(claim);
                }
                await unitOfWork.Commit();
                return new ClaimResult()
                {
                    Succeed = true
                };
            }
            catch (Exception ex)
            {
                return new ClaimResult()
                {
                    Succeed = false,
                    Exceptions = ex
                };
            }
        }

        public async Task<ClaimResult> AssignClaimToRoleAsync(Role role, TEntity claim)
        {
            ThrowIfDisposed();
            try
            {
                bool claimAssignedSuccessfully = true;
                int timesTried = 0;
                while (claimAssignedSuccessfully)
                {
                    var result = await roleManager.AddClaimAsync(role, claim.ToClaim());
                    if (!result.Succeeded)
                    {
                        claimAssignedSuccessfully = false;
                        timesTried++;
                    }
                    if (timesTried > 5)
                    {
                        claimAssignedSuccessfully = true;
                        timesTried = 0;
                    }
                }
                return new ClaimResult()
                {
                    Succeed = true
                };
            }
            catch (Exception ex)
            {

                return new ClaimResult()
                {
                    Succeed = false,
                    Exceptions = ex
                };
            }
        }

        public async Task<ClaimResult> AssignClaimToUserAsync(User user, TEntity claim)
        {
            ThrowIfDisposed();
            try
            {
                bool claimAssignedSuccessfully = true;
                int timesTried = 0;
                while (claimAssignedSuccessfully)
                {
                    var result = await userManager.AddClaimAsync(user, claim.ToClaim());
                    if (!result.Succeeded)
                    {
                        claimAssignedSuccessfully = false;
                        timesTried++;
                    }
                    if (timesTried > 5)
                    {
                        claimAssignedSuccessfully = true;
                        timesTried = 0;
                    }
                }
                return new ClaimResult()
                {
                    Succeed = true
                };
            }
            catch (Exception ex)
            {
                return new ClaimResult()
                {
                    Succeed = false,
                    Exceptions = ex
                };
            }
        }

        public async Task<ClaimResult> DeleteClaimAsync(string claimId, TEntity claim = null)
        {
            ThrowIfDisposed();
            try
            {
                var id = ConvertIdFromString(claimId);
                if (claim == null)
                {
                    claim = Claims.Find(x => x.Id.Equals(id)).FirstOrDefault();
                }
                Claims.Remove(claim);
                return new ClaimResult()
                {
                    Succeed = true
                };
            }
            catch (Exception ex)
            {
                return new ClaimResult()
                {
                    Succeed = false,
                    Exceptions = ex
                };
            }
        }

        public async Task<bool> DoesTheRoleHaveAClaim(Role role, string claimKey)
        {
            ThrowIfDisposed();
            try
            {
                var roleClaims = await roleManager.GetClaimsAsync(role);
                var claim = Claims.Find(x => x.Key == claimKey && x.Type == IdentityClaimTypes.RoleClaim.ToString()).FirstOrDefault();
                if (roleClaims.Contains(claim.ToClaim()))
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DoesTheUserHaveAClaim(User user, string[] claimKey)
        {
            ThrowIfDisposed();
            try
            {
                var userClaims = await userManager.GetClaimsAsync(user);
                if (userClaims.Where(x => claimKey.Contains(x.Type) || claimKey.Contains(x.Value)).Count() > 0)
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ClaimResult> EditClaimAsync(string claimId, TEntity claim = null)
        {
            ThrowIfDisposed();
            try
            {
                if (claim == null)
                {
                    claim = Claims.Find(ConvertIdFromString(claimId));
                }
                await unitOfWork.Update(claim);
                return new ClaimResult()
                {
                    Succeed = true
                };
            }
            catch (Exception ex)
            {
                return new ClaimResult()
                {
                    Succeed = false,
                    Exceptions = ex
                };
            }

        }

        public async Task<ClaimResult> CreateUserClaimAsync(TEntity entity)
        {
            ThrowIfDisposed();

            try
            {
                var alreadyExists = Claims.Find(x => x.Key == entity.Key && x.Type == entity.Type);
                if (alreadyExists.Count() > 0)
                    return new ClaimResult()
                    {
                        Succeed = false,
                        Result = new Exception("Object already exists")
                    };

                Claims.Add(entity);
                await unitOfWork.Commit();
                return new ClaimResult()
                {
                    Succeed = true
                };
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                return new ClaimResult()
                {
                    Succeed = false,
                    Exceptions = ex
                };
            }

        }

        public async Task<ClaimResult> CreateRoleClaimAsync(TEntity claim)
        {
            ThrowIfDisposed();
            try
            {
                var alreadyExists = Claims.Find(x => x.Key == claim.Key && x.Type == claim.Type);
                if (alreadyExists != null)
                    return new ClaimResult()
                    {
                        Succeed = false,
                        Result = new Exception("Object already exists")
                    };
                Claims.Add(claim);
                await unitOfWork.Commit();
                return new ClaimResult()
                {
                    Succeed = true
                };
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                return new ClaimResult()
                {
                    Succeed = false,
                    Exceptions = ex
                };
            }
        }

        public async Task<ClaimResult> RemoveClaimFromRoleAsync(Role role, TEntity claim)
        {
            ThrowIfDisposed();
            try
            {
                var identityResult = await roleManager.RemoveClaimAsync(role, claim.ToClaim());
                return new ClaimResult()
                {
                    Succeed = identityResult.Succeeded,
                    Result = identityResult.Errors
                };
            }
            catch (Exception ex)
            {
                return new ClaimResult()
                {
                    Succeed = false,
                    Exceptions = ex
                };
            }
        }

        public async Task<ClaimResult> RemoveClaimFromUserAsync(User user, TEntity claim)
        {
            ThrowIfDisposed();
            try
            {
                var identityResult = await userManager.RemoveClaimAsync(user, claim.ToClaim());
                return new ClaimResult()
                {
                    Succeed = identityResult.Succeeded,
                    Result = identityResult.Errors
                };
            }
            catch (Exception ex)
            {
                return new ClaimResult()
                {
                    Succeed = false,
                    Exceptions = ex
                };
            }
        }

        public async Task<TEntity> FindClaimAsync(string id, IdentityClaimTypes type, string key = null)
        {
            if (key != null)
            {
                return Claims.Find(x => x.Key == key && x.Type == type.ToString()).FirstOrDefault();
            }
            return Claims.Find(x => x.Id.Equals(id) && x.Type == type.ToString()).FirstOrDefault();
        }
    }
}
