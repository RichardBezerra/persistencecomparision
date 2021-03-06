﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceComparision.Core.Tests
{
    [TestFixture]
    public class PersistTinyModelTests : AssertionHelper
    {
        [Test, Combinatorial]
        public void Create_sequentialy_N_tiny_entities([Values(1,10,30)] int qtd, [Values("ADO", "EF", "ORMLite")] string impl)
        {
            var testImpls = new RepoImplsTests();

            for (int i = 0; i < qtd; i++)
                testImpls.Create_TinyModel(impl);
        }

        [Test, Combinatorial]
        public void CRUD_sequentialy_N_tiny_entities([Values(1,10,30)] int qtd, [Values("ADO", "EF", "ORMLite")] string impl)
        {
            var testImpls = new RepoImplsTests();

            for (int i = 0; i < qtd; i++)
                testImpls.CRUD_TinyModel(impl);
        }

        [Test, Combinatorial]
        public void Create_parallely_N_tiny_entities([Values(1,10,30)] int qtd, [Values("ADO", "EF", "ORMLite")] string impl)
        {
            Parallel.For(0, qtd, (int i) =>
            {
                var testImpls = new RepoImplsTests();

                testImpls.Create_TinyModel(impl);
            });
        }

        [Test, Combinatorial]
        public void CRUD_parallely_N_tiny_entities([Values(1,10,30)] int qtd, [Values("ADO", "EF", "ORMLite")] string impl)
        {
            Parallel.For(0, qtd, (int i) =>
            {
                var testImpls = new RepoImplsTests();

                testImpls.CRUD_TinyModel(impl);
            });
        }
    }
}
