namespace Voin.Core.Rules.CoreDsl
{
    public class And<TGroup, TItem> where TGroup : IGroup<TItem>
    {
        public TGroup First { get; }
        public TGroup Second { get; }

        public And(TGroup first, TGroup second)
        {
            First = first;
            Second = second;
        }

        public bool CanBe(TItem item)
        {
            return this.First.CanBe(item) || this.Second.CanBe(item);
        }
    }
}