// Adam Dernis © 2022

namespace AlgoNet.Tests.Sorting.DataSets
{
    public static class ReverseOrderSets
    {
        private static int[] Reverse(int[] array)
        {
            int[] output = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                output[i] = array[array.Length - i - 1];
            }

            return output;
        }

        public static int[] UniqueInteger100 = Reverse(PresortedSets.UniqueInteger100);

        public static int[] UniqueInteger1000 = Reverse(PresortedSets.UniqueInteger1000);

        public static int[] DuplicateInteger100 = Reverse(PresortedSets.DuplicateInteger100);

        public static int[] DuplicateInteger1000 = Reverse(PresortedSets.DuplicateInteger1000);

        public static int[][] All = new int[][]
        {
            UniqueInteger100,
            UniqueInteger1000,
            DuplicateInteger100,
            DuplicateInteger1000,
        };
    }
}
