namespace Voin.Core.Rules.CoreDsl.Rights
{
    public class RightAnd : And<IRightGroup, IRight>, IRightGroup
    {
        public RightAnd(IRightGroup first, IRightGroup second) : base(first, second)
        {
        }
    }
}