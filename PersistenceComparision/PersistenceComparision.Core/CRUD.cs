﻿using System;
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

        public TinyModel Read(int id)
        {
            return this.Repo.Read(id);
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