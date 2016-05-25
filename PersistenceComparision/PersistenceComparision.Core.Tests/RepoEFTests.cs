using NUnit.Framework;
using NUnit.Framework.Compatibility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceComparision.Core.Tests
{
    [TestFixture]
    class RepoEFTests : AssertionHelper
    {
        [Test]
        public void Create_sequentialy_1000_tiny_entities_using_RepoEF()
        {
            for (int i = 0; i < 1000; i++)
            {
                var service = new CRUD(new Repo.RepoEF());
                TinyModel entity = null;

                entity = new TinyModel { Descricao = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "_" + i };
                service.Create(entity);
            }

            //Expect<int>(entity.Id, Is.GreaterThan(0));
        }

        [Test]
        public void CRUD_sequentialy_1000_tiny_entities_using_RepoEF()
        {
            for (int i = 0; i < 1000; i++)
            {
                var service = new CRUD(new Repo.RepoEF());
                TinyModel entity = null;

                entity = new TinyModel { Descricao = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "_" + i };
                service.Create(entity);

                var r = service.Read(entity.Id);

                r.Descricao += "_" + i;

                service.Update(r);

                service.Delete(r);
            }

            //Expect<int>(entity.Id, Is.GreaterThan(0));
        }

        [Test]
        public void Create_parallely_1000_tiny_entities_using_RepoEF()
        {
            Parallel.For(0, 1000, (int i) =>
            {
                var service = new CRUD(new Repo.RepoEF());
                TinyModel entity = null;

                entity = new TinyModel { Descricao = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "_" + i };
                service.Create(entity);
            });

            //Expect<int>(entity.Id, Is.GreaterThan(0));
        }

        [Test]
        public void CRUD_parallely_1000_tiny_entities_using_RepoEF()
        {
            Parallel.For(0, 1000, (int i) =>
            {
                var service = new CRUD(new Repo.RepoEF());
                TinyModel entity = null;

                entity = new TinyModel { Descricao = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "_" + i};
                service.Create(entity);

                var r = service.Read(entity.Id);

                r.Descricao += "_" + i;

                service.Update(r);

                service.Delete(r);
            });

            //Expect<int>(entity.Id, Is.GreaterThan(0));
        }
    }
}
