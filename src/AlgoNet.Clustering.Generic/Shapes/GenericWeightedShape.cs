// Adam Dernis © 2022

using System;

namespace AlgoNet.Clustering.Generic.Shapes
{
    /// <summary>
    /// A shape for wrapping a weighted point list to cluster in a metric space. 
    /// </summary>
    /// <typeparam name="T">The type of </typeparam>
    /// <typeparam name="TShape">The type of the child shape for <typeparamref name="T"/>.</typeparam>
    /// <typeparam name="TWeight">The type of number used for weight.</typeparam>
    /// <typeparam name="TDistance">The type of number used for distance.</typeparam>
    internal struct GenericWeightedShape<T, TShape, TWeight, TDistance> : IMetricPoint<(T, TWeight), TDistance>
        where T : unmanaged
        where TShape : struct, IMetricPoint<T, TDistance>
        where TWeight : INumber<TWeight>
        where TDistance : IFloatingPoint<TDistance>
    {
        private readonly TShape _shape;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericWeightedShape{T, TShape, TWeight, TDistance}"/> struct.
        /// </summary>
        /// <param name="shape">The child shape for <typeparamref name="T"/>.</param>
        public GenericWeightedShape(TShape shape)
        {
            _shape = shape;
        }

        /// <inheritdoc/>
        public bool AreEqual((T, TWeight) it1, (T, TWeight) it2)
        {
            return _shape.AreEqual(it1.Item1, it2.Item1);
        }

        /// <inheritdoc/>
        public bool AreEqual((T, TWeight) it1, (T, TWeight) it2, TDistance error)
        {
            return _shape.AreEqual(it1.Item1, it2.Item1, error);
        }

        /// <inheritdoc/>
        public TDistance FindDistanceSquared((T, TWeight) it1, (T, TWeight) it2)
        {
            return _shape.FindDistanceSquared(it1.Item1, it2.Item1);
        }
    }
}
