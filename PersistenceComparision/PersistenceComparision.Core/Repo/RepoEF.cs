using System;
using System.Data.Entity;
using System.Linq;

namespace PersistenceComparision.Core.Repo
{
    public class RepoEF : IRepo
    {
        public RepoEF():base()
        {

        }

        public void Create(OneModel model)
        {
            using (var context = new EFContext())
            {
                context.Ones.Add(model);
                context.SaveChanges();
            }
        }

        public void Create(TinyModel model)
        {
            using (var context = new EFContext())
            {
                context.TinyEntities.Add(model);
                context.SaveChanges();
            }
        }

        public void Delete(TinyModel model)
        {
            using (var context = new EFContext())
            {
                context.TinyEntities.Attach(model);
                context.TinyEntities.Remove(model);
                context.SaveChanges();
            }
        }

        public OneModel ReadOneModel(int id)
        {
            using (var context = new EFContext())
            {
                return context.Ones.Include((m) => m.Many).Single(o => o.Id == id);
            }
        }

        public TinyModel ReadTinyModel(int id)
        {
            using (var context = new EFContext())
            {
                return context.TinyEntities.Find(id);
            }
        }

        public void Update(OneModel model)
        {
            using (var context = new EFContext())
            {
                context.Ones.Attach(model);
                context.SaveChanges();
            }
        }

        public void Update(TinyModel model)
        {
            using (var context = new EFContext())
            {
                context.TinyEntities.Attach(model);
                context.SaveChanges();
            }
        }
    }
}
