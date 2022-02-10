// Adam Dernis © 2022

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MES = AlgoNet.Sorting.MergeSort;

namespace AlgoNet.Tests.Sorting.Sorting
{
    [TestClass]
    public class MergeSortTests : SortingTests
    {
        protected override void RunTest(int[] data)
        {
            var clone = new int[data.Length];
            Array.Copy(data, clone, data.Length);
            MES.Sort(data);
            Common.VerifySorted(data, clone);
        }
    }
}
