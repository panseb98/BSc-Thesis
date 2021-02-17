using Database.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.InMemory
{
    public interface IInMemoryRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class
        where TKey : IEquatable<TKey>
    {
        Task Clear();
        InMemoryDatabase InMemory { get; }
        TKey ConvertIdFromString(string id);
    }
}
