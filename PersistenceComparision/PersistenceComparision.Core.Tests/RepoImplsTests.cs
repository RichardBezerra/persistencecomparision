using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;

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
        public void CRUD_TinyModel([Values("ADO", "EF", "ORMLite")] string impl)
        {
            var service = new CRUD(CreateImpl(impl));

            var entity = new TinyModel { Descricao = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() };
            service.Create(entity);

            var r = service.ReadTiny(entity.Id);

            r.Descricao += "_updated";

            service.Update(r);

            service.Delete(r);

            Expect(r.Id, Is.GreaterThan(0).And.EqualTo(entity.Id));
        }

        [Test, Combinatorial]
        public void Create_TinyModel([Values("ADO", "EF", "ORMLite")] string impl)
        {
            var service = new CRUD(CreateImpl(impl));
            
            var entity = new TinyModel { Descricao = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() };
            service.Create(entity);

            Expect(entity.Id, Is.GreaterThan(0));
        }

        [Test, Combinatorial]
        public void CRUD_OneToManyModel([Values("ADO", "EF", "ORMLite")] string impl)
        {
            var service = new CRUD(CreateImpl(impl));

            var many1 = new ManyModel { Many = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() };
            var many2 = new ManyModel { Many = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() };
            var one = new OneModel
            {
                One = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(),
                Many = new List<ManyModel>(new ManyModel[] { many1, many2 })
            };

            CreateImpl(impl).Create(one);

            var r = service.ReadOne(one.Id);

            Expect(r.Many.Select(m => m.Id), EquivalentTo(one.Many.Select(m => m.Id)));

            r.One += "_updated";
            r.Many.ForEach((m) => { m.Many += "_update"; });

            service.Update(r);

            service.Delete(r);

            Expect(r.Id, Is.GreaterThan(0).And.EqualTo(one.Id));
        }

        [Test, Combinatorial]
        public void Create_OneToManyModel([Values("ADO", "EF", "ORMLite")] string impl)
        {
            var service = new CRUD(CreateImpl(impl));

            var many1 = new ManyModel { Many = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() };
            var many2 = new ManyModel { Many = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() };
            var one = new OneModel
            {
                One = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(),
                Many = new List<ManyModel>(new ManyModel[] { many1, many2 })
            };

            CreateImpl(impl).Create(one);

            Expect(one.Id, Is.GreaterThan(0));
        }
    }
}
