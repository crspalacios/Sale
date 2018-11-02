

namespace Sale.Backend.Models
{
    using Domain.Models;

    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<Sale.Common.Models.Product> Products { get; set; }
    }
}