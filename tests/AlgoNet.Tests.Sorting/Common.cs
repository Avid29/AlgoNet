// Adam Dernis © 2022

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgoNet.Tests.Sorting
{
    internal class Common
    {
        internal static void VerifySorted<T>(T[] sortedArray)
            where T : IComparable
        {
            for (int i = 0; i < sortedArray.Length - 1; i++)
            {
                Assert.IsTrue(sortedArray[i].CompareTo(sortedArray[i + 1]) <= 0);
            }
        }

        internal static bool AreEquivilent<T>(T[] data, T[] clone)
            where T : IEquatable<T>
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (!data[i].Equals(clone[i])) return false;
            }
            return true;
        }
    }
}
