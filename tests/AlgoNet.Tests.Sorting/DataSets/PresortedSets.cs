// Adam Dernis © 2022

using AlgoNet.Tests.Data.Gradients.Shape;
using AlgoNet.Tests.Gradients;
using AlgoNet.Tests.Gradients.Easing;

namespace AlgoNet.Tests.Sorting.DataSets
{
    public class PresortedSets
    {
        public static int[] UniqueInteger100 = GradientGenerator.Generate<int, IntegerGradientShape>(
            new DimensionSpecs[]
            {
                new DimensionSpecs(0, 99, 100, new LinearEase()),
            });

        public static int[] DuplicateInteger10 = GradientGenerator.Generate<int, IntegerGradientShape>(
            new DimensionSpecs[]
            {
                new DimensionSpecs(0, 24, 100, new LinearEase()),
            });

        public static int[][] All = new int[][]
        {
            UniqueInteger100,
            DuplicateInteger10,
        };
    }
}
