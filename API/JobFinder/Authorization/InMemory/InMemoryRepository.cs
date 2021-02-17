using Database.Models;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.InMemory
{
    public class InMemoryRepository<TEntity, TKey> : IInMemoryRepository<TEntity, TKey>
           where TEntity : class
           where TKey : IEquatable<TKey>
    {
        public InMemoryRepository(InMemoryDatabase inMemory)
        {
            InMemory = inMemory;
        }

        public InMemoryDatabase InMemory { get; }

        public void Add(TEntity entity)
        {
            InMemory.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            InMemory.Set<TEntity>().AddRange(entities);
        }

        public async Task Clear()
        {
            InMemory.Set<TEntity>().RemoveRange(InMemory.Set<TEntity>());
            await InMemory.SaveChangesAsync();
        }

        public virtual TKey ConvertIdFromString(string id)
        {
            if (id == null)
            {
                return default(TKey);
            }
            return (TKey)TypeDescriptor.GetConverter(typeof(TKey)).ConvertFromInvariantString(id);
        }

        public TEntity Get(TKey id)
        {
            return InMemory.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return InMemory.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> GetAll(int skip, int take, Func<TEntity, object> orderByPredicate, Expression<Func<TEntity, bool>> filterPredicate, Sorted sort = Sorted.ASC)
        {
            var query = sort == Sorted.ASC ? //Sorted is enum
                InMemory.Set<TEntity>().OrderBy(orderByPredicate).Skip(skip).Take(take) :
                InMemory.Set<TEntity>().OrderByDescending(orderByPredicate).Skip(skip).Take(take);
            return query;
        }

        public TEntity Find(TKey id)
        {
            return InMemory.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return InMemory.Set<TEntity>().Where(predicate);
        }

        public void Remove(TEntity entity)
        {
            InMemory.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            InMemory.Set<TEntity>().RemoveRange(entities);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return InMemory.Set<TEntity>().SingleOrDefault(predicate);
        }

        public Task<TEntity> FindAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Option<TEntity> TrySingle(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Option<TEntity>> TrySingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
