﻿using System;
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

        public RightService(IRightStore rightStore, IRepository<IActor> actors, IRepository<IResource> resources, IRepository<IRight> rights)
        {
            this.rightStore = rightStore;
            this.actors = actors;
            this.resources = resources;
            this.rights = rights;
        }

        public bool HasRight(IActor actor, IRight right, IResource resource)
        {
            return this.rightStore.GetRights(actor, resource).Contains(right);
        }

        public IEnumerable<IRight> GetRightsObObject(IActor actor, IResource resource)
        {
            return this.rightStore.GetRights(actor, resource);
        }

        public void AddRule(string ruleId, IActor actor, IResource resource, IRight right)
        {
            this.rightStore.StoreRight(actor, resource, right, ruleId);
        }

        public void AddRule(string ruleId, Expression<Func<IActor, bool>> actorCriteria, Expression<Func<IResource, bool>> resourceCriteria, IRight rightToSet, params IRight[] otherRightsToSet)
        {
            this.InnerAddRule(
                ruleId,
                actors.Where(actorCriteria).ToList(),
                resources.Where(resourceCriteria).ToList(),
                new[] {rightToSet}.Concat(otherRightsToSet).Distinct()
            );
        }

        public void AddRule(string ruleId, IActor actor, Expression<Func<IResource, bool>> resourceCriteria, IRight rightToSet, params IRight[] otherRightsToSet)
        {
            this.InnerAddRule(
                ruleId,
                new[] { actor },
                resources.Where(resourceCriteria).ToList(),
                new[] { rightToSet }.Concat(otherRightsToSet).Distinct()
            );
        }

        public void AddRule(string ruleId, Expression<Func<IActor, bool>> actorCriteria, IResource resource, IRight rightToSet, params IRight[] otherRightsToSet)
        {
            this.InnerAddRule(
                ruleId,
                actors.Where(actorCriteria).ToList(),
                new[] {resource},
                new[] {rightToSet}.Concat(otherRightsToSet).Distinct()
            );
        }

        private void InnerAddRule(string ruleId, IEnumerable<IActor> actors, IEnumerable<IResource> resources, IEnumerable<IRight> rights)
        {
            var combinations = actors
                .SelectMany(actor => resources.SelectMany(
                    resource => rights.Select(
                        right => new RightInfo(actor, resource, right, new [] { new Rule {Id = ruleId}})))).ToList();

            this.rightStore.Add(combinations);
        }

        public void AddRule(Func<Root, ICompleteRule> rule)
        {
            this.rules.Add(rule(new Root()).Rule);
        }

        public void Initialize()
        {
            var combinations = actors
                .SelectMany(actor => resources.SelectMany(
                    resource => rights.Select(
                        right => new {Actor = actor, Resource = resource, Right = right,}))).ToList();

            var validCombinations = combinations
                .Select(c => new RightInfo(c.Actor, c.Resource, c.Right, this.rules.Where(r => r.HasAccess(c.Actor, c.Right, c.Resource))))
                .Where(c => c.Rules.Any())
                .ToList();

            this.rightStore.Add(validCombinations);
        }

        private IEnumerable<RightInfo> GetRightsInfo(IEnumerable<IActor> actorsToUpdate, IEnumerable<IResource> resourcesToUpdate)
        {
            var combinations = actorsToUpdate
                .SelectMany(actor => resourcesToUpdate.SelectMany(
                    resource => rights.Select(
                        right => new { Actor = actor, Resource = resource, Right = right, }))).ToList();

            var validCombinations = combinations
                .Select(c => new RightInfo(c.Actor, c.Resource, c.Right, this.rules.Where(r => r.HasAccess(c.Actor, c.Right, c.Resource))))
                .Where(c => c.Rules.Any())
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