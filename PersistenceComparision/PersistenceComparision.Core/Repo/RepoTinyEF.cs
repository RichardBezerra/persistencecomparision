using System.Data.Entity;

namespace PersistenceComparision.Core.Repo
{
    public class RepoTinyEF : RepoBaseEF<TinyModel>
    {
        public RepoTinyEF() : base(new EFContext())
        {

        }

        public RepoTinyEF(DbContext context) : base(context)
        {
        }
    }
}
