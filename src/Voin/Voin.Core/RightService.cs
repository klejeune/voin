using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Voin.Core.Rules.CoreDsl;
using Voin.Core.Rules.EnglishDsl;

namespace Voin.Core
{
    public class RightService
    {
        private readonly IRightStore rightStore;
        private readonly IRepository<IActor> actors;
        private readonly IRepository<IResource> resources;
        private readonly IRepository<IRight> rights;

        private readonly List<IRule> rules = new List<IRule>();

        public RightService(IRightStore rightStore, IRepository<IActor> actors, IRepository<IResource> resources, IRepository<IRight> rights, IEnumerable<IRule> rules = null)
        {
            this.rightStore = rightStore;
            this.actors = actors;
            this.resources = resources;
            this.rights = rights;
            this.rules = this.rules.ToList();
        }

        public bool HasRight(IActor actor, IRight right, IResource resource)
        {
            return this.rightStore.GetRights(actor, resource).Contains(right);
        }

        public IEnumerable<IRight> GetRights(IActor actor, IResource resource)
        {
            return this.rightStore.GetRights(actor, resource);
        }

        public IEnumerable<IResource> GetAccessibleResources(IActor actor, IRight right)
        {
            return this.rightStore.GetResources(actor, right);
        }

        public IEnumerable<IActor> GetAuthorizedActors(IRight right, IResource resource)
        {
            return this.rightStore.GetActors(right, resource);
        }

        public void AddRule(Func<Root, ICompleteRule> rule)
        {
            var newRule = rule(new Root()).Rule;

            var combinations = actors
                .SelectMany(actor => resources.SelectMany(
                    resource => rights.Select(
                        right => new {Actor = actor, Resource = resource, Right = right,}))).ToList();

            var validCombinations = combinations
                .Where(c => newRule.HasAccess(c.Actor, c.Right, c.Resource))
                .Select(c => new RightInfo(c.Actor, c.Right, c.Resource, newRule))
                .ToList();

            this.rightStore.Add(validCombinations);

            this.rules.Add(newRule);
        }
        
        private IEnumerable<RightInfo> GetRightsInfo(IEnumerable<IActor> actorsToUpdate, IEnumerable<IResource> resourcesToUpdate)
        {
            var combinations = actorsToUpdate
                .SelectMany(actor => resourcesToUpdate.SelectMany(
                    resource => rights.Select(
                        right => new { Actor = actor, Resource = resource, Right = right, }))).ToList();

            var validCombinations = combinations
                .SelectMany(c => this.rules.Where(r => r.HasAccess(c.Actor, c.Right, c.Resource)).Select(r => new RightInfo(c.Actor, c.Right, c.Resource, r)))
                .ToList();

            return validCombinations;
        }

        public void Update(IActor actor)
        {
            var oldRights = this.rightStore.GetRightsInfo(actor).ToList();

            var newRights = this.GetRightsInfo(new[] {actor}, this.resources).ToList();

            var rightsToDelete = oldRights.Where(o => !newRights.Contains(o)).ToList();
            var rightsToAdd = newRights.Where(n => !oldRights.Contains(n)).ToList();

            this.rightStore.Remove(rightsToDelete);
            this.rightStore.Add(rightsToAdd);
        }

        public void Update(IResource resource)
        {
            var oldRights = this.rightStore.GetRightsInfo(resource).ToList();

            var newRights = this.GetRightsInfo(this.actors, new[] {resource}).ToList();

            var rightsToDelete = oldRights.Where(o => !newRights.Contains(o)).ToList();
            var rightsToAdd = newRights.Where(n => !oldRights.Contains(n)).ToList();

            this.rightStore.Remove(rightsToDelete);
            this.rightStore.Add(rightsToAdd);
        }
    }
}