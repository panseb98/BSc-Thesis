using Database.Models.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Services
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _configuration;

        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> GenerateJWTToken(User user)
        {
            var secret = Encoding.ASCII.GetBytes(_configuration["Tokens:JWTSecret"]);
            return await GenerateToken(user, secret);

        }
        private async Task<string> GenerateToken(User user, byte[] symetricKey)
        {
            try
            {
                var userClaims = new List<Claim>();//unitOfWork.Repository.Find(x => x.UserId == user.Id).Select(x => x.ToClaim()).ToList();
                userClaims.Add(new Claim(JwtRegisteredClaimNames.GivenName, user.Email));
               
                var JWToken = new JwtSecurityToken(
                            issuer: _configuration["Tokens:Issuer"],
                            audience: _configuration["Tokens:Audience"],
                            claims: null,
                            notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                            expires: new DateTimeOffset(DateTime.Now.AddMinutes(240)).DateTime,
                            //Using HS256 Algorithm to encrypt Token
                            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(symetricKey),
                                                SecurityAlgorithms.HmacSha256Signature)
                        );
                var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                return token;
            }
            catch (Exception ex)
            {

                return string.Empty;
            }
        }
    }
}
