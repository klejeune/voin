using System.Collections;
using System.Collections.Generic;

namespace Voin.Core
{
    public interface IRightStore
    {
        void StoreRight(IActor actor, IResource resource, IRight right, string ruleId);
        IEnumerable<IRight> GetRights(IActor actor, IResource resource);
        IEnumerable<IRight> GetRights(IActor actor);
        IEnumerable<IRight> GetRights(IResource resource);
        IEnumerable<RightInfo> GetRightsInfo(IActor actor);
        IEnumerable<RightInfo> GetRightsInfo(IResource resource);
        void Add(IEnumerable<RightInfo> rightsToAdd);
        void Remove(IEnumerable<RightInfo> rightsToDelete);
    }
}