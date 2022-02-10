// Adam Dernis © 2022

using Microsoft.VisualStudio.TestTools.UnitTesting;
using IS = AlgoNet.Sorting.InsertionSort;

namespace AlgoNet.Tests.Sorting
{
    [TestClass]
    public class InsertionSortTests : SortingTests
    {
        protected override void RunTest(int[] data)
        {
            IS.Sort(data);
            Common.VerifySorted(data);
        }
    }
}
