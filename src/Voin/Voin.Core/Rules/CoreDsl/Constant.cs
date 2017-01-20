namespace Voin.Core.Rules.CoreDsl
{
    public class Constant<T>
    {
        public T Value { get; }
        public Constant(T value)
        {
            Value = value;
        }

        public bool CanBe(T item)
        {
            return this.Value.Equals(item);
        }
    }
}