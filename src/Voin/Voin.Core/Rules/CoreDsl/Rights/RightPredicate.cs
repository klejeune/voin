using System;

namespace Voin.Core.Rules.CoreDsl.Rights
{
    public class RightPredicate<TItem> : Predicate<TItem, IRight>, IRightGroup where TItem : IRight
    {
        public RightPredicate(Func<TItem, bool> value) : base(value)
        {
        }
    }
}