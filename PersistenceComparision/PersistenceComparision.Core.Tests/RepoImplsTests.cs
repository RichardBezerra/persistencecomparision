using NUnit.Framework;
using System;

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

            var r = service.Read(entity.Id);

            r.Descricao += "_updated";

            service.Update(r);

            service.Delete(r);

            Expect(entity.Id, Is.GreaterThan(0));
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
           
        }
    }
}
