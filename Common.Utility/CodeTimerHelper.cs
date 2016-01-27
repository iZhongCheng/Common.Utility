using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace Common.Utility
{
    /// <summary>
    /// Author：Jeffrey Zhao
    /// Date Created：2009-03-10
    /// Description：性能计数器-工具类
    /// </summary>
    public static class CodeTimerHelper
    {
        public static void Initialize()
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            Time("", 1, () => { });
        }

        public static void Time(string name, int iteration, Action action)
        {
            if (String.IsNullOrEmpty(name)) return;

            // 1.
            ConsoleColor currentForeColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(name);

            // 2.
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            int[] gcCounts = new int[GC.MaxGeneration + 1];
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                gcCounts[i] = GC.CollectionCount(i);
            }

            // 3.
            Stopwatch watch = new Stopwatch();
            watch.Start();
            ulong cycleCount = GetCycleCount();
            for (int i = 0; i < iteration; i++) action();
            ulong cpuCycles = GetCycleCount() - cycleCount;
            watch.Stop();

            // 4.
            Console.ForegroundColor = currentForeColor;
            Console.WriteLine("\tTime Elapsed:\t" + watch.ElapsedMilliseconds.ToString("N0") + "ms");
            Console.WriteLine("\tCPU Cycles:\t" + cpuCycles.ToString("N0"));

            // 5.
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                int count = GC.CollectionCount(i) - gcCounts[i];
                Console.WriteLine("\tGen " + i + ": \t\t" + count);
            }

            Console.WriteLine();
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetCurrentThread();

        private static ulong GetCycleCount()
        {
            ulong cycleCount = 0;
            QueryThreadCycleTime(GetCurrentThread(), ref cycleCount);
            return cycleCount;
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool QueryThreadCycleTime(IntPtr threadHandle, ref ulong cycleTime);
    }

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