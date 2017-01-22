using Voin.Core.Rules.CoreDsl;
using Voin.Core.Rules.CoreDsl.Actors;

namespace Voin.Core.Rules.EnglishDsl
{
    public class Root
    {
        private readonly Rule rule;

        public Root()
        {
            this.rule = new Rule();
        }

        public Subject AnyOne()
        {
            this.rule.Actor = new ActorPredicate<IActor>(actor => true);

            return new Subject(this.rule);
        }

        public Subject Any(string actorType)
        {
            return this.All(actorType);
        }

        public Subject<T> Any<T>() where T : IActor
        {
            return this.All<T>();
        }

        public Subject Only(string actorType)
        {
            this.rule.Actor = new ActorPredicate<IActor>(actor => actor.Type == actorType);

            return new Subject(this.rule);
        }

        public Subject Actor(IActor actor)
        {
            this.rule.Actor = new ActorPredicate<IActor>(a => a.Equals(actor));

            return new Subject(this.rule);
        }

        public Subject Only<T>() where T : IActor
        {
            var actorType = this.GetActorType<T>();
            this.rule.Actor = new ActorPredicate<T>(actor => actor.Type == actorType);

            return new Subject(this.rule);
        }

        public Subject Only(IActor actor)
        {
            this.rule.Actor = new ActorPredicate<IActor>(a => a.Equals(actor));

            return new Subject(this.rule);
        }

        public Subject All(string actorType)
        {
            this.rule.Actor = new ActorPredicate<IActor>(actor => actor.Type == actorType);

            return new Subject(this.rule);
        }

        public Subject<T> All<T>() where T : IActor
        {
            var actorType = this.GetActorType<T>();
            var actorPredicate = new ActorPredicate<T>(actor => actor.Type == actorType);
            this.rule.Actor = actorPredicate;

            return new Subject<T>(this.rule, actorPredicate);
        }

        private string GetActorType<T>() where T : IActor
        {
            return typeof(T).Name;
        }
    }
}