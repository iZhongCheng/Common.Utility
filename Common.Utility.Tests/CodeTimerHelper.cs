using System;

namespace Common.Utility.Tests
{
    /// <summary>
    /// 运行示例
    /// </summary>
    internal partial class Program
    {
        private static void Main_CodeTimerHelper(string[] args)
        {
            int arraySize;
            int[] data;
            Random rnd;

            arraySize = 10000;
            data = new int[arraySize];

            rnd = new Random(0);
            for (int c = 0; c < arraySize; ++c)
                data[c] = rnd.Next(256);

            long sum = 0;
            CodeTimerHelper.Time("unsorted array", 100000, () =>
            {
                for (int c = 0; c < arraySize; ++c)
                {
                    if (data[c] >= 128)
                        sum += data[c];
                }
            });

            Array.Sort(data);

            sum = 0;
            CodeTimerHelper.Time("sorted array", 100000, () =>
            {
                for (int c = 0; c < arraySize; ++c)
                {
                    if (data[c] >= 128)
                        sum += data[c];
                }
            });
        }
    }
}