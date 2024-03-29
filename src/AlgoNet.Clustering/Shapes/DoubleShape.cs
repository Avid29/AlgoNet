﻿// Adam Dernis © 2022

using System;

namespace AlgoNet.Clustering
{
    /// <summary>
    /// A shape defining how to handle <see cref="double"/>s in a geometric space.
    /// </summary>
    public struct DoubleShape : IGeometricSpace<double>
    {
        /// <inheritdoc/>
        public bool AreEqual(double it1, double it2)
        {
            return it1 == it2;
        }

        /// <inheritdoc/>
        public double Average(double[] items)
        {
            double sum = 0;
            int count = 0;
            foreach (var item in items)
            {
                sum += item;
                count++;
            }
            return sum /= count;
        }

        /// <inheritdoc/>
        public double FindDistanceSquared(double it1, double it2)
        {
            return Math.Abs(it1 - it2);
        }

        /// <inheritdoc/>
        public double GetCell(double value, double detail)
        {
            return Math.Round(value / detail) * detail;
        }

        /// <inheritdoc/>
        public double WeightedAverage((double, double)[] items)
        {
            double sum = 0;
            double totalWeight = 0;
            foreach (var item in items)
            {
                sum += item.Item1 * item.Item2;
                totalWeight += item.Item2;
            }
            return sum / totalWeight;
        }
    }
}
