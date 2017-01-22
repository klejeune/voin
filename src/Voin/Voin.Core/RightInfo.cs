using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Voin.Core.Rules.CoreDsl;

namespace Voin.Core
{
    public class RightInfo
    {
        public IActor Actor { get; }
        public IResource Resource { get; }
        public IRight Right { get; }
        public IRule Rule { get; }

        public RightInfo(IActor actor, IRight right, IResource resource, IRule rule)
        {
            this.Actor = actor;
            this.Resource = resource;
            this.Right = right;
            this.Rule = rule;
        }

        public override string ToString()
        {
            return $"{this.Actor} - {this.Right} - {this.Resource}";
        }

        public override int GetHashCode()
        {
            return this.Actor.GetHashCode() + this.Resource.GetHashCode() + this.Right.GetHashCode() + this.Rule.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as RightInfo);
        }

        private bool Equals(RightInfo obj)
        {
            return obj != null
                   && obj.Actor.Equals(this.Actor)
                   && obj.Resource.Equals(this.Resource)
                   && obj.Right.Equals(this.Right)
                   && obj.Rule.Equals(this.Rule);
        }
    }
}