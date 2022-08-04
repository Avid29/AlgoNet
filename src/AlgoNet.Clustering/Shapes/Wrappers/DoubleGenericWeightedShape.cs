// Adam Dernis © 2022

namespace AlgoNet.Clustering.Shapes
{
    /// <summary>
    /// A shape for wrapping in a <see cref="double"/> weighted point list to cluster in a metric space. 
    /// </summary>
    /// <typeparam name="T">The type of </typeparam>
    /// <typeparam name="TShape">The type of the child shape for <typeparamref name="T"/>.</typeparam>
    internal struct DoubleGenericWeightedShape<T, TShape> : IDistanceSpace<(T, double)>
        where T : unmanaged
        where TShape : struct, IDistanceSpace<T>
    {
        private readonly TShape _shape;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleGenericWeightedShape{T, TShape}"/> struct.
        /// </summary>
        /// <param name="shape">The child shape for <typeparamref name="T"/>.</param>
        public DoubleGenericWeightedShape(TShape shape)
        {
            _shape = shape;
        }

        /// <inheritdoc/>
        public bool AreEqual((T, double) it1, (T, double) it2)
        {
            return _shape.AreEqual(it1.Item1, it2.Item1);
        }

        /// <inheritdoc/>
        public double FindDistanceSquared((T, double) it1, (T, double) it2)
        {
            return _shape.FindDistanceSquared(it1.Item1, it2.Item1);
        }
    }
}
