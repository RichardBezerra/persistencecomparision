using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PersistenceComparision.Core.Repo.EF
{
    public class RepoBaseEF<T> : Repo<T> where T : class
    {
        protected DbContext Context { get; set; }
        protected DbSet<T> DbSet { get; set; }

        public RepoBaseEF(DbContext context)
        {
            this.Context = context;
            this.DbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> Read()
        {
            return this.DbSet.ToList();
        }

        public virtual T FindById(int id)
        {
            return this.DbSet.Find(id);
        }

        public virtual void Create(T entity)
        {   
            this.DbSet.Add(entity);
            Context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            this.Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            if (entity != null)
            {
                this.DbSet.Remove(entity);
                Context.SaveChanges();
            }
        }
    }
}
