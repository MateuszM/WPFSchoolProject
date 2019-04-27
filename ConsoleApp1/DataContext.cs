using Tpa3.Models;


namespace Data
{
    public partial class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
        public DbSet<ASe> Ameta { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Favorite> Favorite { get; set; }
        public DbSet<In_Store_Promotions> In_Store_Promotions { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Opinion> Opinion { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Promotions> Promotions { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}