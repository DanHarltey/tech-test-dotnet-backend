using System.Linq;

namespace Moonpig.PostOffice.Data
{
    public interface IDbContext
    {
        IQueryable<Supplier> Suppliers { get; }

        IQueryable<Product> Products { get; }
    }
}
