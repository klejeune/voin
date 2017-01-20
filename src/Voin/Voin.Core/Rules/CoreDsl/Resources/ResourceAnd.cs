namespace Voin.Core.Rules.CoreDsl.Resources
{
    public class ResourceAnd : And<IResourceGroup, IResource>, IResourceGroup
    {
        public ResourceAnd(IResourceGroup first, IResourceGroup second) : base(first, second)
        {
        }
    }
}