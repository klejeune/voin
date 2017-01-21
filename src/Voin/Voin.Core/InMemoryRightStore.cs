using System.Collections.Generic;
using System.Linq;
using Voin.Core.Rules.CoreDsl;

namespace Voin.Core
{
    public class InMemoryRightStore : IRightStore
    {
        private readonly List<RightInfo> instances = new List<RightInfo>();

        public void StoreRight(IActor actor, IResource resource, IRight right, string ruleId)
        {
            instances.Add(new RightInfo(actor, resource, right, new[] { new Rule { Id = ruleId}}));
        }

        public void Add(IEnumerable<RightInfo> rightsToAdd)
        {
            this.instances.AddRange(rightsToAdd);
        }

        public IEnumerable<IRight> GetRights(IActor actor, IResource resource)
        {
            return this.instances
                .Where(i => i.Actor.Id == actor.Id && i.Resource.Id == resource.Id)
                .Select(i => i.Right)
                .Distinct();
        }

        public IEnumerable<IRight> GetRights(IActor actor)
        {
            return this.instances
                .Where(i => i.Actor.Id == actor.Id)
                .Select(i => i.Right)
                .Distinct();
        }

        public IEnumerable<RightInfo> GetRightsInfo(IActor actor)
        {
            return this.instances.Where(i => i.Actor.Id == actor.Id);
        }

        public IEnumerable<RightInfo> GetRightsInfo(IResource resource)
        {
            return this.instances.Where(i => i.Resource.Id == resource.Id);
        }

        public IEnumerable<IRight> GetRights(IResource resource)
        {
            return this.instances
                .Where(i => i.Resource.Id == resource.Id)
                .Select(i => i.Right)
                .Distinct();
        }

        public void Remove(IEnumerable<RightInfo> rightsToDelete)
        {
            foreach (var r in rightsToDelete)
            {
                this.instances.Remove(r);
            }
        }
    }
}