using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.Configuration;

namespace PersistenceComparision.Core.Repo.ORMLite
{
    public class RepoBaseORMLite<T> : Repo<T> where T : ModelBase
    {
        private string ConnString { get { return ConfigurationManager.ConnectionStrings["PersistenceComparision.Core.Repo.EF.EFContext"].ConnectionString; } }

        protected OrmLiteConnectionFactory dbFactory = null;

        public RepoBaseORMLite()
        {
            dbFactory = new OrmLiteConnectionFactory(ConnString, MySqlDialect.Provider);
        }

        public virtual IEnumerable<T> Read()
        {
            using (var db = dbFactory.Open())
            {
                return db.Select<T>();
            }
        }

        public virtual T FindById(int id)
        {
            using (var db = dbFactory.Open())
            {
                return db.SingleById<T>(id);
            }
        }

        public virtual void Create(T entity)
        {
            using (var db = dbFactory.Open())
            {
                entity.Id = (int)db.Insert(entity, selectIdentity: true);
            }
        }

        public virtual void Update(T entity)
        {
            using (var db = dbFactory.Open())
            {
                db.Update(entity);
            }
        }

        public virtual void Delete(T entity)
        {
            using (var db = dbFactory.Open())
            {
                db.DeleteById<T>(entity.Id);
            }
        }
    }
}
