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

        public static int[] DuplicateInteger10 = Shuffle(PresortedSets.DuplicateInteger10);

        public static int[][] All = new int[][]
        {
            UniqueInteger100,
            DuplicateInteger10,
        };
    }
}
