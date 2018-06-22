using System;
using System.Threading;

namespace StopwatchTest
{
    class Program
    {
        // 用于测试 程序具体方法运行时长
        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch(); // 一种测量方法运行时长的方法
            sw.Start();
            Console.WriteLine("测试方法1运行");
            Thread.Sleep(10);
            Console.WriteLine(sw.Elapsed.Milliseconds);
            sw.Restart();
            Console.WriteLine("测试方法2运行");
            Thread.Sleep(10);
            Console.WriteLine(sw.Elapsed.Milliseconds);
            Console.Read();
        }
    }
}
