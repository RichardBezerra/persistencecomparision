using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PersistenceComparision.Core.Repo
{
    class EFContext : DbContext
    {
        public DbSet<TinyModel> TinyEntities { get; set; }

        public EFContext():base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
