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
        public IEnumerable<IRule> Rules { get; }

        public RightInfo(IActor actor, IResource resource, IRight right, IEnumerable<IRule> rules)
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

        public override int GetHashCode()
        {
            return this.Actor.GetHashCode() + this.Resource.GetHashCode() + this.Right.GetHashCode() + this.Rules.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as RightInfo);
        }

        private bool Equals(RightInfo obj)
        {
            return obj != null
                   && obj.Actor.Equals(this.Actor)
                   && obj.Resource == this.Resource
                   && obj.Right == this.Right
                   && obj.Rules.SequenceEqual(this.Rules);
        }
    }
}