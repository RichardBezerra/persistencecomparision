using System.Data.Entity;

namespace PersistenceComparision.Core.Repo.EF
{
    public class RepoLargeEF : RepoBaseEF<LargeModel>
    {
        public RepoLargeEF() : base(new EFContext())
        {

        }

        public RepoLargeEF(DbContext context) : base(context)
        {
        }
    }
}
