using Database;
using Database.IUnitOfWork;
using Database.Models.Authorization;
using Database.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using Authorization.Repository.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace Authorization.Repository
{
    public class UserUOW<TEntity, TKey> : GenericUnitOfWork<TEntity, TKey>, IUserUOW<TEntity, TKey>
               where TEntity : User
               where TKey : IEquatable<TKey>
    {
        public UserUOW(
            IRepository<TEntity, TKey> repository,
            DataContext database,
            IRepository<IdentityUserRole<int>, int> userRoleRepository,
            IRepository<Role, int> roleRepository
            ) : base(repository, database)
        {
            UserRoleRepository = userRoleRepository;
            RoleRepository = roleRepository;
        }

        public IRepository<IdentityUserRole<int>, int> UserRoleRepository { get; }
        public IRepository<Role, int> RoleRepository { get; }
    }
}
