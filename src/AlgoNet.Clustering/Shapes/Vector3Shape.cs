// Adam Dernis © 2022

using System;
using System.Numerics;

namespace AlgoNet.Clustering
{
    /// <summary>
    /// A shape defining how to handle <see cref="Vector3"/>s in a geometric space.
    /// </summary>
    public struct Vector3Shape : IGeometricSpace<Vector3, (int, int, int)>
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
        public (int, int, int) GetCell(Vector3 value, double window)
        {
            var shape = new FloatShape();
            int x = shape.GetCell(value.X, window);
            int y = shape.GetCell(value.Y, window);
            int z = shape.GetCell(value.Z, window);
            return (x, y, z);
        }

        public ReadOnlySpan<(int, int, int)> GetNeighbors((int, int, int) cell)
        {
            var values = new (int, int, int)[(3*3*3)-1];
            int i = 0;
            for (int j = -1; j <= 1; j++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    for (int l = -1; l <= 1; l++)
                    {
                        if (j == k && j == l) continue;

                        values[i] = (j, k, l);
                        i++;
                    }
                }
            }

            return values;
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
