using Voin.Core.Rules.CoreDsl.Actors;
using Voin.Core.Rules.CoreDsl.Rights;

namespace Voin.Core.Rules.CoreDsl
{
    public interface IRule
    {
        string Id { get; }
        IActorGroup Actor { get; }
        IRightGroup Right { get; }
        IResourceGroup Resource { get; }
        bool HasAccess(IActor actor, IRight right, IResource resource);
    }
}