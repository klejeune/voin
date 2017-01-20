namespace Voin.Core.Rules.CoreDsl.Resources
{
    public class ResourceOr : Or<IResourceGroup, IResource>, IResourceGroup
    {
        public ResourceOr(IResourceGroup first, IResourceGroup second) : base(first, second)
        {
        }
    }
}