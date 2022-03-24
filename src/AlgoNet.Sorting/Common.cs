// Adam Dernis © 2022

using System.Runtime.CompilerServices;

namespace AlgoNet.Sorting
{
    /// <summary>
    /// A static class containing common operations between sorting methods.
    /// </summary>
    internal static class Common
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Swap<T>(ref T a, ref T b) => (b, a) = (a, b);
    }
}
