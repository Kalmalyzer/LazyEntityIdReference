using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LazyEntityIdReference
{
    [TestClass]
    public class LazyReferenceTest
    {
        public class TestClass
        {
            public readonly string Name;

            public TestClass(string name)
            {
                Name = name;
            }
        }

        public static TestClass CreateTestInstance()
        {
            return new TestClass("TestInstance");
        }

        [TestMethod]
        public void TestLazyReference()
        {
            LazyReference<TestClass> lazyTestInstance = new LazyReference<TestClass>(CreateTestInstance);
        }
    }
}
