using Authorization.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Services
{
    public class TokenService<TEntity, TKey> : ITokenService<TEntity, TKey>
           where TEntity : AuthorizationToken, new()
           where TKey : IEquatable<TKey>
    {
        private readonly IInMemoryUnitOfWork<TEntity, TKey> UOW;

        public TokenService(IInMemoryUnitOfWork<TEntity, TKey> UOW)
        {
            this.UOW = UOW;
        }

        public async Task KillAllTokensAsync()
        {
            await UOW.Repository.Clear();
        }

        public async Task KillTokenAsync(string tokenId, TEntity token = null)
        {
            if (!string.IsNullOrEmpty(tokenId))
                token = UOW.Repository.Get(UOW.Repository.ConvertIdFromString(tokenId));
            if (token is null)
                return;
            UOW.Repository.Remove(token);
            await UOW.Commit();
        }

        public async Task SaveTokenToDatabaseAsync(TEntity token)
        {
            UOW.Repository.Add(token);
            await UOW.Commit();
        }

        public async Task<int> GetUserId(string token)
        {
            return await Task.FromResult(UOW.Repository.Find(x => x.Token == token).FirstOrDefault().UserId);
        }

        public async Task<bool> TokenExistsAsync(string token)
        {
            var tokenEntity = UOW.Repository.SingleOrDefault(x => x.Token == token);
            if (tokenEntity is null)
                return false;
            if (tokenEntity.IsAlive)
                return true;
            return false;
        }
    }
}
