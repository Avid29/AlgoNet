// Adam Dernis © 2022

using System.Numerics;

namespace AlgoNet.Clustering
{
    /// <summary>
    /// A shape defining how to handle <see cref="Vector4"/>s in a geometric space.
    /// </summary>
    public struct Vector4Shape : IGeometricSpace<Vector4>
    {
        /// <inheritdoc/>
        public bool AreEqual(Vector4 it1, Vector4 it2)
        {
            return it1 == it2;
        }

        /// <inheritdoc/>
        public Vector4 Average(Vector4[] items)
        {
            Vector4 sumVector = Vector4.Zero;
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
        public double FindDistanceSquared(Vector4 it1, Vector4 it2)
        {
            return (it1 - it2).LengthSquared();
        }

        /// <inheritdoc/>
        public Vector4 Round(Vector4 value, double detail)
        {
            var shape = new FloatShape();
            Vector4 rounded = value;
            rounded.W = shape.Round(rounded.W, detail);
            rounded.X = shape.Round(rounded.X, detail);
            rounded.Y = shape.Round(rounded.Y, detail);
            rounded.Z = shape.Round(rounded.Z, detail);
            return rounded;
        }

        /// <inheritdoc/>
        public Vector4 WeightedAverage((Vector4, double)[] items)
        {
            Vector4 sumVector = Vector4.Zero;
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
