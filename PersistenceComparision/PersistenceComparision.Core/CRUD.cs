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

        }

        public TinyModel Read(int id)
        {
            return null;
        }

        public void Update(TinyModel model)
        {

        }

        public void Delete(TinyModel model)
        {

        }
    }
}
