using System;
using System.Collections.Generic;

namespace Voin.Core.Rules.CoreDsl
{
    public abstract class Predicate<TItem, TItemType> : IGroup<TItemType> where TItem : TItemType
    {
        public System.Func<TItemType, bool> Value { get; }
        private readonly List<Func<TItem, bool>> criterions = new List<Func<TItem, bool>>();

        protected Predicate(System.Func<TItem, bool> value)
        {
            Value = itemType => itemType is TItem && value((TItem)itemType);
            this.criterions.Add(value);
        }

        public bool CanBe(TItemType item)
        {
            return this.Value(item) && this.criterions.TrueForAll(c => item is TItem && c((TItem)item));
        }

        public IEnumerable<Func<TItem, bool>> Criterions => this.criterions;
        public void AddCriterion(Func<TItem, bool> criterion)
        {
            this.criterions.Add(criterion);
        }
    }
}