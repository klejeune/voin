using System;
using Voin.Core.Rules.CoreDsl;

namespace Voin.Core.Rules.EnglishDsl
{
    public class VerbObject<T> : VerbObject where T : IResource
    {
        private readonly IResourceGroup<T> currentResourceGroup;

        public VerbObject(Rule rule, IResourceGroup<T> currentResourceGroup) : base(rule)
        {
            this.currentResourceGroup = currentResourceGroup;
        }
        public VerbObject With(Func<T, bool> criterion)
        {
            this.currentResourceGroup.AddCriterion(criterion);

            return this;
        }
    }

    public class VerbObject : ICompleteRule
    {
        public IRule Rule { get; }
        public VerbObject(Rule rule)
        {
            this.Rule = rule;
        }
    }
}