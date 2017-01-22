using System.Collections.Generic;
using System.Linq;
using Voin.Core.Rules.CoreDsl;

namespace Voin.Core
{
    public class InMemoryRightStore : IRightStore
    {
        private readonly List<RightInfo> instances = new List<RightInfo>();

        public void StoreRight(IActor actor, IRight right, IResource resource, IRule rule)
        {
            instances.Add(new RightInfo(actor, right, resource, rule));
        }

        public void Add(IEnumerable<RightInfo> rightsToAdd)
        {
            this.instances.AddRange(rightsToAdd);
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

        public IEnumerable<IRight> GetRights(IActor actor, IResource resource)
        {
            return this.instances
                .Where(i => i.Actor.Id == actor.Id && i.Resource.Id == resource.Id)
                .Select(i => i.Right)
                .Distinct();
        }

        public IEnumerable<IResource> GetResources(IActor actor, IRight right)
        {
            return this.instances
                .Where(i => i.Actor.Id == actor.Id && i.Right.Id == right.Id)
                .Select(i => i.Resource)
                .Distinct();
        }

        public IEnumerable<IActor> GetActors(IRight right, IResource resource)
        {
            return this.instances
                .Where(i => i.Right.Id == right.Id && i.Resource.Id == resource.Id)
                .Select(i => i.Actor)
                .Distinct();
        }
    }
}