using Voin.Core;

namespace Voin.Test.Models.Actors
{
    public class User : IActor
    {
        public string Id { get; }
        public string Type => "User";

        public string Email { get; }

        public User(string id)
        {
            this.Id = id;
        }

        public User(string id, string email)
        {
            this.Id = id;
            this.Email = email;
        }

        public override string ToString()
        {
            return $"{this.Id} ({this.Type})";
        }
    }
}