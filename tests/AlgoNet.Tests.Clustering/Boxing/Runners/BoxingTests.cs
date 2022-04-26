// Adam Dernis © 2022

using AlgoNet.Tests.Clustering.Boxing.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoNet.Tests.Clustering.Boxing.Runners
{
    [TestClass]
    public class BoxingTests
    {
        [TestMethod]
        public void Explicit()
        {
            foreach (var test in ExplicitTests.All)
            {
                TestRunner.RunTest(test);
            }
        }

        [TestMethod]
        public void Gradient()
        {
            foreach (var test in GradientTests.All)
            {
                TestRunner.RunTest(test);
            }
        }
    }
}
