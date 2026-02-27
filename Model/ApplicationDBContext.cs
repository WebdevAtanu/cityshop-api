using Microsoft.EntityFrameworkCore;

namespace cityshop_api.Model
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<ItemGroup> ItemGroups { get; set; }
        public DbSet<ItemSize> ItemSizes { get; set; }
        public DbSet<OrderBill> OrderBills { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<ShopItem> ShopItems { get; set; }
        public DbSet<ShopOrder> ShopOrders { get; set; }
        public DbSet<ShopStatus> ShopStatuses { get; set; }
        public DbSet<ShopType> ShopTypes { get; set; }
    }
}
