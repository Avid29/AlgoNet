// Adam Dernis © 2022

namespace AlgoNet.Tests.Clustering.MeanShift
{
    public interface IMSTest : ITest
    {
        void RunAsyncCompare();

        void RunWeightedCompare();
    }
}
