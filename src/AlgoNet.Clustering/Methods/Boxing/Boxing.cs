// Adam Dernis © 2022

using System.Collections.Generic;

namespace AlgoNet.Clustering
{
    /// Boxing, or Box Clustering, is my name for this algorithm since I couldn't find one.
    /// Box Clustering is an extremely naive grid based clustering method.
    /// 
    /// The area is split into a grid of evenly sized segments. The average of all points in each segment is taken.
    /// The resulting average of each segment is treated as a cluster.
    /// 
    /// For example, let's imagine this grid:
    /// + - - - - - - - - - - - +
    /// |     x     x           |
    /// |          x x          |
    /// | x         x     x     |
    /// |                       |
    /// | x       x             |
    /// |                       |
    /// |     x     x     x x x |
    /// |                       |
    /// |    x    xxx           |
    /// |         xxx           |
    /// |    x    xxx           |
    /// + - - - - - - - - - - - +
    /// 
    /// The grid could then be split into segments like this:
    /// + - - - + - - - + - - - +
    /// |     x |   x   |       |
    /// |       |  x x  |       |
    /// | x     |   x   | x     |
    /// + - - - + - - - + - - - +
    /// | x     | x     |       |
    /// |       |       |       |
    /// |     x |   x   | x x x |
    /// + - - - + - - - + - - - +
    /// |    x  | xxx   |       |
    /// |       | xxx   |       |
    /// |    x  | xxx   |       |
    /// + - - - + - - - + - - - +
    /// 
    /// Which could be clustered into a single value per segment, with a weight of the number of clusters:
    /// + - - - + - - - + - - - +
    /// |       |       |       |
    /// |   2   |   4   |       |
    /// |       |       | 1     |
    /// + - - - + - - - + - - - +
    /// |       |       |       |
    /// |   2   |  2    |       |
    /// |       |       |   3   |
    /// + - - - + - - - + - - - +
    /// |       |       |       |
    /// |    2  |  9    |       |
    /// |       |       |       |
    /// + - - - + - - - + - - - +


    /// <summary>
    /// A static class containing Box Clustering methods.
    /// </summary>
    public class Boxing
    {
        public static (T, int)[] Cluster<T, TShape>(T[] points, double window, TShape shape = default)
            where T : unmanaged
            where TShape : struct, IRoundableSpace<T>, IAverageSpace<T>
        {
            Dictionary<T, List<T>> segments = new Dictionary<T, List<T>>();
            foreach (var point in points)
            {
                T segment = shape.Round(point, window);
                if (!segments.ContainsKey(segment))
                {
                    segments.Add(segment, new List<T>());
                }

                segments[segment].Add(point);
            }

            (T, int)[] clusters = new (T, int)[segments.Count];
            int i = 0;
            foreach (var segment in segments.Values)
            {
                T centroid = shape.Average(segment.ToArray());
                int weight = segment.Count;
                clusters[i] = (centroid, weight);
                i++;
            }

            return clusters;
        }
    }
}
