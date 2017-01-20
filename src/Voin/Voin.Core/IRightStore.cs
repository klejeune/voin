using System.Collections;
using System.Collections.Generic;

namespace Voin.Core
{
    public interface IRightStore
    {
        void StoreRight(IActor actor, IResource resource, IRight right, string ruleId);
        void StoreRightList(IEnumerable<RightInstance> instances);
        IEnumerable<IRight> GetRights(IActor actor, IResource resource);
    }
}