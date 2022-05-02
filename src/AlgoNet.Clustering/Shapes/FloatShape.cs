// Adam Dernis © 2022

using System;

namespace AlgoNet.Clustering
{
    /// <summary>
    /// A shape defining how to handle <see cref="float"/>s in a geometric space.
    /// </summary>
    public struct FloatShape : IGeometricSpace<float>
    {
        /// <inheritdoc/>
        public bool AreEqual(float it1, float it2)
        {
            return it1 == it2;
        }

        /// <inheritdoc/>
        public float Average(float[] items)
        {
            float sum = 0;
            int count = 0;
            foreach (var item in items)
            {
                sum += item;
                count++;
            }
            return sum /= count;
        }

        /// <inheritdoc/>
        public double FindDistanceSquared(float it1, float it2)
        {
            return Math.Abs(it1 - it2);
        }

        /// <inheritdoc/>
        public float GetCell(float value, double detail)
        {
            return (float)(Math.Round(value / detail) * detail);
        }

        /// <inheritdoc/>
        public float WeightedAverage((float, double)[] items)
        {
            float sum = 0;
            double totalWeight = 0;
            foreach (var item in items)
            {
                sum += (float)(item.Item1 * item.Item2);
                totalWeight += item.Item2;
            }
            return (float)(sum / totalWeight);
        }
    }
}
