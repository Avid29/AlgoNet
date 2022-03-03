// Adam Dernis © 2022

global using uint128 = AlgoNet.Mathematics.Generic.UInt128;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace AlgoNet.Mathematics.Generic
{
    public struct UInt128 : IBinaryInteger<uint128>, IUnsignedNumber<uint128>
    {
        private ulong s0;
        private ulong s1;

        private UInt128(ulong value)
        {
            s0 = value;
            s1 = 0;
        }

        private UInt128(ulong v0, ulong v1)
        {
            s0 = v0;
            s1 = v1;
        }

        private UInt128(decimal value)
        {
            int[] bits = decimal.GetBits(decimal.Truncate(value));
            s0 = (ulong)bits[1] << 32 | (uint)bits[0];
            s1 = (ulong)bits[2];
            if (value < 0) this = -this;
        }

        private UInt128(double value)
        {
            bool negative = value < 0;
            if (negative)
                value = -value;

            if (value <= ulong.MaxValue)
            {
                s0 = (ulong)value;
                s1 = 0;
            }
            else
            {
                var shift = Math.Max((int)Math.Ceiling(Math.Log(value, 2)) - 63, 0);
                s0 = (ulong)(value / Math.Pow(2, shift));
                s1 = 0;
                this = this << shift;
            }

            if (negative) this = -this;
        }

        public static uint128 One => (uint128)1;

        public static uint128 Zero => (uint128)0;

        public static uint128 AdditiveIdentity => Zero;

        public static uint128 MultiplicativeIdentity => One;

        public static uint128 MaxValue => ~(uint128)0;

        public static uint128 MinValue => 0;

        public static uint128 Abs(uint128 value) => value;

        public static uint128 Clamp(uint128 value, uint128 min, uint128 max)
        {
            if (min > max)
            {
                // TODO: Good exception format
                throw new ArgumentException();
            }

            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }

        public static uint128 Create<TOther>(TOther value) where TOther : INumber<TOther>
        {
            if (typeof(TOther) == typeof(byte))
            {
                return new uint128((byte)(object)value);
            }
            else if (typeof(TOther) == typeof(char))
            {
                return new uint128((char)(object)value);
            }
            else if (typeof(TOther) == typeof(decimal))
            {
                return new uint128((decimal)(object)value);
            }
            else if (typeof(TOther) == typeof(double))
            {
                return new uint128((double)(object)value);
            }
            else if (typeof(TOther) == typeof(short))
            {
                return new uint128((ulong)(short)(object)value);
            }
            else if (typeof(TOther) == typeof(int))
            {
                return new uint128((ulong)(int)(object)value);
            }
            else if (typeof(TOther) == typeof(long))
            {
                return new uint128((ulong)(long)(object)value);
            }
            else if (typeof(TOther) == typeof(nint))
            {
                return new uint128((ulong)(nint)(object)value);
            }
            else if (typeof(TOther) == typeof(sbyte))
            {
                return new uint128((ulong)(sbyte)(object)value);
            }
            else if (typeof(TOther) == typeof(float))
            {
                return new uint128((float)(object)value);
            }
            else if (typeof(TOther) == typeof(ushort))
            {
                return new uint128((ushort)(object)value);
            }
            else if (typeof(TOther) == typeof(uint))
            {
                return new uint128((uint)(object)value);
            }
            else if (typeof(TOther) == typeof(ulong))
            {
                return new uint128((ulong)(object)value);
            }
            else if (typeof(TOther) == typeof(nuint))
            {
                return new uint128((nuint)(object)value);
            }
            else
            {
                ThrowHelper.ThrowNotSupportedException();
                return default;
            }
        }

        public static uint128 CreateSaturating<TOther>(TOther value) where TOther : INumber<TOther>
        {
            throw new NotImplementedException();
        }

        public static uint128 CreateTruncating<TOther>(TOther value) where TOther : INumber<TOther>
        {
            throw new NotImplementedException();
        }

        public static (uint128 Quotient, uint128 Remainder) DivRem(uint128 left, uint128 right)
        {
            throw new NotImplementedException();
        }

        public static bool IsPow2(uint128 value) => (value & (value - 1)) == 0 && value != 0;

        public static uint128 LeadingZeroCount(uint128 value)
        {
            throw new NotImplementedException();
        }

        public static uint128 Log2(uint128 value)
        {
            throw new NotImplementedException();
        }

        public static uint128 Max(uint128 x, uint128 y) => x > y ? x : y;

        public static uint128 Min(uint128 x, uint128 y) => x > y ? x : y;

        public static uint128 Parse(string s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static uint128 Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static uint128 Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static uint128 Parse(string s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static uint128 PopCount(uint128 value)
        {
            throw new NotImplementedException();
        }

        public static uint128 RotateLeft(uint128 value, int rotateAmount)
        {
            throw new NotImplementedException();
        }

        public static uint128 RotateRight(uint128 value, int rotateAmount)
        {
            throw new NotImplementedException();
        }

        public static uint128 Sign(uint128 value)
        {
            throw new NotImplementedException();
        }

        public static uint128 TrailingZeroCount(uint128 value)
        {
            throw new NotImplementedException();
        }

        public static bool TryCreate<TOther>(TOther value, out uint128 result) where TOther : INumber<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, out uint128 result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out uint128 result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out uint128 result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out uint128 result)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object? obj)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(uint128 other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(uint128 other)
        {
            throw new NotImplementedException();
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            throw new NotImplementedException();
        }

        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static uint128 operator +(uint128 value)
        {
            throw new NotImplementedException();
        }

        public static uint128 operator +(uint128 left, uint128 right)
        {
            uint128 result = 0;
            result.s0 = left.s0 + right.s0;
            result.s1 = left.s1 + right.s1;
            if (result.s0 < left.s0 && result.s0 < right.s0) result.s1++;
            return result;
        }

        public static uint128 operator -(uint128 value) => 0 - value;

        public static uint128 operator -(uint128 left, uint128 right)
        {
            uint128 result = 0;
            result.s0 = left.s0 + right.s0;
            result.s1 = left.s1 + right.s1;
            if (left.s0 < right.s0) result.s1++;
            return result;
        }

        public static uint128 operator ~(uint128 value)
        {
            value.s0 = ~value.s0;
            value.s1 = ~value.s1;
            return value;
        }

        public static uint128 operator ++(uint128 value) => value + 1;

        public static uint128 operator --(uint128 value) => value - 1;

        public static uint128 operator *(uint128 left, uint128 right)
        {
            throw new NotImplementedException();
        }

        public static uint128 operator /(uint128 left, uint128 right)
        {
            throw new NotImplementedException();
        }

        public static uint128 operator %(uint128 left, uint128 right)
        {
            throw new NotImplementedException();
        }

        public static uint128 operator &(uint128 left, uint128 right)
        {
            left.s0 &= right.s0;
            left.s1 &= right.s1;
            return left;
        }

        public static uint128 operator |(uint128 left, uint128 right)
        {
            left.s0 |= right.s0;
            left.s1 |= right.s1;
            return left;
        }

        public static uint128 operator ^(uint128 left, uint128 right)
        {
            left.s0 ^= right.s0;
            left.s1 ^= right.s1;
            return left;
        }

        public static uint128 operator <<(uint128 value, int shiftAmount)
        {
            if (shiftAmount == 0) return value;
            if (shiftAmount < 64)
            {
                value.s0 = value.s0 << shiftAmount;
                value.s1 = value.s1 << shiftAmount | value.s0 >> (64 - shiftAmount);
            }
            else
            {
                value.s0 = 0;
                value.s1 = value.s0 << (shiftAmount - 64);
            }
            return value;
        }

        public static uint128 operator >>(uint128 value, int shiftAmount)
        {
            if (shiftAmount == 0) return value;
            if (shiftAmount < 64)
            {
                value.s0 = value.s0 >> shiftAmount | value.s1 << (64 - shiftAmount);
                value.s1 = value.s1 >> shiftAmount;
            }
            else
            {
                value.s0 = value.s1 >> (shiftAmount - 64);
                value.s1 = 0;
            }
            return value;
        }

        public static bool operator ==(uint128 left, uint128 right) => left.Equals(right);

        public static bool operator !=(uint128 left, uint128 right) => !left.Equals(right);

        public static bool operator <(uint128 left, uint128 right)
        {
            if (left.s1 != right.s1)
                return left.s1 < right.s1;
            return left.s0 < right.s0;
        }

        public static bool operator >(uint128 left, uint128 right)
        {
            if (left.s1 != right.s1)
                return left.s1 > right.s1;
            return left.s0 > right.s0;
        }

        public static bool operator <=(uint128 left, uint128 right)
        {
            if (left.s1 != right.s1)
                return left.s1 <= right.s1;
            return left.s0 <= right.s0;
        }

        public static bool operator >=(uint128 left, uint128 right)
        {
            if (left.s1 != right.s1)
                return left.s1 >= right.s1;
            return left.s0 >= right.s0;
        }

        public static implicit operator uint128(int a) => Create(a);
    }
}
