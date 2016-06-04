using System.Linq;
using ServiceStack.OrmLite;

namespace PersistenceComparision.Core.Repo.ORMLite
{
    public class RepoOneToManyORMLite : RepoBaseORMLite<OneModel>
    {
        public RepoOneToManyORMLite() : base()
        {

        }

        public override void Create(OneModel model)
        {
            base.Create(model);

            using (var db = dbFactory.Open())
            {
                model.Many.ForEach((m) =>
                {
                    m.OneModelId = model.Id;
                    m.Id = (int)db.Insert(m, selectIdentity: true);
                });
            }
        }

        public override OneModel FindById(int id)
        {
            var one = base.FindById(id);

            using (var db = this.dbFactory.Open())
            {
                one.Many.AddRange(db.SelectLazy<ManyModel>("OneModelId = @omi", new { omi = id }));
            }

            return one;
        }

        public override void Update(OneModel model)
        {
            base.Update(model);

            using (var db = dbFactory.Open())
            {
                db.UpdateAll(model.Many);
            }
        }

        public override void Delete(OneModel model)
        {
            using (var db = dbFactory.Open())
            {
                db.DeleteByIds<ManyModel>(model.Many.Select(m => m.Id).ToList());
            }

            base.Delete(model);
        }
    }
}
