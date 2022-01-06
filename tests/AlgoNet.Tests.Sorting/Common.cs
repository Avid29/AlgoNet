// Adam Dernis © 2022

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoNet.Tests.Sorting
{
    internal class Common
    {
        internal static void VerifySorted(int[] sortedArray)
        {
            for (int i = 0; i < sortedArray.Length - 1; i++)
            {
                Assert.IsTrue(sortedArray[i] <= sortedArray[i + 1]);
            }
        }
    }
}
