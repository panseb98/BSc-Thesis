using Authorization.Models;
using Database.IUnitOfWork.Interfaces;
using Database.Models.Authorization;
using Database.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Repository.Interfaces
{
    public interface IUserUOW<TEntity, TKey>
        where TEntity : class
        where TKey : IEquatable<TKey>
    {
        IRepository<IdentityUserRole<int>, int> UserRoleRepository { get; }
        IRepository<Role, int> RoleRepository { get; }
    }
}
