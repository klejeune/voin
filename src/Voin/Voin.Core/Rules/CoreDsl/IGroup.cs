namespace Voin.Core.Rules.CoreDsl
{
    public interface IGroup<in TItemType>
    {
        bool CanBe(TItemType item);
    }
}