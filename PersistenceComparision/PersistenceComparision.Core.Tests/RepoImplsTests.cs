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
        private static LargeModel CreateLargeModel()
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
            return entity;
        }

        private static Repo<TinyModel> CreateTinyModelImpl(string impl)
        {
            if (impl.Equals("EF"))
                return new Repo.EF.RepoTinyEF();
            else if (impl.Equals("ORMLite"))
                return new Repo.ORMLite.RepoTinyORMLite();
            else
                return new Repo.ADO.RepoTinyADO();
        }

        private static Repo<OneModel> CreateOneModelImpl(string impl)
        {
            if (impl.Equals("EF"))
                return new Repo.EF.RepoOneToManyEF();
            else if (impl.Equals("ORMLite"))
                return new Repo.ORMLite.RepoOneToManyORMLite();
            else
                return new Repo.ADO.RepoOneToManyADO();
        }

        private static Repo<LargeModel> CreateLargeModelImpl(string impl)
        {
            if (impl.Equals("EF"))
                return new Repo.EF.RepoLargeEF();
            else if (impl.Equals("ORMLite"))
                return new Repo.ORMLite.RepoLargeORMLite();
            else
                return new Repo.ADO.RepoLargeADO();
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
            LargeModel entity = CreateLargeModel();

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

        [Test, Combinatorial]
        public void CRUD_LargeModel([Values("EF", "ORMLite", "ADO")] string impl)
        {
            var repo = CreateLargeModelImpl(impl);

            var largeModel = CreateLargeModel();

            repo.Create(largeModel);

            var r = repo.FindById(largeModel.Id);

            r.Large += "_updated_" + impl;
            r.LargeDescription2 += "_updated_" + impl;
            r.LargeDescription3 += "_updated_" + impl;
            r.LargeDescription4 += "_updated_" + impl;
            r.LargeDescription5 += "_updated_" + impl;
            r.LargeDescription6 += "_updated_" + impl;
            r.LargeDescription7 += "_updated_" + impl;
            r.LargeDescription8 += "_updated_" + impl;
            r.LargeDescription9 += "_updated_" + impl;
            r.LargeDescription10 += "_updated_" + impl;
            r.LargeDescription11 += "_updated_" + impl;
            r.LargeDescription12 += "_updated_" + impl;
            r.LargeDescription13 += "_updated_" + impl;
            r.LargeDescription14 += "_updated_" + impl;
            r.LargeDescription15 += "_updated_" + impl;
            r.LargeDescription16 += "_updated_" + impl;
            r.LargeDescription17 += "_updated_" + impl;
            r.LargeDescription18 += "_updated_" + impl;
            r.LargeDescription19 += "_updated_" + impl;
            r.LargeDescription20 += "_updated_" + impl;

            repo.Update(r);

            repo.Delete(r);

            Expect(r.Id, Is.GreaterThan(0).And.EqualTo(largeModel.Id));
        }

    }
}
