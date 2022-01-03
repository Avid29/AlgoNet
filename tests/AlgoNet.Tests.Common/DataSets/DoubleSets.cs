// Adam Dernis © 2021

namespace AlgoNet.Tests.Data
{
    public static class DoubleSets
    {
        public static DataSet<double> Test1 = new DataSet<double>(
            "Double test 1",
            new double[]
            {
                0,
                1,
                8,
                10,
                12,
                22,
                24,
            });

        public static DataSet<double>[] All = new DataSet<double>[]
        {
            Test1,
        };
    }
}
