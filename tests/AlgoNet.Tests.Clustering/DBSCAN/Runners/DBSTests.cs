// Adam Dernis © 2022

using AlgoNet.Tests.Clustering.DBSCAN.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoNet.Tests.Clustering.DBSCAN.Runners
{
    [TestClass]
    public class DBSTests
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
