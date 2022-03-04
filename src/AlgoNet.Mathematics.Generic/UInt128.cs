// Adam Dernis © 2022

global using uint128 = AlgoNet.Mathematics.Generic.UInt128;
using Microsoft.Toolkit.Diagnostics;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace AlgoNet.Mathematics.Generic
{
    [DebuggerDisplay("{ToString()}")]
    public struct UInt128 : IBinaryInteger<uint128>, IUnsignedNumber<uint128>
    {
        private ulong s0;
        private ulong s1;

        private UInt128(ulong value)
        {
            s0 = value;
            s1 = 0;
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

        private UInt128(BigInteger value)
        {
            bool negative = value.Sign == -1;
            if (negative)
                value = -value;
            s0 = (ulong)(value & ulong.MaxValue);
            s1 = (ulong)(value >> 64);

            if (negative)
                this = -this;
        }

        /// <inheritdoc/>
        public static uint128 One => (uint128)1;

        /// <inheritdoc/>
        public static uint128 Zero => (uint128)0;

        /// <inheritdoc/>
        public static uint128 AdditiveIdentity => Zero;

        /// <inheritdoc/>
        public static uint128 MultiplicativeIdentity => One;

        /// <inheritdoc/>
        public static uint128 MaxValue => ~(uint128)0;

        /// <inheritdoc/>
        public static uint128 MinValue => 0;

        /// <inheritdoc/>
        public static uint128 Abs(uint128 value) => value;

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public static uint128 Create<TOther>(TOther value) where TOther : INumber<TOther>
        {
            bool success = TryCreate(value, out uint128 result);
            if (!success) ThrowHelper.ThrowArgumentException();
            return result;
        }

        /// <inheritdoc/>
        public static uint128 CreateSaturating<TOther>(TOther value) where TOther : INumber<TOther>
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public static uint128 CreateTruncating<TOther>(TOther value) where TOther : INumber<TOther>
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public static (uint128 Quotient, uint128 Remainder) DivRem(uint128 left, uint128 right)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public static bool IsPow2(uint128 value) => (value & (value - 1)) == 0 && value != 0;

        /// <inheritdoc/>
        public static uint128 LeadingZeroCount(uint128 value)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public static uint128 Log2(uint128 value)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public static uint128 Max(uint128 x, uint128 y) => x > y ? x : y;

        /// <inheritdoc/>
        public static uint128 Min(uint128 x, uint128 y) => x > y ? x : y;

        /// <inheritdoc/>
        public static uint128 Parse(string s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public static uint128 Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public static uint128 Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public static uint128 Parse(string s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public static uint128 PopCount(uint128 value) => BitOperations.PopCount(value.s0) + BitOperations.PopCount(value.s1);

        /// <inheritdoc/>
        public static uint128 RotateLeft(uint128 value, int rotateAmount)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public static uint128 RotateRight(uint128 value, int rotateAmount)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public static uint128 Sign(uint128 value)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public static uint128 TrailingZeroCount(uint128 value)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public static bool TryCreate<TOther>(TOther value, out uint128 result) where TOther : INumber<TOther>
        {
            if (typeof(TOther) == typeof(byte))
            {
                result = new uint128((byte)(object)value);
                return true;
            }
            else if (typeof(TOther) == typeof(char))
            {
                result = new uint128((char)(object)value);
                return true;
            }
            else if (typeof(TOther) == typeof(decimal))
            {
                result = new uint128((decimal)(object)value);
                return true;
            }
            else if (typeof(TOther) == typeof(double))
            {
                result = new uint128((double)(object)value);
                return true;
            }
            else if (typeof(TOther) == typeof(short))
            {
                result = new uint128((ulong)(short)(object)value);
                return true;
            }
            else if (typeof(TOther) == typeof(int))
            {
                result = new uint128((ulong)(int)(object)value);
                return true;
            }
            else if (typeof(TOther) == typeof(long))
            {
                result = new uint128((ulong)(long)(object)value);
                return true;
            }
            else if (typeof(TOther) == typeof(nint))
            {
                result = new uint128((ulong)(nint)(object)value);
                return true;
            }
            else if (typeof(TOther) == typeof(sbyte))
            {
                result = new uint128((ulong)(sbyte)(object)value);
                return true;
            }
            else if (typeof(TOther) == typeof(float))
            {
                result = new uint128((float)(object)value);
                return true;
            }
            else if (typeof(TOther) == typeof(ushort))
            {
                result = new uint128((ushort)(object)value);
                return true;
            }
            else if (typeof(TOther) == typeof(uint))
            {
                result = new uint128((uint)(object)value);
                return true;
            }
            else if (typeof(TOther) == typeof(ulong))
            {
                result = new uint128((ulong)(object)value);
                return true;
            }
            else if (typeof(TOther) == typeof(nuint))
            {
                result = new uint128((nuint)(object)value);
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        /// <inheritdoc/>
        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, out uint128 result)
        {
            bool success = BigInteger.TryParse(s, style, provider, out BigInteger biResult);
            result = (uint128)biResult;
            return success;
        }

        /// <inheritdoc/>
        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out uint128 result)
        {
            bool success = BigInteger.TryParse(s, style, provider, out BigInteger biResult);
            result = (uint128)biResult;
            return success;
        }

        /// <inheritdoc/>
        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out uint128 result)
        {
            bool success = BigInteger.TryParse(s, NumberStyles.Integer, provider, out BigInteger biResult);
            result = (uint128)biResult;
            return success;
        }

        /// <inheritdoc/>
        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out uint128 result)
        {
            bool success = BigInteger.TryParse(s, NumberStyles.Integer, provider, out BigInteger biResult);
            result = (uint128)biResult;
            return success;
        }

        /// <inheritdoc/>
        public int CompareTo(object? obj)
        {
            if (obj == null)
                return 1;
            if (!(obj is uint128))
                throw new ArgumentException();
            return CompareTo((uint128)obj);
        }

        /// <inheritdoc/>
        public int CompareTo(uint128 other)
        {
            if (s1 != other.s1)
                return s1.CompareTo(other.s1);
            return s0.CompareTo(other.s0);
        }

        /// <inheritdoc/>
        public bool Equals(uint128 other) => s0 == other.s0 && s1 == other.s1;

        /// <inheritdoc/>
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            return ((BigInteger)this).ToString(format, formatProvider);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return ((BigInteger)this).ToString();
        }

        /// <inheritdoc/>
        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public static uint128 operator +(uint128 value)
        {
            return value;
        }

        /// <inheritdoc/>
        public static uint128 operator +(uint128 left, uint128 right)
        {
            uint128 result = 0;
            result.s0 = left.s0 + right.s0;
            result.s1 = left.s1 + right.s1;
            if (result.s0 < left.s0 && result.s0 < right.s0) result.s1++;
            return result;
        }

        /// <inheritdoc/>
        public static uint128 operator -(uint128 value) => 0 - value;

        /// <inheritdoc/>
        public static uint128 operator -(uint128 left, uint128 right)
        {
            uint128 result = 0;
            result.s0 = left.s0 - right.s0;
            result.s1 = left.s1 - right.s1;
            if (left.s0 < right.s0) result.s1--;
            return result;
        }

        /// <inheritdoc/>
        public static uint128 operator ~(uint128 value)
        {
            value.s0 = ~value.s0;
            value.s1 = ~value.s1;
            return value;
        }

        /// <inheritdoc/>
        public static uint128 operator ++(uint128 value) => value + 1;

        /// <inheritdoc/>
        public static uint128 operator --(uint128 value) => value - 1;
        
        /// <inheritdoc/>
        public static uint128 operator *(uint128 left, uint128 right)
        {
            throw new NotImplementedException();
        }
        
        /// <inheritdoc/>
        public static uint128 operator /(uint128 left, uint128 right)
        {
            throw new NotImplementedException();
        }
        
        /// <inheritdoc/>
        public static uint128 operator %(uint128 left, uint128 right)
        {
            throw new NotImplementedException();
        }
        
        /// <inheritdoc/>
        public static uint128 operator &(uint128 left, uint128 right)
        {
            left.s0 &= right.s0;
            left.s1 &= right.s1;
            return left;
        }
        
        /// <inheritdoc/>
        public static uint128 operator |(uint128 left, uint128 right)
        {
            left.s0 |= right.s0;
            left.s1 |= right.s1;
            return left;
        }
        
        /// <inheritdoc/>
        public static uint128 operator ^(uint128 left, uint128 right)
        {
            left.s0 ^= right.s0;
            left.s1 ^= right.s1;
            return left;
        }
        
        /// <inheritdoc/>
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
        
        /// <inheritdoc/>
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
        
        /// <inheritdoc/>
        public static bool operator ==(uint128 left, uint128 right) => left.Equals(right);
        
        /// <inheritdoc/>
        public static bool operator !=(uint128 left, uint128 right) => !left.Equals(right);

        /// <inheritdoc/>
        public static bool operator <(uint128 left, uint128 right)
        {
            if (left.s1 != right.s1)
                return left.s1 < right.s1;
            return left.s0 < right.s0;
        }

        /// <inheritdoc/>
        public static bool operator >(uint128 left, uint128 right)
        {
            if (left.s1 != right.s1)
                return left.s1 > right.s1;
            return left.s0 > right.s0;
        }
        
        /// <inheritdoc/>
        public static bool operator <=(uint128 left, uint128 right)
        {
            if (left.s1 != right.s1)
                return left.s1 <= right.s1;
            return left.s0 <= right.s0;
        }
        
        /// <inheritdoc/>
        public static bool operator >=(uint128 left, uint128 right)
        {
            if (left.s1 != right.s1)
                return left.s1 >= right.s1;
            return left.s0 >= right.s0;
        }
        
        /// <inheritdoc/>
        public static implicit operator uint128(int a) => Create(a);

        /// <inheritdoc/>
        public static implicit operator uint128(ulong a) => Create(a);

        /// <inheritdoc/>
        public static implicit operator uint128(BigInteger a) => new(a);

        /// <inheritdoc/>
        public static implicit operator BigInteger(uint128 a)
        {
            if (a.s1 == 0)
                return a.s0;
            return (BigInteger)a.s1 << 64 | a.s0;
        }
    }
}
