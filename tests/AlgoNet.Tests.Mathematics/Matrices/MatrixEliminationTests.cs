// Adam Dernis © 2022

using AlgoNet.Mathematics.Matrices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Matrix = Microsoft.Toolkit.HighPerformance.Span2D<double>;

namespace AlgoNet.Tests.Mathematics.Matrices
{
    [TestClass]
    public class MatrixEliminationTests
    {
        [TestMethod]
        public void RowEchelon()
        {
            Matrix matrix = new double[,]
            {
                { 1, 3, 4 },
                { 1, 4, 5 },
                { 2, 6, 9 },
            };

            MatrixOperations.RowEchelon(matrix);
        }
    }
}
