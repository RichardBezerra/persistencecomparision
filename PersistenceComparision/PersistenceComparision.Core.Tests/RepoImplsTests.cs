using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PersistenceComparision.Core.Tests
{
    [TestFixture]
    class RepoImplsTests : AssertionHelper
    {
        private static Repo<TinyModel> CreateTinyModelImpl(string impl)
        {
            if (impl.Equals("EF"))
                return new Repo.RepoTinyEF();
            else if (impl.Equals("ORMLite"))
                return new Repo.RepoTinyORMLite();
            else
                return new Repo.RepoTinyADO();
        }

        private static Repo<OneModel> CreateOneModelImpl(string impl)
        {
            if (impl.Equals("EF"))
                return new Repo.RepoOneToManyEF();
            else if (impl.Equals("ORMLite"))
                return new Repo.RepoOneToManyORMLite();
            else
                return new Repo.RepoOneToManyADO();
        }

        private static Repo<LargeModel> CreateLargeModelImpl(string impl)
        {
            if (impl.Equals("EF"))
                return new Repo.RepoLargeEF();
            //else if (impl.Equals("ORMLite"))
            //    return new Repo.RepoOneToManyORMLite();
            else
                return new Repo.RepoLargeADO();

            return null;
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
        public void Create_LargeModel([Values("EF", "ORMLite", "ADO")] string impl)
        {
            var sb = new StringBuilder();

            for (int i = 1; i <= 10; i++)
            {
                char t = (char)(64 + i);
                sb.Append(t, i);
                sb.Append(" ");
            }

            var entity = new LargeModel { Large = sb.ToString() };
            entity.LargeDescription2 = entity.Large + entity.Large;
            entity.LargeDescription3 = entity.Large + entity.LargeDescription2;
            entity.LargeDescription4 = entity.LargeDescription2 + entity.LargeDescription3;
            entity.LargeDescription5 = entity.LargeDescription3 + entity.LargeDescription4;
            entity.LargeDescription6 = entity.LargeDescription4 + entity.LargeDescription5;
            entity.LargeDescription7 = entity.LargeDescription5 + entity.LargeDescription6;
            entity.LargeDescription8 = entity.LargeDescription6 + entity.LargeDescription7;
            entity.LargeDescription9 = entity.LargeDescription7 + entity.LargeDescription8;
            entity.LargeDescription10 = entity.LargeDescription8 + entity.LargeDescription9;
            entity.LargeDescription11 = entity.LargeDescription9 + entity.LargeDescription10;
            entity.LargeDescription12 = entity.LargeDescription10 + entity.LargeDescription11;
            entity.LargeDescription13 = entity.LargeDescription11 + entity.LargeDescription12;
            entity.LargeDescription14 = entity.LargeDescription12 + entity.LargeDescription13;
            entity.LargeDescription15 = entity.LargeDescription13 + entity.LargeDescription14;
            entity.LargeDescription16 = entity.LargeDescription14 + entity.LargeDescription15;
            entity.LargeDescription17 = entity.LargeDescription15 + entity.LargeDescription16;
            entity.LargeDescription18 = entity.LargeDescription16 + entity.LargeDescription17;
            entity.LargeDescription19 = entity.LargeDescription17 + entity.LargeDescription18;
            entity.LargeDescription20 = entity.LargeDescription18 + entity.LargeDescription19;

            CreateLargeModelImpl(impl).Create(entity);

            Expect(entity.Id, Is.GreaterThan(0));
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
