using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LazyEntityIdReference
{
    [TestClass]
    public class LazyEntityIdReferenceTest
    {
        public class TestEntityA
        {
            public readonly string Name;

            public TestEntityA(string name)
            {
                Name = name;
            }
        }

        public class TestEntityB
        {
            public readonly string Name;

            private readonly LazyEntityIdReference<TestEntityA> relatedEntity;
            public TestEntityA RelatedEntity { get { return relatedEntity.Get(); } }

            public TestEntityB(string name, EntityIdRegistry<TestEntityA> entityIdRegistry, int idA)
            {
                Name = name;
                relatedEntity = new LazyEntityIdReference<TestEntityA>(entityIdRegistry, idA);
            }
        }

        [TestMethod]
        public void TestLazyEntityIdReference()
        {
            EntityIdRegistry<TestEntityA> testEntityARegistry = new EntityIdRegistry<TestEntityA>();
            EntityIdRegistry<TestEntityB> testEntityBRegistry = new EntityIdRegistry<TestEntityB>();

            TestEntityA testEntityA0 = new TestEntityA("TestEntityA0");
            TestEntityA testEntityA1 = new TestEntityA("TestEntityA1");
            TestEntityA testEntityA2 = new TestEntityA("TestEntityA2");

            int testEntityA0Id = testEntityARegistry.Add(testEntityA0);
            int testEntityA1Id = testEntityARegistry.Add(testEntityA1);
            int testEntityA2Id = testEntityARegistry.Add(testEntityA2);

            TestEntityB testEntityB0 = new TestEntityB("TestEntityB0", testEntityARegistry, testEntityARegistry.ToId(testEntityA1));
            TestEntityB testEntityB1 = new TestEntityB("TestEntityB1", testEntityARegistry, testEntityARegistry.ToId(testEntityA2));
            TestEntityB testEntityB2 = new TestEntityB("TestEntityB2", testEntityARegistry, 0);

            int testEntityB0Id = testEntityBRegistry.Add(testEntityB0);
            int testEntityB1Id = testEntityBRegistry.Add(testEntityB1);
            int testEntityB2Id = testEntityBRegistry.Add(testEntityB2);

            Assert.AreEqual("TestEntityB0", testEntityB0.Name);
            Assert.IsNotNull(testEntityB0.RelatedEntity);
            Assert.AreEqual("TestEntityA1", testEntityB0.RelatedEntity.Name);
            Assert.AreEqual("TestEntityB2", testEntityB2.Name);
            Assert.IsNull(testEntityB2.RelatedEntity);
        }
    }
}
