// Adam Dernis © 2021

using ComputeSharp;
using System;
using System.Numerics;

namespace AlgoNet.Clustering.MeanShift.Shaders
{
    [AutoConstructor]
    public partial struct EuclideanVector3MeanShiftShader : IComputeShader
    {
        private const double ACCEPTED_ERROR = 0.000005;

        private ReadWriteBuffer<Vector3> _points;
        private ReadOnlyBuffer<Vector3> _field;
        private ReadWriteTexture2D<float> _fieldWeight;
        private float _windowDenominatorBandwidth;

        private float FindDistanceSquared(Vector3 item1, Vector3 item2)
            => (item1 - item2).LengthSquared();

        private float WeightDistance(float distanceSquared)
            => Hlsl.Exp(distanceSquared / _windowDenominatorBandwidth);

        private Vector3 FieldWeightedAverage(int thread)
        {
            Vector3 sumVector = Vector3.Zero;
            float sumWeight = 0;
            for (int i = 0; i < thread; i++)
            {
                Vector3 value = _field[i];
                float weight = _fieldWeight[thread, i];

                sumVector += value * weight;
                sumWeight += weight;
            }
            return sumVector / sumWeight;
        }

        /// <inheritdoc/>
        public void Execute()
        {
            int x = ThreadIds.X;
            Vector3 cluster = _points[x];
            bool changed = true;

            // Shift point until it converges
            while (changed)
            {
                for (int i = 0; i < _field.Length; i++)
                {
                    Vector3 point = _field[i];
                    float distSqrd = FindDistanceSquared(point, cluster);
                    float weight = WeightDistance(distSqrd);
                    _fieldWeight[x, i] = weight;
                }

                _points[x] = FieldWeightedAverage(x);
                changed = FindDistanceSquared(cluster, _points[x]) > ACCEPTED_ERROR;
                cluster = _points[x];
            }
        }
    }
}
