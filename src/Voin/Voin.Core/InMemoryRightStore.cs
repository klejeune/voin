using System.Collections.Generic;
using System.Linq;
using Voin.Core.Rules.CoreDsl;

namespace Voin.Core
{
    public class InMemoryRightStore : IRightStore
    {
        private List<RightInstance> instances = new List<RightInstance>();

        public void StoreRight(IActor actor, IResource resource, IRight right, string ruleId)
        {
            instances.Add(new RightInstance(actor, resource, right, new[] { new Rule { Id = ruleId}}));
        }

        public void StoreRightList(IEnumerable<RightInstance> instances)
        {
            this.instances.AddRange(instances);
        }

        public IEnumerable<IRight> GetRights(IActor actor, IResource resource)
        {
            return this.instances
                .Where(i => i.Actor.Id == actor.Id && i.Resource.Id == resource.Id)
                .Select(i => i.Right)
                .Distinct();
        }
    }
}