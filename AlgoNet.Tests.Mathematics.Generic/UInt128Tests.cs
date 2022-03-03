using AlgoNet.Mathematics.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace AlgoNet.Tests.Mathematics.Generic
{
    [TestClass]
    public class UInt128Tests
    {
        private BigInteger[] _values = new BigInteger[]
        {
            1,
            10,
            ulong.MaxValue,
            ((BigInteger)ulong.MaxValue) + 10,
        };

        private (BigInteger, BigInteger)[] _bigPairs = new (BigInteger, BigInteger)[]
        {
            (1, 1),
            (10, 5),
            (ulong.MaxValue, 5),
            (((BigInteger)ulong.MaxValue) + 10, 5),
        };

        private (BigInteger, int)[] _intPairs = new (BigInteger, int)[]
        {
            (1, 1),
            (10, 5),
            (ulong.MaxValue, 5),
            (((BigInteger)ulong.MaxValue) + 10, 5),
        };

        private (UInt128, UInt128) ConvertPair((BigInteger, BigInteger) pair) => ((UInt128)pair.Item1, (UInt128)pair.Item2);

        private (UInt128, int) ConvertPair((BigInteger, int) pair) => ((UInt128)pair.Item1, pair.Item2);

        [TestMethod]
        public void Add()
        {
            foreach (var pairBig in _bigPairs)
            {
                (UInt128, UInt128) pair128 = ConvertPair(pairBig);
                BigInteger canon = pairBig.Item1 + pairBig.Item2;
                UInt128 result = pair128.Item1 + pair128.Item2;

                Assert.IsTrue(canon == (BigInteger)result);
            }
        }

        [TestMethod]
        public void Subtract()
        {
            foreach (var pairBig in _bigPairs)
            {
                (UInt128, UInt128) pair128 = ConvertPair(pairBig);
                BigInteger canon = pairBig.Item1 - pairBig.Item2;
                UInt128 result = pair128.Item1 - pair128.Item2;

                Assert.IsTrue(canon == (BigInteger)result);
            }
        }

        [TestMethod]
        public void Increment()
        {
            foreach (var value in _values)
            {
                BigInteger valueBig = value;
                UInt128 value128 = (UInt128)value;
                BigInteger canon = valueBig++;
                UInt128 result = value128++;

                Assert.IsTrue(canon == (BigInteger)result);
            }
        }

        [TestMethod]
        public void Decrement()
        {
            foreach (var value in _values)
            {
                BigInteger valueBig = value;
                UInt128 value128 = (UInt128)value;
                BigInteger canon = valueBig--;
                UInt128 result = value128--;

                Assert.IsTrue(canon == (BigInteger)result);
            }
        }

        [TestMethod]
        public void And()
        {
            foreach (var pairBig in _intPairs)
            {
                (UInt128, int) pair128 = ConvertPair(pairBig);
                BigInteger canon = pairBig.Item1 & pairBig.Item2;
                UInt128 result = pair128.Item1 & pair128.Item2;

                Assert.IsTrue(canon == (BigInteger)result);
            }
        }

        [TestMethod]
        public void Or()
        {
            foreach (var pairBig in _intPairs)
            {
                (UInt128, int) pair128 = ConvertPair(pairBig);
                BigInteger canon = pairBig.Item1 | pairBig.Item2;
                UInt128 result = pair128.Item1 | pair128.Item2;

                Assert.IsTrue(canon == (BigInteger)result);
            }
        }

        [TestMethod]
        public void XOr()
        {
            foreach (var pairBig in _intPairs)
            {
                (UInt128, int) pair128 = ConvertPair(pairBig);
                BigInteger canon = pairBig.Item1 ^ pairBig.Item2;
                UInt128 result = pair128.Item1 ^ pair128.Item2;

                Assert.IsTrue(canon == (BigInteger)result);
            }
        }

        [TestMethod]
        public void Flip()
        {
            foreach (var value in _values)
            {
                UInt128 value128 = (UInt128)value;
                UInt128 result = ~value128;
                UInt128 canon = UInt128.MaxValue - value128;

                Assert.IsTrue(canon == result);
            }
        }

        [TestMethod]
        public void RightShift()
        {
            foreach (var pairBig in _intPairs)
            {
                (UInt128, int) pair128 = ConvertPair(pairBig);
                BigInteger canon = pairBig.Item1 >> pairBig.Item2;
                UInt128 result = pair128.Item1 >> pair128.Item2;

                Assert.IsTrue(canon == (BigInteger)result);
            }
        }

        [TestMethod]
        public void LeftShift()
        {
            foreach (var pairBig in _intPairs)
            {
                (UInt128, int) pair128 = ConvertPair(pairBig);
                BigInteger canon = pairBig.Item1 << pairBig.Item2;
                UInt128 result = pair128.Item1 << pair128.Item2;

                Assert.IsTrue(canon == (BigInteger)result);
            }
        }
    }
}
