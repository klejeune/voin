using Voin.Core.Rules.CoreDsl;
using Voin.Core.Rules.CoreDsl.Resources;
using Voin.Core.Rules.CoreDsl.Rights;

namespace Voin.Core.Rules.EnglishDsl
{
    public class Verb
    {
        private Rule rule;

        public Verb(Rule rule)
        {
            this.rule = rule;
        }

        public VerbObject The(IResource resource)
        {
            this.rule.Resource = new ResourceConstant(resource);

            return new VerbObject(this.rule);
        }

        public VerbObject All(string resourceType)
        {
            this.rule.Resource = new ResourcePredicate(resource => resource.Type == resourceType);

            return new VerbObject(this.rule);
        }

        public VerbObject<T> All<T>() where T : IResource
        {
            var resourceType = this.GetResourceType<T>();
            var resource = new ResourcePredicate<T>(r => r.Type == resourceType);
            this.rule.Resource = resource;

            return new VerbObject<T>(this.rule, resource);
        }

        public VerbObject OnlyThe(IResource resource)
        {
            this.rule.Resource = new ResourceConstant(resource);

            return new VerbObject(this.rule);
        }

        public Verb And(IRight right)
        {
            this.rule.Right = new RightAnd(this.rule.Right, new RightConstant(right));

            return this;
        }

        public Verb Or(IRight right)
        {
            this.rule.Right = new RightAnd(this.rule.Right, new RightConstant(right));

            return this;
        }

        private string GetResourceType<T>() where T : IResource
        {
            return typeof(T).Name;
        }
    }
}