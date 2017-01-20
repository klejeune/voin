namespace Voin.Core.Rules.CoreDsl
{
    public class Or<TGroup, TItem> where TGroup : IGroup<TItem>
    {
        public TGroup First { get; }
        public TGroup Second { get; }

        public Or(TGroup first, TGroup second)
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