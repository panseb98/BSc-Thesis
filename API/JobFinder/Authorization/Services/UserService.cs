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
    public class UsersService : IUsersService
    {
        private readonly UserManager<User> userManager;

        public UsersService(
            UserManager<User> userManager,
            IUnitOfWork<User, int> unitOfWork,
            IRepository<IdentityUserRole<int>, int> userRoleRepository,
            IRepository<Role, int> roleRepository,
            IUnitOfWork<IdentityUserClaim<int>, int> userClaims
            )
        {
            this.userManager = userManager;
            this.userUnitOfWork = unitOfWork;
            this.roleRepository = roleRepository;
            UserClaims = userClaims;
            this.userRoleRepository = userRoleRepository;
        }

        public IUnitOfWork<User, int> userUnitOfWork { get; }
        public IRepository<Role, int> roleRepository { get; }
        public IUnitOfWork<IdentityUserClaim<int>, int> UserClaims { get; }
        public IRepository<IdentityUserRole<int>, int> userRoleRepository { get; }

        public virtual T ConvertIdFromString<T>(string id) where T : IEquatable<T>
        {
            if (id == null)
            {
                return default(T);
            }
            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(id);
        }
        public async Task DeleteUserAsync<T>(T Id) where T : IEquatable<T>
        {
            try
            {
                var user = await userManager.FindByIdAsync(Id as string);
                user.IsActive = false;
                await UpdateUserAsync(user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<User> FindUserAsync<T>(T userId, string username = null, string email = null) where T : IEquatable<T>
        {
            if (!string.IsNullOrEmpty(username))
            {
                return await Task.FromResult(userUnitOfWork.Repository.Find(x => x.UserName == username && x.IsActive).FirstOrDefault());
            }
            else if (!string.IsNullOrEmpty(email))
            {
                return await Task.FromResult(userUnitOfWork.Repository.Find(x => x.Email == email && x.IsActive).FirstOrDefault());
            }
            else
            {
                return await Task.FromResult(userUnitOfWork.Repository.Find(x => x.Id.Equals(userId) && x.IsActive).FirstOrDefault());
            }

        }



        public async Task<LoggedUser> GetUserData(string email)
        {
            var user = await FindUserAsync(userId: string.Empty, email: email);

            if (user is null)
                return null;
            return new LoggedUser();
        }

        public async Task<string> GetUserRole<T>(T userId) where T : IEquatable<T>
        {
            var userRole = userRoleRepository.Find(x => x.UserId.Equals(userId)).FirstOrDefault();
            if (userRole is null)
                return await Task.FromResult(string.Empty);
            var role = roleRepository.Get(userRole.RoleId);
            return await Task.FromResult(role.Name);

        }

        public async Task<int> GetUsersCountAsync(bool isAdmin, string company, int? companyId)
        {
            /*  IQueryable<User> usersCountQuery = userUnitOfWork.Database.Users.Where(x => x.IsActive);

              return await Task.FromResult(usersCountQuery.Count());*/
            return 1;
        }

        public async Task<AuthResult<IEnumerable<LoggedUser>>> GetUsersListAsync(int skip, int take, bool isAdmin, string company, int? companyId)
        {
            /* try
             {
                 var usersList = userUnitOfWork.Repository.GetAll(skip, take, x => x.Id, y => y.IsActive)
                 if (!isAdmin)
                     usersList = usersList.Where(y => y.Company == company || (companyId.HasValue ? y.CompanyId == companyId.Value : false));
                 var list = usersList.ToList();
                 list.ForEach(item =>
                 {
                     item.Role = UserClaims.Repository.Find(x => x.UserId == Guid.Parse(item.Id)).FirstOrDefault() is null ?
                     string.Empty : UserClaims.Repository.Find(x => x.UserId == Guid.Parse(item.Id)).FirstOrDefault().ClaimType;
                     item.Claim = UserClaims.Repository.Find(x => x.UserId == Guid.Parse(item.Id)).Select(x => new Dictionary<string, string>() { { x.ClaimType, x.ClaimValue } }).FirstOrDefault();
                 });
                 return await Task.FromResult(new AuthResult<IEnumerable<LoggedUser>>(list));
             }
             catch (Exception ex)
             {
                 return new AuthResult<IEnumerable<LoggedUser>>(new string[] { ex.Message });
             }*/
            return new AuthResult<IEnumerable<LoggedUser>>(new string[] { "Aaa" });
        }



        public async Task<AuthResult<string>> UpdateUserAsync(User user, LoggedUser userModel = null)
        {
            IdentityResult updateResult = null;
            //For email change security
            var userWithNewEmailExist = await FindUserAsync(string.Empty, email: userModel.Email);
            if (userWithNewEmailExist != null && userWithNewEmailExist.Id != user.Id)
            {
                return new AuthResult<string>(new string[] { "Nie można nadać użytkownikowi danego adresu email, ponieważ jest on już w użyciu" });
            }

           /* if (userModel != null)
            {
                user = userModel.ToUserEntity(user);
                logger.Info($"Serwis {GetType().Name}. Zapis nowych danych użytkownika.");
                await userUnitOfWork.Update(user);
                logger.Info($"Serwis {GetType().Name}. Aktualizacji przywilejów użytkownika.");
                var userClaim = UserClaims.Repository.Find(x => x.Id == user.Id).FirstOrDefault();
                if (userClaim is null)
                {
                    userClaim = new IdentityUserClaim<int>() { UserId = user.Id, ClaimType = userModel.Claim.Keys.FirstOrDefault() };
                    UserClaims.Repository.Add(userClaim);
                    await UserClaims.Commit();
                    return new AuthResult<string>("Pomyślnie edytowano użytkownika");
                }
                userClaim.ClaimType = string.IsNullOrEmpty(key) ? "UserWithoutEdit" : key;
                userClaim.ClaimValue = string.IsNullOrEmpty(value) ? "USER" : value;
                await UserClaims.Update(userClaim);
                return new AuthResult<string>("Pomyślnie edytowano użytkownika");
            }*/
            updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return new AuthResult<string>(updateResult.Errors.Select(x => x.Description).ToArray());
            }
            return new AuthResult<string>("Pomyślnie edytowano użytkownika");
        }

       
    }
}
