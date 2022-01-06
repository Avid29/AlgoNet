// Adam Dernis © 2022

namespace AlgoNet.Sorting
{
    /// <summary>
    /// A static class containing common operations between sorting methods.
    /// </summary>
    internal static class Common
    {
        internal static void Swap<T>(ref T a, ref T b)
        {
            T c = a;
            a = b;
            b = c;
        }
    }
}
