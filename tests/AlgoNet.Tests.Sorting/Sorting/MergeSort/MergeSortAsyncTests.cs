// Adam Dernis © 2022

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using MES = AlgoNet.Sorting.MergeSort;

namespace AlgoNet.Tests.Sorting.Sorting
{
    [TestClass]
    public class MergeSortAsyncTests : SortingTests
    {
        protected override async void RunTest(int[] data)
        {
            var list = data.ToList();

            // As array
            var aClone = new int[data.Length];
            Array.Copy(data, aClone, data.Length);
            await MES.SortAsync(data);
            Common.VerifySorted(data, aClone);
        }
    }
}
