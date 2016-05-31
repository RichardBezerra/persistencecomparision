using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceComparision.Core
{
    public class CRUD
    {
        private IRepo Repo { get; set; }

        public CRUD(IRepo repo)
        {
            this.Repo = repo;
        }

        public void Create(TinyModel model)
        {
            this.Repo.Create(model);
        }

        public TinyModel ReadTiny(int id)
        {
            return this.Repo.ReadTinyModel(id);
        }

        public OneModel ReadOne(int id)
        {
            return this.Repo.ReadOneModel(id);
        }

        public void Update(TinyModel model)
        {
            this.Repo.Update(model);
        }

        public void Delete(TinyModel model)
        {
            this.Repo.Delete(model);
        }
    }
}
