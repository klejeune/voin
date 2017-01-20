namespace Voin.Core.Rules.CoreDsl.Resources
{
    public class ResourceConstant : Constant<IResource>, IResourceGroup
    {
        public ResourceConstant(IResource value) : base(value)
        {
        }
    }
}