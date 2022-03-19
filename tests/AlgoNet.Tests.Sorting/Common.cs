// Adam Dernis © 2022

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoNet.Tests.Sorting
{
    internal class Common
    {
        internal static void VerifySorted<T>(T[] sorted, T[] raw)
            where T : IComparable<T>, IEquatable<T>
        {
            Array.Sort(raw);
            CollectionAssert.AreEqual(raw, sorted);
        }

        internal static void VerifySorted<T>(IList<T> sorted, IList<T> raw)
            where T : IComparable<T>, IEquatable<T>
        {
            raw = raw.OrderBy(x => x).ToList();
            Assert.IsTrue(AreEquivilent(raw, sorted));
        }

        internal static void VerifyPartition<T>(T[] partitionedArray, int k, T kth)
            where T : IComparable<T>
        {
            for (int i = 0; i < k; i++)
                Assert.IsTrue(partitionedArray[i].CompareTo(kth) <= 0);

            for (int i = k + 1; i < partitionedArray.Length; i++)
                Assert.IsTrue(partitionedArray[i].CompareTo(kth) >= 0);
        }

        internal static bool AreEquivilent<T>(IList<T> data, IList<T> clone)
            where T : IEquatable<T>
        {
            for (int i = 0; i < data.Count; i++)
            {
                if (!data[i].Equals(clone[i])) return false;
            }
            return true;
        }
    }
}
