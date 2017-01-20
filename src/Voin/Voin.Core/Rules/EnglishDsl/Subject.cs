using System;
using Voin.Core.Rules.CoreDsl;
using Voin.Core.Rules.CoreDsl.Actors;
using Voin.Core.Rules.CoreDsl.Rights;

namespace Voin.Core.Rules.EnglishDsl
{
    public class Subject<T> : Subject where T : IActor
    {
        private readonly IActorGroup<T> currentActorGroup;

        public Subject(Rule rule, IActorGroup<T> currentActorGroup) : base(rule)
        {
            this.currentActorGroup = currentActorGroup;
        }

        public Subject<T> With(Func<T, bool> criterion)
        {
            this.currentActorGroup.AddCriterion(criterion);

            return this;
        }
    }

    public class Subject
    {
        private readonly Rule rule;

        public Subject(Rule rule)
        {
            this.rule = rule;
        }

        public Verb Can(IRight right)
        {
            this.rule.Right = new RightConstant(right);

            return new Verb(this.rule);
        }
    }
}