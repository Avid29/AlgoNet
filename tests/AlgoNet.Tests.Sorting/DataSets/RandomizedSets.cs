// Adam Dernis © 2022

using AlgoNet.Sorting.Shuffle;

namespace AlgoNet.Tests.Sorting.DataSets
{
    public class RandomizedSets
    {
        private static int[] Shuffle(int[] array)
        {
            FisherYates.Shuffle(array, 0);
            return array;
        }

        public static int[] UniqueInteger100 = Shuffle(PresortedSets.UniqueInteger100);

        public static int[] UniqueInteger1000 = Shuffle(PresortedSets.UniqueInteger1000);

        public static int[] DuplicateInteger100 = Shuffle(PresortedSets.DuplicateInteger100);

        public static int[] DuplicateInteger1000 = Shuffle(PresortedSets.DuplicateInteger1000);

        public static int[][] All = new int[][]
        {
            UniqueInteger100,
            UniqueInteger1000,
            DuplicateInteger100,
            DuplicateInteger1000,
        };
    }
}
