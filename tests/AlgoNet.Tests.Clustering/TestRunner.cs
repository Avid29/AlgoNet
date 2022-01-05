// Adam Dernis © 2021

using System;

namespace AlgoNet.Tests.Clustering
{
    public static class TestRunner
    {
        public static void RunTest(ITest test)
        {
            try
            {
                test.Run();
            }
            catch (Exception e)
            {
                throw new Exception($"Test {test.Name} failed.", e);
            }
        }
    }
}
