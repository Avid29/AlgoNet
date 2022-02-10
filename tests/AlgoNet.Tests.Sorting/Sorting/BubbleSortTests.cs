// Adam Dernis © 2022

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BS = AlgoNet.Sorting.BubbleSort;

namespace AlgoNet.Tests.Sorting
{
    [TestClass]
    public class BubbleSortTests : SortingTests
    {
        protected override void RunTest(int[] data)
        {
            var clone = new int[data.Length];
            Array.Copy(data, clone, data.Length);
            BS.Sort(data);
            Common.VerifySorted(data, clone);
        }
    }
}
