// Adam Dernis © 2021

using AlgoNet.Tests.Clustering.MeanShift.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoNet.Tests.Clustering.MeanShift
{
    [TestClass]
    public class MSEquivalencyTests
    {
        public const double ACCEPTED_ERROR = .000001;

        [TestMethod]
        public void WeightedEquivalency()
        {
            foreach (var test in DoubleTests.All)
            {
                test.RunWeightedCompare();
            }

            foreach (var test in Vector2Tests.All)
            {
                test.RunWeightedCompare();
            }

            foreach (var test in GradientTests.All)
            {
                test.RunWeightedCompare();
            }
        }
    }
}
