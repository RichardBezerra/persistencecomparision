using ServiceStack.OrmLite;
using System.Configuration;

namespace PersistenceComparision.Core.Repo
{
    public class RepoORMLite : IRepo
    {
        private string ConnString { get { return ConfigurationManager.ConnectionStrings["PersistenceComparision.Core.Repo.EFContext"].ConnectionString; } }

        public void Create(TinyModel model)
        {
            var dbFactory = new OrmLiteConnectionFactory(ConnString, MySqlDialect.Provider);

            using (var db = dbFactory.Open())
            {
                //if (db.CreateTableIfNotExists<TinyModel>())
                    model.Id = (int)db.Insert(model, selectIdentity: true);
            }
        }

        public void Delete(TinyModel model)
        {
            var dbFactory = new OrmLiteConnectionFactory(ConnString, MySqlDialect.Provider);

            using (var db = dbFactory.Open())
            {
                db.DeleteById<TinyModel>(model.Id);
            }
        }

        public TinyModel Read(int id)
        {
            var dbFactory = new OrmLiteConnectionFactory(ConnString, MySqlDialect.Provider);

            using (var db = dbFactory.Open())
            {
                return db.SingleById<TinyModel>(id);
            }
        }

        public void Update(TinyModel model)
        {
            var dbFactory = new OrmLiteConnectionFactory(ConnString, MySqlDialect.Provider);

            using (var db = dbFactory.Open())
            {
                db.Update<TinyModel>(model);
            }
        }
    }
}
