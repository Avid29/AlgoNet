// Adam Dernis © 2022

using AlgoNet.Tests.Clustering.MeanShift.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoNet.Tests.Clustering.MeanShift
{
    [TestClass]
    public class MSEquivalencyTests
    {
        public const double ACCEPTED_ERROR = .000001;

        [TestMethod]
        public void WeightedEquivalencyExplicit()
        {
            foreach (var test in ExplicitTests.All)
            {
                test.RunWeightedCompare();
            }
        }

        [TestMethod]
        public void WeightedEquivalencyGradient()
        {
            foreach (var test in GradientTests.All)
            {
                test.RunWeightedCompare();
            }
        }
    }
}
