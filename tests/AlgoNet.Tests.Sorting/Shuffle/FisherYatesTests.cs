// Adam Dernis © 2022

using AlgoNet.Mathematics;
using AlgoNet.Sorting.Shuffle;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace AlgoNet.Tests.Sorting.Shuffle
{
    [TestClass]
    public class FisherYatesTests
    {
        private const int SAMPLES = 1_000;

        private static void RunTest(int[] data)
        {
            int[] clone = new int[data.Length];
            Array.Copy(data, clone, data.Length);

            double[] distirbution = new double[data.Length];

            for (int i = 0; i < SAMPLES; i++)
            {
                FisherYates.Shuffle(data, i);

                for (int j = 0; j < data.Length; j++)
                    distirbution[j] += data[j];
            }

            double stdDev = Statistics.StandardDeviation(distirbution);

            CollectionAssert.AreNotEqual(data, clone);
        }

        [TestMethod]
        [DataRow(10)]
        [DataRow(100)]
        public void Test(int size)
        {
            RunTest(Enumerable.Range(0, size).ToArray());
        }
    }
}
