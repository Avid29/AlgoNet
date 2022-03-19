// Adam Dernis © 2022

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using QS = AlgoNet.Sorting.QuickSort;

namespace AlgoNet.Tests.Sorting.Methods.QuickSort
{
    [TestClass]
    public class QuickSortTests : SortingTests
    {
        protected override void RunTest(int[] data)
        {
            var list = data.ToList();

            // As array
            var clone = new int[data.Length];
            Array.Copy(data, clone, data.Length);
            QS.Sort(data);
            Common.VerifySorted(data, clone);
        }
    }
}
