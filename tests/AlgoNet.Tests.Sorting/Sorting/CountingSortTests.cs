// Adam Dernis © 2022

using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS = AlgoNet.Sorting.CountingSort;

namespace AlgoNet.Tests.Sorting.Sorting
{
    [TestClass]
    public class CountingSortTests : SortingTests
    {
        protected override void RunTest(int[] data)
        {
            data = CS.Sort(data, x => x, data.Length);
            Common.VerifySorted(data);
        }
    }
}
