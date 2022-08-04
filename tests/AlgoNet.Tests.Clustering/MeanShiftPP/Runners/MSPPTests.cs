// Adam Dernis © 2022

using AlgoNet.Tests.Clustering.MeanShiftPP.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoNet.Tests.Clustering.MeanShiftPP
{
    [TestClass]
    public class MSPPTests
    {
        [TestMethod]
        public void Explict()
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
