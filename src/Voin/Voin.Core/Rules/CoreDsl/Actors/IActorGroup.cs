using System;
using System.Collections;
using System.Collections.Generic;

namespace Voin.Core.Rules.CoreDsl.Actors
{
    public interface IActorGroup : IGroup<IActor>
    {
        
    }

    public interface IActorGroup<TItem> : IActorGroup
    {
        IEnumerable<Func<TItem, bool>> Criterions { get; }

        void AddCriterion(Func<TItem, bool> criterion);
    }
}