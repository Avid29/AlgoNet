// Adam Dernis © 2022

using AlgoNet.Tests.Clustering.MeanShiftPP.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoNet.Tests.Clustering.MeanShiftPP
{
    [TestClass]
    public class MSPPEquivalencyTests
    {
        public const double ACCEPTED_ERROR = .000001;

        [TestMethod]
        public void StandardEquivalencyExplicit()
        {
            foreach (var test in ExplicitTests.All)
            {
                test.RunStandardCompare();
            }
        }

        [TestMethod]
        public void StandardEquivalencyGradient()
        {
            foreach (var test in GradientTests.All)
            {
                test.RunStandardCompare();
            }
        }
    }
}
