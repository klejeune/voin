using System;
using System.Collections.Generic;

namespace Voin.Core.Rules.CoreDsl
{
    public interface IResourceGroup : IGroup<IResource>
    {
    }

    public interface IResourceGroup<TItem> : IResourceGroup
    {
        IEnumerable<Func<TItem, bool>> Criterions { get; }

        void AddCriterion(Func<TItem, bool> criterion);
    }
}