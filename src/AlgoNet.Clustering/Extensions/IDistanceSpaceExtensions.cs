// Adam Dernis © 2022

namespace AlgoNet.Clustering
{
    /// <summary>
    /// A class containing extensions for the <see cref="IDistanceSpace{T}"/> interface.
    /// </summary>
    public static class IDistanceSpaceExtensions
    {
        public static bool AreEqual<T>(this IDistanceSpace<T> space, T it1, T i2)
        {
            return space.FindDistanceSquared(it1, i2) == 0;
        }

        public static bool AreEqual<T>(this IDistanceSpace<T> space, T it1, T i2, double error)
        {
            return space.FindDistanceSquared(it1, i2) < error;
        }
    }
}
