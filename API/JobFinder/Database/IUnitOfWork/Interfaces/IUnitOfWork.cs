using Database.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Database.IUnitOfWork.Interfaces
{
    public interface IUnitOfWork<TEntity, TKey> : IDisposable
        where TEntity : class
        where TKey : IEquatable<TKey>
    {
        DataContext Database { get; }
        IRepository<TEntity, TKey> Repository { get; }
        Task<int> Update(TEntity entity);
        Task<int> Commit();
        void Rollback();
    }
}
