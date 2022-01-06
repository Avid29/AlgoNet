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

        public static int[] DuplicateInteger10 = Reverse(PresortedSets.DuplicateInteger10);

        public static int[][] All = new int[][]
        {
            UniqueInteger100,
            DuplicateInteger10,
        };
    }
}
