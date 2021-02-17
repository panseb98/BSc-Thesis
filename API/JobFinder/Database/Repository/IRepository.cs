using Database.Models;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repository
{
    public interface IRepository<TEntity, TKey>
            where TEntity : class
            where TKey : IEquatable<TKey>
    {
        TEntity Get(TKey id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(int skip, int take, Func<TEntity, object> orderByPredicate, Expression<Func<TEntity, bool>> filterPredicate, Sorted sort = Sorted.ASC);
        TEntity Find(TKey id);

        Option<TEntity> TrySingle(Expression<Func<TEntity, bool>> predicate);

        Task<Option<TEntity>> TrySingleAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FindAsync(TKey id);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        // This method was not in the videos, but I thought it would be useful to add.
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
