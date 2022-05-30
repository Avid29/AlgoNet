// Adam Dernis © 2022

using System.Numerics;

namespace AlgoNet.Clustering
{
    /// <summary>
    /// A shape defining how to handle <see cref="Vector3"/>s in a geometric space.
    /// </summary>
    public struct Vector3Shape : IGeometricSpace<Vector3>
    {
        /// <inheritdoc/>
        public bool AreEqual(Vector3 it1, Vector3 it2)
        {
            return it1 == it2;
        }

        /// <inheritdoc/>
        public Vector3 Average(Vector3[] items)
        {
            Vector3 sumVector = Vector3.Zero;
            foreach (var item in items)
            {
                sumVector += item;
            }
            sumVector /= items.Length;
            return sumVector;
        }

        /// <inheritdoc/>
        public double FindDistanceSquared(Vector3 it1, Vector3 it2)
        {
            return (it1 - it2).LengthSquared();
        }

        /// <inheritdoc/>
        public Vector3 GetCell(Vector3 value, double window)
        {
            var shape = new FloatShape();
            Vector3 rounded = value;
            rounded.X = shape.GetCell(rounded.X, window);
            rounded.Y = shape.GetCell(rounded.Y, window);
            rounded.Z = shape.GetCell(rounded.Z, window);
            return rounded;
        }

        /// <inheritdoc/>
        public Vector3 WeightedAverage((Vector3, double)[] items)
        {
            Vector3 sumVector = Vector3.Zero;
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
