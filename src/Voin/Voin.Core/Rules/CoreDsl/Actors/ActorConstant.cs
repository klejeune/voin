namespace Voin.Core.Rules.CoreDsl.Actors
{
    public class ActorConstant : Constant<IActor>, IActorGroup
    {
        public ActorConstant(IActor value) : base(value)
        {
        }
    }
}