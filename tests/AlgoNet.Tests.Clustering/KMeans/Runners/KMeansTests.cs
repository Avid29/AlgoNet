// Adam Dernis © 2022

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoNet.Tests.Clustering.KMeans
{
    [TestClass]
    public class KMeansTests
    {
        [TestMethod]
        public void Explicit()
        {
            foreach (var test in ExplicitTests.All)
            {
                test.Run();
            }
        }

        [TestMethod]
        public void Gradient()
        {
            foreach (var test in GradientTests.All)
            {
                test.Run();
            }
        }
    }
}
