using System;

namespace Voin.Core.Rules.CoreDsl.Resources
{
    public class ResourcePredicate<TResource> : Predicate<TResource, IResource>, IResourceGroup where TResource : IResource
    {
        public ResourcePredicate(Func<TResource, bool> value) : base(value)
        {
        }
    }

    public class ResourcePredicate : ResourcePredicate<IResource>
    {
        public ResourcePredicate(Func<IResource, bool> value) : base(value)
        {
        }
    }
}