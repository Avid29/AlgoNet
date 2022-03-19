// Adam Dernis © 2022

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SS = AlgoNet.Sorting.SelectionSort;

namespace AlgoNet.Tests.Sorting.Sorting
{
    [TestClass]
    public class SelectionSortTests : SortingTests
    {
        protected override void RunTest(int[] data)
        {
            var clone = new int[data.Length];
            Array.Copy(data, clone, data.Length);
            SS.Sort(data);
            Common.VerifySorted(data, clone);
        }
    }
}
