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
        /// <summary>
        /// Clusters a set of points using Boxing cluster.
        /// </summary>
        /// <typeparam name="T">The type of points to cluster.</typeparam>
        /// <typeparam name="TShape">The type of shape to use on the points to cluster.</typeparam>
        /// <param name="points">The set of points to cluster.</param>
        /// <param name="window">The size of the cells.</param>
        /// <param name="shape">The shape to use on the points to cluster.</param>
        /// <returns>An array of clusters.</returns>
        public static (T, int)[] Cluster<T, TShape>(T[] points, double window, TShape shape = default)
            where T : unmanaged
            where TShape : struct, IGridSpace<T, T>, IAverageSpace<T>
        {
            Dictionary<T, List<T>> cells = new Dictionary<T, List<T>>();
            foreach (var point in points)
            {
                T cell = shape.GetCell(point, window);
                if (!cells.ContainsKey(cell))
                {
                    cells.Add(cell, new List<T>());
                }

                cells[cell].Add(point);
            }

            (T, int)[] clusters = new (T, int)[cells.Count];
            int i = 0;
            foreach (var segment in cells.Values)
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
