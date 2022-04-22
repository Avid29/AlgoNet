// Adam Dernis © 2022

using AlgoNet.Clustering;
using System.Numerics;

namespace AlgoNet.Tests.Data.Shapes
{
    public struct Vector3Shape : IGeometricPoint<Vector3>
    {
        public bool AreEqual(Vector3 it1, Vector3 it2)
        {
            return it1 == it2;
        }

        public bool AreEqual(Vector3 it1, Vector3 it2, double error = 0)
        {
            return FindDistanceSquared(it1, it2) <= error;
        }

        public Vector3 Average(Vector3[] items)
        {
            Vector3 sumVector = Vector3.Zero;
            int count = 0;
            foreach (var item in items)
            {
                sumVector += item;
                count++;
            }
            return sumVector /= count; ;
        }

        public double FindDistanceSquared(Vector3 it1, Vector3 it2)
            => (it1 - it2).LengthSquared();

        public Vector3 WeightedAverage((Vector3, double)[] items)
        {
            Vector3 sumVector = Vector3.Zero;
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
