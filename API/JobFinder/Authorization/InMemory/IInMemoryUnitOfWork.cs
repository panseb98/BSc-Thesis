using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.InMemory
{
    public interface IInMemoryUnitOfWork<TEntity, TKey> : IDisposable
           where TEntity : class
           where TKey : IEquatable<TKey>
    {
        InMemoryDatabase Database { get; }
        IInMemoryRepository<TEntity, TKey> Repository { get; }
        void Update(TEntity entity);
        Task<int> Commit();
        void Rollback();
    }
}
