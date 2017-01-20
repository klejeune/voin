using Voin.Core.Rules.CoreDsl.Rights;

namespace Voin.Core.Rules.CoreDsl
{
    public class RightOr : Or<IRightGroup, IRight>, IRightGroup
    {
        public RightOr(IRightGroup first, IRightGroup second) : base(first, second)
        {
        }
    }
}