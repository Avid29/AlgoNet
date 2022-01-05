// Adam Dernis © 2022

namespace AlgoNet.Clustering.Shapes
{
    /// <summary>
    /// A shape for wrapping in an <see cref="int"/> weighted point list to cluster in a metric space. 
    /// </summary>
    /// <typeparam name="T">The type of </typeparam>
    /// <typeparam name="TShape">The type of the child shape for <typeparamref name="T"/>.</typeparam>
    internal struct GenericWeightedShape<T, TShape> : IMetricPoint<(T, int)>
        where T : unmanaged
        where TShape : struct, IMetricPoint<T>
    {
        private readonly TShape _shape;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericWeightedShape{T, TShape}"/> struct.
        /// </summary>
        /// <param name="shape">The child shape for <typeparamref name="T"/>.</param>
        public GenericWeightedShape(TShape shape)
        {
            _shape = shape;
        }

        /// <inheritdoc/>
        public bool AreEqual((T, int) it1, (T, int) it2)
        {
            return _shape.AreEqual(it1.Item1, it2.Item1);
        }

        /// <inheritdoc/>
        public bool AreEqual((T, int) it1, (T, int) it2, double error)
        {
            return _shape.AreEqual(it1.Item1, it2.Item1, error);
        }

        /// <inheritdoc/>
        public double FindDistanceSquared((T, int) it1, (T, int) it2)
        {
            return _shape.FindDistanceSquared(it1.Item1, it2.Item1);
        }
    }
}
