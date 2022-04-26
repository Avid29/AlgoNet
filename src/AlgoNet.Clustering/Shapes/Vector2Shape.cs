// Adam Dernis © 2022

using System.Numerics;

namespace AlgoNet.Clustering
{
    /// <summary>
    /// A shape defining how to handle <see cref="Vector2"/>s in a geometric space.
    /// </summary>
    public struct Vector2Shape : IGeometricSpace<Vector2>
    {
        /// <inheritdoc/>
        public bool AreEqual(Vector2 it1, Vector2 it2)
        {
            return it1 == it2;
        }

        /// <inheritdoc/>
        public Vector2 Average(Vector2[] items)
        {
            Vector2 sumVector = Vector2.Zero;
            int count = 0;
            foreach (var item in items)
            {
                sumVector += item;
                count++;
            }
            sumVector /= count;
            return sumVector;
        }

        /// <inheritdoc/>
        public double FindDistanceSquared(Vector2 it1, Vector2 it2)
        {
            return (it1 - it2).LengthSquared();
        }

        /// <inheritdoc/>
        public Vector2 Round(Vector2 value, double detail)
        {
            var shape = new FloatShape();
            Vector2 rounded = value;
            rounded.X = shape.Round(rounded.X, detail);
            rounded.Y = shape.Round(rounded.Y, detail);
            return rounded;
        }

        /// <inheritdoc/>
        public Vector2 WeightedAverage((Vector2, double)[] items)
        {
            Vector2 sumVector = Vector2.Zero;
            double totalWeight = 0;
            foreach (var item in items)
            {
                sumVector += item.Item1 * (float)item.Item2;
                totalWeight += item.Item2;
            }
            sumVector /= (float)totalWeight;
            return sumVector;
        }
    }
}
