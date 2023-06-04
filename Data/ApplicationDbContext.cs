using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCWebshop.Models;

namespace MVCWebshop.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ShopItem>()
                .Property(x => x.Price)
                .HasColumnType("decimal(6,2)");
        }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<ShopItem> ShopItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}
