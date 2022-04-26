// Adam Dernis © 2022

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using HS = AlgoNet.Sorting.HeapSort;

namespace AlgoNet.Tests.Sorting.Sorting
{
    [TestClass]
    public class HeapSortTests : SortingTests
    {
        protected override void RunTest(int[] data)
        {
            // As array
            var aClone = new int[data.Length];
            Array.Copy(data, aClone, data.Length);
            HS.Sort(data);
            Common.VerifySorted(data, aClone);
        }
    }
}
