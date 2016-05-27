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
    class RepoImplsTests : AssertionHelper
    {
        IRepo CreateImpl(string key)
        {
            if (key.Equals("EF"))
                return new Repo.RepoEF();

            if (key.Equals("ADO"))
                return new Repo.RepoADO();

            if (key.Equals("ORMLite"))
                return new Repo.RepoORMLite();

            return null;
        }

        [Test, Combinatorial]
        public void Create_sequentialy_N_tiny_entities([Values(1000)] int qtd, [Values("ADO","EF", "ORMLite")] string impl)
        {
            for (int i = 0; i < qtd; i++)
            {
                var service = new CRUD(CreateImpl(impl));
                TinyModel entity = null;

                entity = new TinyModel { Descricao = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "_" + i };
                service.Create(entity);
            }

            //Expect<int>(entity.Id, Is.GreaterThan(0));
        }

        [Test, Combinatorial]
        public void CRUD_sequentialy_N_tiny_entities([Values(1000)] int qtd, [Values("ADO", "EF", "ORMLite")] string impl)
        {
            for (int i = 0; i < qtd; i++)
            {
                var service = new CRUD(CreateImpl(impl));
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

        [Test, Combinatorial]
        public void Create_parallely_N_tiny_entities([Values(1000)] int qtd, [Values("ADO", "EF", "ORMLite")] string impl)
        {
            Parallel.For(0, qtd, (int i) =>
            {
                var service = new CRUD(CreateImpl(impl));
                TinyModel entity = null;

                entity = new TinyModel { Descricao = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "_" + i };
                service.Create(entity);
            });

            //Expect<int>(entity.Id, Is.GreaterThan(0));
        }

        [Test, Combinatorial]
        public void CRUD_parallely_N_tiny_entities([Values(1000)] int qtd, [Values("ADO", "EF", "ORMLite")] string impl)
        {
            Parallel.For(0, qtd, (int i) =>
            {
                var service = new CRUD(CreateImpl(impl));
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
