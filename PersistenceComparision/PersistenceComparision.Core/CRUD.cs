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

        public void Create(OneModel model)
        {
            this.Repo.Create(model);
        }

        public OneModel ReadTiny(int id)
        {
            return this.Repo.ReadTinyModel(id);
        }

        public OneModel ReadOne(int id)
        {
            return this.Repo.ReadOneModel(id);
        }

        public void Update(OneModel model)
        {
            this.Repo.Update(model);
        }

        public void Update(OneModel model)
        {
            this.Repo.Update(model);
        }

        public void Delete(OneModel model)
        {
            this.Repo.Delete(model);
        }

        public void Delete(OneModel model)
        {
            this.Repo.Delete(model);
        }
    }
}
