using Microsoft.EntityFrameworkCore;
using WishList.API.Models;

namespace WishList.API.Repository
{
    public class WishListDbContext : DbContext
    {
        public WishListDbContext(DbContextOptions<WishListDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
