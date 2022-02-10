// Adam Dernis © 2022

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using CS = AlgoNet.Sorting.CountingSort;

namespace AlgoNet.Tests.Sorting.Sorting
{
    [TestClass]
    public class CountingSortTests : SortingTests
    {
        protected override void RunTest(int[] data)
        {
            var list = data.ToList();

            // As array
            var aClone = new int[data.Length];
            Array.Copy(data, aClone, data.Length);
            data = CS.Sort(data, x => x, data.Length);
            Common.VerifySorted(data, aClone);

            // As list
            var lClone = data.ToList();
            var lSorted = CS.Sort(list, x => x, list.Count);
            Common.VerifySorted(lSorted, lClone);
        }
    }
}
