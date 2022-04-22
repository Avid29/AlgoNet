// Adam Dernis © 2021

using AlgoNet.Clustering;
using System.Numerics;

namespace AlgoNet.Tests.Shapes
{
    public struct Vector2Shape : IGeometricPoint<Vector2>
    {
        public bool AreEqual(Vector2 it1, Vector2 it2)
        {
            return it1 == it2;
        }

        public bool AreEqual(Vector2 it1, Vector2 it2, double error = 0)
        {
            return FindDistanceSquared(it1, it2) <= error;
        }

        public Vector2 Average(Vector2[] items)
        {
            Vector2 sumVector = new Vector2(0);
            int count = 0;
            foreach (var item in items)
            {
                sumVector += item;
                count++;
            }
            sumVector.X /= count;
            sumVector.Y /= count;
            return sumVector;
        }

        public double FindDistanceSquared(Vector2 it1, Vector2 it2)
            => (it1 - it2).LengthSquared();

        public Vector2 WeightedAverage((Vector2, double)[] items)
        {
            Vector2 sumVector = Vector2.Zero;
            double totalWeight = 0;
            foreach (var item in items)
            {
                sumVector += item.Item1 * (float)item.Item2;
                totalWeight += item.Item2;
            }
            return sumVector / (float)totalWeight;
        }
    }
}
