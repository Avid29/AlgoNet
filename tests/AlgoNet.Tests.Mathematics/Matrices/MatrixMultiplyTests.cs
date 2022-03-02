// Adam Dernis © 2022

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;
using AlgoNet.Mathematics.Matrices;
using System;
using MOGPU = AlgoNet.Mathematics.GPU.Matricies.MatrixOperations.MatrixOperations;

namespace AlgoNet.Tests.Mathematics.Matrices
{
    [TestClass]
    public class MatrixMultiplyTests
    {
        private const double ACCEPTED_ERROR = 0.0000001d;

        private (Matrix4x4, Matrix4x4)[] _pairs = new (Matrix4x4, Matrix4x4)[]
        {
            (Matrix4x4.Identity, Matrix4x4.Identity),
            (Matrix4x4.CreateTranslation(2, 4, 3), Matrix4x4.CreateScale(8)),
            (Matrix4x4.CreateRotationX(2) * Matrix4x4.CreateRotationY(5), Matrix4x4.CreateRotationZ(3) * Matrix4x4.CreateRotationY(2) * Matrix4x4.CreateTranslation(2, 5, 8)),
        };

        private bool AreEqual(Matrix a, Matrix b)
        {
            for(int i = 0; i < a.Height; i++)
            {
                for(int j = 0; j < a.Width; j++)
                {
                    if (Math.Abs(a[i, j] - b[i, j]) > ACCEPTED_ERROR) return false;
                }
            }
            return true;
        }

        [TestMethod]
        public void NaiveMultiply()
        {
            foreach (var pair in _pairs)
            {
                var mCanon = pair.Item1 * pair.Item2;
                var a = (Matrix)pair.Item1;
                var b = (Matrix)pair.Item2;
                var canon = (Matrix)mCanon;

                var result = a * b;
                Assert.IsTrue(AreEqual(canon, result));
            }
        }

        [TestMethod]
        public void StrassenMultiply()
        {
            foreach (var pair in _pairs)
            {
                var mCanon = pair.Item1 * pair.Item2;
                var a = (Matrix)pair.Item1;
                var b = (Matrix)pair.Item2;
                var canon = (Matrix)mCanon;

                var result = Strassen.Multiply(a, b);
                Assert.IsTrue(AreEqual(canon, result));
            }
        }

        [TestMethod]
        public void GPUMultiply()
        {
            foreach (var pair in _pairs)
            {
                var mCanon = pair.Item1 * pair.Item2;
                var a = AsMatrix(pair.Item1);
                var b = AsMatrix(pair.Item2);
                var canon = AsMatrix(mCanon);

                var result = MOGPU.Multiply(a, b);
                Assert.IsTrue(AreEqual(canon, result));
            }
        }
    }
}
