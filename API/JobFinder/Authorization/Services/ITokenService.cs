using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Services
{
    public interface ITokenService<TEntity, TKey>
        where TEntity : class
        where TKey : IEquatable<TKey>
    {
        Task SaveTokenToDatabaseAsync(TEntity token);
        Task KillTokenAsync(string tokenId, TEntity token = null);
        Task KillAllTokensAsync();
        Task<bool> TokenExistsAsync(string token);
        Task<int> GetUserId(string token);
    }
}
