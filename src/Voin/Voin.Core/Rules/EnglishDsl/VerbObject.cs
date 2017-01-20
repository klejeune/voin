using Voin.Core.Rules.CoreDsl;

namespace Voin.Core.Rules.EnglishDsl
{
    public class VerbObject : ICompleteRule
    {
        public VerbObject(Rule rule)
        {
            this.Rule = rule;
        }

        public IRule Rule { get; }
    }
}