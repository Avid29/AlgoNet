// Adam Dernis © 2022

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using IS = AlgoNet.Sorting.InsertionSort;

namespace AlgoNet.Tests.Sorting
{
    [TestClass]
    public class InsertionSortTests : SortingTests
    {
        protected override void RunTest(int[] data)
        {
            var clone = new int[data.Length];
            Array.Copy(data, clone, data.Length);
            IS.Sort(data);
            Common.VerifySorted(data, clone);
        }
    }
}
