using System.Linq;

namespace Voin.Core
{
    public interface IRepository<T> : IQueryable<T>
    {
        
    }
}