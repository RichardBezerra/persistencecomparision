using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceComparision.Core.Tests
{
    [TestFixture]
    class RepoEFTests : AssertionHelper
    {
        [Test]
        public void CreateEntities()
        {
            var service = new CRUD(new Repo.RepoEF());
            var entity = new TinyModel { Descricao = DateTime.Now.ToLongDateString() };
            service.Create(entity);

            Expect<int>(entity.Id, Is.GreaterThan(0));
        }
    }
}
