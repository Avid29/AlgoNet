// Adam Dernis © 2022

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using QS = AlgoNet.Sorting.QuickSort;

namespace AlgoNet.Tests.Sorting.QuickSort
{
    [TestClass]
    public class QuickSortAsyncTests : SortingTests
    {
        protected override async void RunTest(int[] data)
        {
            var list = data.ToList();

            // As array
            var clone = new int[data.Length];
            Array.Copy(data, clone, data.Length);
            await QS.SortAsync(data);
            Common.VerifySorted(data, clone);
        }
    }
}
