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

            private readonly LazyEntityIdReference<TestEntityA> relatedReadOnlyEntity;
            public TestEntityA RelatedReadOnlyEntity { get { return relatedReadOnlyEntity.Get(); } }

            private LazyEntityIdReference<TestEntityA> relatedChangableEntity;
            public TestEntityA RelatedChangableEntity { get { return relatedChangableEntity.Get(); }  set { relatedChangableEntity.Set(value); } }


            public TestEntityB(string name, EntityIdRegistry<TestEntityA> entityIdRegistry, int idReadOnlyEntity, int idChangableEntity)
            {
                Name = name;
                relatedReadOnlyEntity = new LazyEntityIdReference<TestEntityA>(entityIdRegistry, idReadOnlyEntity);
                relatedChangableEntity = new LazyEntityIdReference<TestEntityA>(entityIdRegistry, idChangableEntity);
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

            TestEntityB testEntityB0 = new TestEntityB("TestEntityB0", testEntityARegistry, testEntityARegistry.ToId(testEntityA1), testEntityARegistry.ToId(testEntityA2));
            TestEntityB testEntityB1 = new TestEntityB("TestEntityB1", testEntityARegistry, testEntityARegistry.ToId(testEntityA2), testEntityARegistry.ToId(testEntityA2));
            TestEntityB testEntityB2 = new TestEntityB("TestEntityB2", testEntityARegistry, 0, 0);

            int testEntityB0Id = testEntityBRegistry.Add(testEntityB0);
            int testEntityB1Id = testEntityBRegistry.Add(testEntityB1);
            int testEntityB2Id = testEntityBRegistry.Add(testEntityB2);

            Assert.AreEqual("TestEntityB0", testEntityB0.Name);
            Assert.IsNotNull(testEntityB0.RelatedReadOnlyEntity);
            Assert.AreEqual("TestEntityA1", testEntityB0.RelatedReadOnlyEntity.Name);
            Assert.IsNotNull(testEntityB0.RelatedChangableEntity);
            Assert.AreEqual("TestEntityA2", testEntityB0.RelatedChangableEntity.Name);
            Assert.AreEqual("TestEntityB2", testEntityB2.Name);
            Assert.IsNull(testEntityB2.RelatedReadOnlyEntity);

            testEntityB0.RelatedChangableEntity = testEntityA0;
            Assert.AreEqual("TestEntityA0", testEntityB0.RelatedChangableEntity.Name);
        }
    }
}
