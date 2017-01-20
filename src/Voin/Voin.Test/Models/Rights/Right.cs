using Voin.Core;

namespace Voin.Test.Models.Rights
{
    public class Right : IRight
    {
        public string Id { get; }

        public Right(string id)
        {
            this.Id = id;
        }

        public override string ToString()
        {
            return $"{this.Id}";
        }
    }
}