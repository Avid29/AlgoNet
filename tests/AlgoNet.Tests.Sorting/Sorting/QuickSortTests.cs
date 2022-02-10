// Adam Dernis © 2022

using Microsoft.VisualStudio.TestTools.UnitTesting;
using QS = AlgoNet.Sorting.QuickSort;

namespace AlgoNet.Tests.Sorting.Methods
{
    [TestClass]
    public class QuickSortTests : SortingTests
    {
        protected override void RunTest(int[] data)
        {
            QS.Sort(data);
            Common.VerifySorted(data);
        }
    }
}
