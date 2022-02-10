// Adam Dernis © 2022

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CS = AlgoNet.Sorting.CountingSort;

namespace AlgoNet.Tests.Sorting.Sorting
{
    [TestClass]
    public class CountingSortTests : SortingTests
    {
        protected override void RunTest(int[] data)
        {
            var clone = new int[data.Length];
            Array.Copy(data, clone, data.Length);
            data = CS.Sort(data, x => x, data.Length);
            Common.VerifySorted(data, clone);
        }
    }
}
