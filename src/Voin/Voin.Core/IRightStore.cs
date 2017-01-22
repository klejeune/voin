using System.Collections;
using System.Collections.Generic;
using Voin.Core.Rules.CoreDsl;

namespace Voin.Core
{
    public interface IRightStore
    {
        void StoreRight(IActor actor, IRight right, IResource resource, IRule rule);
        IEnumerable<IRight> GetRights(IActor actor, IResource resource);
        IEnumerable<IResource> GetResources(IActor actor, IRight right);
        IEnumerable<IActor> GetActors(IRight right, IResource resource);
        IEnumerable<IRight> GetRights(IActor actor);
        IEnumerable<IRight> GetRights(IResource resource);
        IEnumerable<RightInfo> GetRightsInfo(IActor actor);
        IEnumerable<RightInfo> GetRightsInfo(IResource resource);
        void Add(IEnumerable<RightInfo> rightsToAdd);
        void Remove(IEnumerable<RightInfo> rightsToDelete);
    }
}