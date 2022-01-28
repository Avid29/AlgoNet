// Adam Dernis © 2022

using System;

namespace AlgoNet.Sorting.Sorting
{
    /// <summary>
    /// A static class containing merge sort methods.
    /// </summary>
    public static class MergeSort
    {
        public static void Sort<T>(T[] array) where T : IComparable => Sort(array.AsSpan());

        public static void Sort<T>(Span<T> array)
            where T : IComparable
        {
            Span<T> buffer = new T[array.Length];
            SplitAndMerge(array, buffer);
        }

        private static Span<T> SplitAndMerge<T>(Span<T> a, Span<T> b)
            where T : IComparable
        {
            if (a.Length <= 1)
            {
                b[0] = a[0];
                return b;
            }

            int center = a.Length / 2;
            Span<T> left = SplitAndMerge(a.Slice(0, center), b);
            Span<T> right = SplitAndMerge(a.Slice(center + 1), b);

            int lPos = 0;
            int rPos = 0;
            for (int i = 0; i < b.Length; i++)
            {
                if (lPos == left.Length)
                {
                    right.Slice(rPos).CopyTo(b.Slice(i));
                    break;
                }
                else if (rPos == right.Length)
                {
                    left.Slice(lPos).CopyTo(b.Slice(i));
                    break;
                }
                else
                {
                    bool useLeft = left[lPos].CompareTo(right[rPos]) <= 0;

                    ref int usePos = ref lPos;
                    ref Span<T> source = ref left;
                    if (!useLeft)
                    {
                        usePos = ref rPos;
                        source = ref right;
                    }

                    b[i] = source[usePos];
                    usePos++;
                }
            }
            return b;
        }
    }
}
