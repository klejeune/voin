using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Voin.Core
{
    public class InMemoryRepository<T> : IRepository<T>
    {
        private readonly List<T> items;

        public InMemoryRepository(IEnumerable<T> items)
        {
            this.items = items.ToList();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Expression Expression => items.AsQueryable().Expression;
        public Type ElementType => typeof(T);
        public IQueryProvider Provider => items.AsQueryable().Provider;
    }
}