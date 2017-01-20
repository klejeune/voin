using System;
using System.Collections.Generic;
using Voin.Core.Tools;

namespace Voin.Core.Rules.CoreDsl.Actors
{
    public class ActorPredicate<TItem> : Predicate<TItem, IActor>, IActorGroup<TItem> where TItem : IActor
    {
        private readonly List<Func<TItem, bool>> criterions = new List<Func<TItem, bool>>();

        public ActorPredicate(Func<TItem, bool> value) : base(value)
        {
            this.criterions.Add(value);
        }

        public IEnumerable<Func<TItem, bool>> Criterions => this.criterions;
        public void AddCriterion(Func<TItem, bool> criterion)
        {
            this.criterions.Add(criterion);
        }
    }
}