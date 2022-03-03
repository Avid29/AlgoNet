// Adam Dernis © 2022

using System;

namespace AlgoNet.Mathematics.Generic
{
    public static class ExtraMath
    {
        public static T Sum<T>(params T[] values)
            where T : unmanaged, IAdditionOperators<T, T, T>
        {
            T sum = values[0];
            for (int i = 1; i < values.Length; i++)
                sum += values[i];

            return sum;
        }

        public static TBase Pow<TBase, TExponent>(TBase b, TExponent e)
            where TBase : IMultiplyOperators<TBase, TBase, TBase>
            where TExponent : IUnsignedNumber<TExponent>, IBinaryInteger<TExponent>
        {
            TExponent two = TExponent.One + TExponent.One;
            return Pow(b, e, two);
        }

        private static TBase Pow<TBase, TExponent>(TBase b, TExponent e, TExponent two)
            where TBase : IMultiplyOperators<TBase, TBase, TBase>
            where TExponent : IUnsignedNumber<TExponent>, IBinaryInteger<TExponent>
        {
            if (e == two) return b * b;
            if (e % two == TExponent.Zero) return Pow(b * b, e / two, two);
            return Pow(b * b, e / two, two) * b;
        }
    }
}
