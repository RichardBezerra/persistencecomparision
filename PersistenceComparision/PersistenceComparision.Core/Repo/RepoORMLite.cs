using ServiceStack.OrmLite;
using System.Configuration;
using System;

namespace PersistenceComparision.Core.Repo
{
    public class RepoORMLite : IRepo
    {
        private string ConnString { get { return ConfigurationManager.ConnectionStrings["PersistenceComparision.Core.Repo.EFContext"].ConnectionString; } }

        public void Create(OneModel model)
        {
            var dbFactory = new OrmLiteConnectionFactory(ConnString, MySqlDialect.Provider);

            using (var db = dbFactory.Open())
            {
                model.Id = (int)db.Insert(model, selectIdentity: true);
                model.Many.ForEach((m) => { m.OneModelId = model.Id; });
                db.InsertAll(model.Many);
            }
        }

        public void Create(TinyModel model)
        {
            var dbFactory = new OrmLiteConnectionFactory(ConnString, MySqlDialect.Provider);

            using (var db = dbFactory.Open())
            {
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

        public OneModel ReadOneModel(int id)
        {
            var dbFactory = new OrmLiteConnectionFactory(ConnString, MySqlDialect.Provider);

            using (var db = dbFactory.Open())
            {
                var one = db.SingleById<OneModel>(id);

                one.Many.AddRange(db.SelectLazy<ManyModel>("OneModelId = @omi", new { omi = id }));

                return one;
            }
        }

        public TinyModel ReadTinyModel(int id)
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
