// Adam Dernis © 2022

using System;
using System.Numerics;

namespace AlgoNet.Clustering
{
    /// <summary>
    /// A shape defining how to handle <see cref="Vector4"/>s in a geometric space.
    /// </summary>
    public struct Vector4Shape : IGeometricSpace<Vector4, (int, int, int, int)>
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
            foreach (var item in items)
            {
                sumVector += item;
            }
            sumVector /= items.Length;
            return sumVector;
        }

        /// <inheritdoc/>
        public double FindDistanceSquared(Vector4 it1, Vector4 it2)
        {
            return (it1 - it2).LengthSquared();
        }

        /// <inheritdoc/>
        public (int, int, int, int) GetCell(Vector4 value, double window)
        {
            var shape = new FloatShape();
            int w = shape.GetCell(value.W, window);
            int x = shape.GetCell(value.X, window);
            int y = shape.GetCell(value.Y, window);
            int z = shape.GetCell(value.Z, window);
            return (w, x, y, z);
        }

        public ReadOnlySpan<(int, int, int, int)> GetNeighbors((int, int, int, int) cell)
        {
            var values = new (int, int, int, int)[(3*3*3*3)-1];
            int i = 0;
            for (int j = -1; j <= 1; j++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    for (int l = -1; l <= 1; l++)
                    {
                        for (int m = -1; m <= 1; m++)
                        {
                            if (j == k && j == l && j == m) continue;

                            values[i] = (j, k, l, m);
                            i++;
                        }
                    }
                }
            }

            return values;
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
