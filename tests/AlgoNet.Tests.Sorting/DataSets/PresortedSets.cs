// Adam Dernis © 2022

using AlgoNet.Tests.Data.Gradients.Shapes;
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

        public static int[] UniqueInteger1000 = GradientGenerator.Generate<int, IntegerGradientShape>(
            new DimensionSpecs[]
            {
                new DimensionSpecs(0, 999, 1000, new LinearEase()),
            });

        public static int[] DuplicateInteger100 = GradientGenerator.Generate<int, IntegerGradientShape>(
            new DimensionSpecs[]
            {
                new DimensionSpecs(0, 24, 100, new LinearEase()),
            });

        public static int[] DuplicateInteger1000 = GradientGenerator.Generate<int, IntegerGradientShape>(
            new DimensionSpecs[]
            {
                new DimensionSpecs(0, 249, 1000, new LinearEase()),
            });

        public static int[][] All = new int[][]
        {
            UniqueInteger100,
            UniqueInteger1000,
            DuplicateInteger100,
            DuplicateInteger1000,
        };
    }
}
