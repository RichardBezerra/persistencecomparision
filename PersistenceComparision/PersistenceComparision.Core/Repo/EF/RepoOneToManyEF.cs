using System.Data.Entity;

namespace PersistenceComparision.Core.Repo.EF
{
    public class RepoOneToManyEF : RepoBaseEF<OneModel>
    {
        public RepoOneToManyEF():base(new EFContext())
        {

        }

        public RepoOneToManyEF(DbContext context) : base(context)
        {
        }
    }
}
