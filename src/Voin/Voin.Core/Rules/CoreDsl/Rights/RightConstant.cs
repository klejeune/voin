namespace Voin.Core.Rules.CoreDsl.Rights
{
    public class RightConstant : Constant<IRight>, IRightGroup
    {
        public RightConstant(IRight value) : base(value)
        {
        }
    }
}