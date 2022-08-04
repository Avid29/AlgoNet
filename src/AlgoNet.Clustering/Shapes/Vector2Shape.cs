// Adam Dernis © 2022

using System;
using System.Numerics;

namespace AlgoNet.Clustering
{
    /// <summary>
    /// A shape defining how to handle <see cref="Vector2"/>s in a geometric space.
    /// </summary>
    public struct Vector2Shape : IGeometricSpace<Vector2, (int, int)>
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
            foreach (var item in items)
            {
                sumVector += item;
            }
            sumVector /= items.Length;
            return sumVector;
        }

        /// <inheritdoc/>
        public double FindDistanceSquared(Vector2 it1, Vector2 it2)
        {
            return (it1 - it2).LengthSquared();
        }

        /// <inheritdoc/>
        public (int, int) GetCell(Vector2 value, double window)
        {
            var shape = new FloatShape();
            int x = shape.GetCell(value.X, window);
            int y = shape.GetCell(value.Y, window);
            return (x, y);
        }
        
        /// <inheritdoc/>
        public ReadOnlySpan<(int, int)> GetNeighbors((int, int) cell)
        {
            var values = new (int, int)[8];
            int k = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == j) continue;

                    values[k] = (i, j);
                    k++;
                }
            }

            return values;
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
