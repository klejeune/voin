namespace Voin.Core.Tools
{
    public static class EnumerableExtensions
    {
        public static T SafeCast<T>(this T item)
        {
            return item;
        }
    }
}