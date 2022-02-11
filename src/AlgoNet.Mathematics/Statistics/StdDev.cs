// Adam Dernis © 2022

using System;

namespace AlgoNet.Mathematics
{
    internal static class Statistics
    {
        internal static double Mean(params double[] values)
        {
            double sum = ExtraMath.Sum(values);
            return sum / values.Length;
        }

        internal static double Variance(params double[] values)
        {
            double mean = Mean(values);
            double sum = 0;

            // sum = (x1 - mean)^2 + ... (xn - mean)^2
            for (int i = 0; i < values.Length; i++)
            {
                // (xi - mean)^2
                double value = values[i] - mean;
                value *= value;
                sum += value;
            }

            return sum / values.Length;
        }

        internal static double StandardDeviation(params double[] values) => Math.Sqrt(Variance(values));
    }
}
