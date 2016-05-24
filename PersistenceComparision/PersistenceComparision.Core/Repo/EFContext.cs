using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceComparision.Core.Repo
{
    class EFContext : DbContext
    {
        public DbSet<TinyModel> TinyEntities { get; set; }

        public EFContext():base()
        {

        }
    }
}
