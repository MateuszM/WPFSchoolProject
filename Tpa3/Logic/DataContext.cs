using System.Data.Entity;
using Tpa3.Models;


namespace Tpa3.Logic
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
        public DbSet<AssemblyMetadata> Ameta { get; set; }
        public DbSet<ParameterMetadata> Parameter { get; set; }
        public DbSet<TypeMetadata> TypeMeta { get; set; }
        public DbSet<NamespaceMeta> Namespace { get; set; }
        public DbSet<PropertyMetadata> Property { get; set; }
    }
}