namespace Voin.Core.Rules.CoreDsl.Actors
{
    public class ActorAnd : And<IActorGroup, IActor>, IActorGroup
    {
        public ActorAnd(IActorGroup first, IActorGroup second) : base(first, second)
        {
        }
    }
}