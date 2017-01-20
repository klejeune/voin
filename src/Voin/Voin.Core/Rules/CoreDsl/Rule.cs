using Voin.Core.Rules.CoreDsl.Actors;
using Voin.Core.Rules.CoreDsl.Rights;

namespace Voin.Core.Rules.CoreDsl
{
    public class Rule : IRule
    {
        public IActorGroup Actor { get; set; }
        public IRightGroup Right { get; set; }
        public IResourceGroup Resource { get; set; }
        public string Id { get; set; }
        public bool HasAccess(IActor actor, IRight right, IResource resource)
        {
            return this.Actor.CanBe(actor) && this.Right.CanBe(right) && this.Resource.CanBe(resource);
        }
    }
}