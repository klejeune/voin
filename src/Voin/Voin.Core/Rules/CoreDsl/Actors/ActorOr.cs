using Voin.Core.Rules.CoreDsl.Actors;

namespace Voin.Core.Rules.CoreDsl
{
    public class ActorOr : Or<IActorGroup, IActor>, IActorGroup
    {
        public ActorOr(IActorGroup first, IActorGroup second) : base(first, second)
        {
        }
    }
}