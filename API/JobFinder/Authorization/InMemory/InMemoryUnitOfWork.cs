using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.InMemory
{
    public class InMemoryUnitOfWork<TEntity, TKey> : IInMemoryUnitOfWork<TEntity, TKey>
        where TEntity : AuthorizationToken
        where TKey : IEquatable<TKey>
    {
        private bool _disposed = false;
        public InMemoryUnitOfWork(IInMemoryRepository<TEntity, TKey> inMemoryRepository, InMemoryDatabase context)
        {
            this.Repository = inMemoryRepository;
            this.Database = context;
        }
        public InMemoryDatabase Database { get; }
        public IInMemoryRepository<TEntity, TKey> Repository { get; }
        ~InMemoryUnitOfWork() => Dispose(false);
        public async Task<int> Commit()
        {
            return await Database.SaveChangesAsync();
        }

        public void Rollback()
        {
            Database.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                Database.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            _disposed = true;
        }

        public void Update(TEntity entity)
        {
            Database.Entry(entity).State = EntityState.Modified;
            Database.Set<TEntity>().Attach(entity);

        }
    }
}
