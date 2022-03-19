// Adam Dernis © 2022

namespace AlgoNet.Mathematics
{
    public static partial class ExtraMath
    {
        /// <summary>
        /// Returns the sum of an array of numbers.
        /// </summary>
        /// <param name="values">The numbers to sum.</param>
        /// <returns>The sum of the numbers in <paramref name="values"/>.</returns>
        public static double Sum(params double[] values)
        {
            double sum = values[0];
            for (int i = 1; i < values.Length; i++)
                sum += values[i];

            return sum;
        }

        /// <summary>
        /// Returns b to the power of e.
        /// </summary>
        /// <param name="b">The base for the power operation.</param>
        /// <param name="e">The exponent for the power operation.</param>
        /// <returns>B to the power of E.</returns>
        public static double Pow(double b, uint e)
        {
            if (e == 0) return 1;
            if (e % 2 == 0) return Pow(b * b, e / 2);
            return Pow(b * b, e / 2) * b;
        }
    }
}
