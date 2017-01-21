using Voin.Core;

namespace Voin.Test.Models.Resources
{
    public class Printer : IResource
    {
        public bool IsActive { get; set; }

        public Printer(string id)
        {
            Id = id;
            this.IsActive = true;
        }

        public string Id { get; }
        public string Type => "Printer";

        public override string ToString()
        {
            return $"{this.Id} ({this.Type})";
        }
    }
}