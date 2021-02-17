using Authorization.Database;
using Authorization.Database.Models;
using Authorization.Helpers.Exceptions;
using Authorization.Models;
using AutoMapper;
using Database;
using Database.Models.Authorization;
using Database.Repository;
using LanguageExt;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _dataContext;
        private readonly AuthorizationDbContext _authdbContext;

        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        // private readonly ITokenService<AuthorizationToken, Guid> tokenService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
      //  private readonly IRepository<IdentityUserClaim<int>, int> claimsRepository;


        public AuthService(IMapper mapper,
                           UserManager<User> userManager,
                           SignInManager<User> signInManager,
                           IJWTService jWTService,
                           AuthorizationDbContext authorizationDb)
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jWTService;
            _authdbContext = authorizationDb;
        }
        public async Task<Either<Exception, LoggedUser>> RegisterNewUser(RegisterUser model)
        {

            async Task AssignUserToRole(User user)
            {
                var claim = _authdbContext.Claims.First(x => x.Key == "NormalUser");
                var claimsResult = _authdbContext.UserClaims.Add(new UserClaim() { ClaimType = "NormalUser", ClaimValue = "NormalUser", UserId = user.Id });
                await _authdbContext.SaveChangesAsync();
            }
            try
            {
                var isUserExists = _authdbContext.Users.Find(x => x.Email == model.Email).Count().Equals(0);

                if (!isUserExists)
                    return new UserExistsException("Użytkownik o ponadym adresie email już istnieje");

                var userDbModel = new User()
                {
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    Email = model.Email
                };

                userDbModel.PasswordHash = _userManager.PasswordHasher.HashPassword(userDbModel, model.Password);

                _authdbContext.Users.Add(userDbModel);
                await _authdbContext.SaveChangesAsync();

                await AssignUserToRole(userDbModel);

                return await SingInUser(new LoginModel() { Email = model.Email, Password = model.Password });

            }
            catch (Exception ex)
            {
                return new Exception("Wystąpił nieoczekiwany błąd, spróbuj ponownie później");
            }   

        }

        public async Task<Either<Exception, LoggedUser>> SingInUser(LoginModel model)
        {
            var user = _authdbContext.Users.FirstOrDefault(x => x.Email == model.Email);

            if (user is null)
            {
                return new Exception("Nie ma konta z takim adresem email");
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (signInResult.Succeeded)
            {
                try
                {
                    var userClaims = _authdbContext.UserClaims.Find(x => x.UserId == user.Id).Select(x => x.ToClaim()).FirstOrDefault();
                    var userModel = new LoggedUser()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Id = user.Id.ToString(),
                        Username = user.Email,
                        Email = user.Email,
                        Token = await _jwtService.GenerateJWTToken(user),
                        Claim = new Dictionary<string, string>() { { userClaims.Type, userClaims.Value }, { ClaimTypes.Sid, user.Id.ToString() } }
                    };

                    return userModel;
                }
                catch(Exception e)
                {
                    return new Exception("To hasło nie pasuje do tego adresu email");
                }

            }
            else
            {
                return new Exception("To hasło nie pasuje do tego adresu email");
            }

        }
    }
}
