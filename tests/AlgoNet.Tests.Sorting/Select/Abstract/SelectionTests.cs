// Adam Dernis © 2022

using AlgoNet.Tests.Sorting.DataSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoNet.Tests.Sorting.Select.Abstract
{
    public abstract class SelectionTests
    {
        protected abstract void RunTest(int[] data, int k);

        [TestMethod]
        public void PresortedTests()
        {
            foreach (var set in PresortedSets.All)
            {
                RunTest(set, set.Length / 4);
                RunTest(set, set.Length / 2);
                RunTest(set, 3 * set.Length / 4);
            }
        }

        [TestMethod]
        public void ReverseOrderTests()
        {
            foreach (var set in ReverseOrderSets.All)
            {
                RunTest(set, set.Length / 4);
                RunTest(set, set.Length / 2);
                RunTest(set, 3 * set.Length / 4);
            }
        }

        [TestMethod]
        public void RandomizedTests()
        {
            foreach (var set in RandomizedSets.All)
            {
                RunTest(set, set.Length / 4);
                RunTest(set, set.Length / 2);
                RunTest(set, 3 * set.Length / 4);
            }
        }
    }
}
