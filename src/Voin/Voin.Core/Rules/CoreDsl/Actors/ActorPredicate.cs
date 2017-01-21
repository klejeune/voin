using System;
using System.Collections.Generic;
using Voin.Core.Tools;

namespace Voin.Core.Rules.CoreDsl.Actors
{
    public class ActorPredicate<TItem> : Predicate<TItem, IActor>, IActorGroup<TItem> where TItem : IActor
    {
        public ActorPredicate(Func<TItem, bool> value) : base(value)
        {
        }
    }
}