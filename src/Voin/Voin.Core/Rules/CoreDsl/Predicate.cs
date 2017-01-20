using System;

namespace Voin.Core.Rules.CoreDsl
{
    public abstract class Predicate<TItem, TItemType> : IGroup<TItemType> where TItem : TItemType
    {
        public System.Func<TItemType, bool> Value { get; }

        protected Predicate(System.Func<TItem, bool> value)
        {
            Value = itemType => itemType is TItem && value((TItem)itemType);
        }

        public bool CanBe(TItemType item)
        {
            return this.Value(item);
        }
    }
}