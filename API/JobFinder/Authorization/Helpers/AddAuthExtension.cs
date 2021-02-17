using Authorization.Services;
using Database;
using Database.Models.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

namespace Authorization.Helpers
{
    public static class AddAuthExtension
    {
        public static void AddAuth(this IServiceCollection services, string issuer, string audience, string secretKey)
        {

            services.AddAuthentication(auth => PrepareJWTConfig(auth))
             .AddJwtBearer(token => PrepareJWTokenConfig(token, secretKey, issuer, audience))
             .AddIdentityCookies();

              services.AddIdentityCore<User>()
                   .AddRoles<Role>()
                   .AddEntityFrameworkStores<DataContext>()
                   .AddSignInManager()
                   .AddDefaultTokenProviders();
               services.TryAddScoped(typeof(IAuthService), typeof(AuthService));
           

          /*public static void AddTokenValidation(this IServiceCollection services)
           {
               services.AddDbContext<InMemoryDatabase>(options => {
                   options.UseInMemoryDatabase("inMemory");
               });
               services.TryAddScoped(typeof(IInMemoryUnitOfWork<,>), typeof(InMemoryUnitOfWork<,>));
               services.TryAddScoped(typeof(IInMemoryRepository<,>), typeof(InMemoryRepository<,>));
               services.TryAddScoped(typeof(ITokenService<,>), typeof(TokenService<,>));
           }*/
        } 
            public static void AddTokenMiddleware(this IApplicationBuilder app)
            {
                app.Use(async (context, next) =>
                {
                    StringValues authToken;
                    context.Request.Headers.TryGetValue("Authorization", out authToken);
                    if (string.IsNullOrEmpty(authToken) && !authToken.ToString().StartsWith("Bearer ", System.StringComparison.OrdinalIgnoreCase))
                    {
                        context.Response.StatusCode = 401;
                        await next();
                    }
                    using (var scope = context.RequestServices.CreateScope())
                    {
                       // var tokenService = scope.ServiceProvider.GetService<ITokenService<AuthorizationToken, Guid>>();
                      /*  var tokenExists = await tokenService.TokenExistsAsync(authToken.ToString().Remove(0, 7));
                        if (!tokenExists)
                            context.Response.StatusCode = 401;*/
                    }
                    await next();
                });
            }

            private static AuthenticationOptions PrepareJWTConfig(AuthenticationOptions options)
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                return options;
            }
            private static JwtBearerOptions PrepareJWTokenConfig(JwtBearerOptions token, string SecretKey, string issuer, string audience)
            {
                token.RequireHttpsMetadata = false;
                token.SaveToken = true;
                token.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey)),
                    ValidateIssuer = false,
                    ValidIssuer = issuer,
                    ValidateAudience = false,
                    ValidAudience = audience,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
               /* token.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        //return Task.CompletedTask;

                    },
                };*/
                return token;
            }
            /// <summary>
            /// Funkcja przytotowuje opcje konta użytkownika.
            /// </summary>
            /// <param name="options"></param>
            /// <returns></returns>
            private static IdentityOptions ConfigurateIdentityOptions(IdentityOptions options)
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = false;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "aąbcćdeęfghijklłmnńoópqrsśtuvwxyzżźAĄBCĆDEĘFGHIJKLŁMNŃOÓPQRSŚTUVWXYZŻŹ0123456789-._@+";
                options.User.RequireUniqueEmail = true;


                return options;

            
        }
    }
}
