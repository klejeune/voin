using Voin.Core;

namespace Voin.Test.Models.Resources
{
    public class File : IResource
    {
        public File(string id)
        {
            Id = id;
        }

        public string Id { get; }
        public string Type => "File";

        public override string ToString()
        {
            return $"{this.Id} ({this.Type})";
        }
    }
}