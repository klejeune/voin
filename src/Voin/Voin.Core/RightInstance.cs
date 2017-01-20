using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Voin.Core.Rules.CoreDsl;

namespace Voin.Core
{
    public class RightInstance
    {
        public IActor Actor { get; }
        public IResource Resource { get; }
        public IRight Right { get; }
        public IEnumerable<IRule> Rules { get; }

        public RightInstance(IActor actor, IResource resource, IRight right, IEnumerable<IRule> rules)
        {
            this.Actor = actor;
            this.Resource = resource;
            this.Right = right;
            this.Rules = rules.ToList();
        }
        
        public override string ToString()
        {
            return $"{this.Actor} - {this.Right} - {this.Resource}";
        }
    }
}