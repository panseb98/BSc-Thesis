using Database.IUnitOfWork.Interfaces;
using Database.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Database.IUnitOfWork
{
    public class GenericUnitOfWork<TEntity, TKey> : IUnitOfWork<TEntity, TKey>
               where TEntity : class
               where TKey : IEquatable<TKey>
    {
        private bool _disposed = false;
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        public IRepository<TEntity, TKey> Repository { get; }
        public DataContext Database { get; }


        public GenericUnitOfWork(IRepository<TEntity, TKey> repository, DataContext database)
        {
            Repository = repository;
            Database = database;
        }

        ~GenericUnitOfWork() => Dispose(false);
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

        public async Task<int> Update(TEntity entity)
        {
            Database.Set<TEntity>().Update(entity);
            Database.Entry(entity).State = EntityState.Modified;
            return await Database.SaveChangesAsync();
        }
    }
}
