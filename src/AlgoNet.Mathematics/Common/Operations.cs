// Adam Dernis © 2022

namespace AlgoNet.Mathematics
{
    public static partial class ExtraMath
    {
        internal static double Sum(params double[] values)
        {
            double sum = values[0];
            for (int i = 1; i < values.Length; i++)
                sum += values[i];

            return sum;
        }
    }
}
