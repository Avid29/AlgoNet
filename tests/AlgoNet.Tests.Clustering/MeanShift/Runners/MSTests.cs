﻿// Adam Dernis © 2022

using AlgoNet.Tests.Clustering.MeanShift.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoNet.Tests.Clustering.MeanShift
{
    [TestClass]
    public class MSTests
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
