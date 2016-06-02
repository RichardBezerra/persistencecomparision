using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PersistenceComparision.Core.Tests
{
    [TestFixture]
    class RepoImplsTests : AssertionHelper
    {
        private static Repo.Repo<TinyModel> CreateTinyModelImpl(string impl)
        {
            if (impl.Equals("EF"))
                return new Repo.RepoTinyEF();
            else if (impl.Equals("ORMLite"))
                return new Repo.RepoTinyORMLite();
            else
                return new Repo.RepoTinyADO();
        }

        private static Repo.Repo<OneModel> CreateOneModelImpl(string impl)
        {
            if (impl.Equals("EF"))
                return new Repo.RepoOneToManyEF();
            else if (impl.Equals("ORMLite"))
                return new Repo.RepoOneToManyORMLite();
            else
                return new Repo.RepoOneToManyADO();
        }

        [Test, Combinatorial]
        public void Create_TinyModel([Values("EF", "ORMLite", "ADO")] string impl)
        {
            var entity = new TinyModel { Descricao = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() };

            CreateTinyModelImpl(impl).Create(entity);

            Expect(entity.Id, Is.GreaterThan(0));
        }

        [Test, Combinatorial]
        public void Create_OneToManyModel([Values("EF", "ORMLite", "ADO")] string impl)
        {
            var many1 = new ManyModel { Many = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() };
            var many2 = new ManyModel { Many = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() };
            var one = new OneModel
            {
                One = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(),
                Many = new List<ManyModel>(new ManyModel[] { many1, many2 })
            };

            CreateOneModelImpl(impl).Create(one);

            Expect(one.Id, Is.GreaterThan(0));
        }

        [Test, Combinatorial]
        public void CRUD_TinyModel([Values("EF", "ORMLite", "ADO")] string impl)
        {
            var repo = CreateTinyModelImpl(impl);

            var entity = new TinyModel { Descricao = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() };
            repo.Create(entity);

            var r = repo.FindById(entity.Id);

            r.Descricao += "_updated";

            repo.Update(r);

            repo.Delete(r);

            Expect(r.Id, Is.GreaterThan(0).And.EqualTo(entity.Id));
        }

        [Test, Combinatorial]
        public void CRUD_OneToManyModel([Values("EF", "ORMLite", "ADO")] string impl)
        {
            var repo = CreateOneModelImpl(impl);

            var many1 = new ManyModel { Many = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() };
            var many2 = new ManyModel { Many = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() };
            var one = new OneModel
            {
                One = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(),
                Many = new List<ManyModel>(new ManyModel[] { many1, many2 })
            };

            repo.Create(one);

            var r = repo.FindById(one.Id);

            Expect(r.Many.Select(m => m.Id), EquivalentTo(one.Many.Select(m => m.Id)));

            r.One += "_updated_" + impl;
            r.Many.ForEach((m) => { m.Many += "_update_" + impl; });

            repo.Update(r);

            repo.Delete(r);

            Expect(r.Id, Is.GreaterThan(0).And.EqualTo(one.Id));
        }
    }
}
