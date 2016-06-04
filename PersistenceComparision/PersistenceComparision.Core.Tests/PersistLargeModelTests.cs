using NUnit.Framework;
using System.Threading.Tasks;

namespace PersistenceComparision.Core.Tests
{
    [TestFixture]
    public class PersistLargeModelTests : AssertionHelper
    {
        [Test, Combinatorial]
        public void Create_sequentialy_N_large_entities([Values(100)] int qtd, [Values("ADO", "EF", "ORMLite")] string impl)
        {
            var testImpls = new RepoImplsTests();

            for (int i = 0; i < qtd; i++)
                testImpls.Create_LargeModel(impl);
        }

        [Test, Combinatorial]
        public void CRUD_sequentialy_N_large_entities([Values(100)] int qtd, [Values("ADO", "EF", "ORMLite")] string impl)
        {
            var testImpls = new RepoImplsTests();

            for (int i = 0; i < qtd; i++)
                testImpls.CRUD_LargeModel(impl);
        }

        [Test, Combinatorial]
        public void Create_parallely_N_large_entities([Values(100)] int qtd, [Values("ADO", "EF", "ORMLite")] string impl)
        {
            Parallel.For(0, qtd, (int i) =>
            {
                var testImpls = new RepoImplsTests();

                testImpls.Create_LargeModel(impl);
            });
        }

        [Test, Combinatorial]
        public void CRUD_parallely_N_large_entities([Values(100)] int qtd, [Values("ADO", "EF", "ORMLite")] string impl)
        {
            Parallel.For(0, qtd, (int i) =>
            {
                var testImpls = new RepoImplsTests();

                testImpls.CRUD_LargeModel(impl);
            });
        }
    }
}
