// Adam Dernis © 2022

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SS = AlgoNet.Sorting.SelectionSort;

namespace AlgoNet.Tests.Sorting.Sorting
{
    [TestClass]
    public class SelectionSortTests : SortingTests
    {
        protected override void RunTest(int[] data)
        {
            SS.Sort(data);
            Common.VerifySorted(data);
        }
    }
}
