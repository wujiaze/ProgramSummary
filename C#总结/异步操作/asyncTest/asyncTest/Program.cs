using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace asyncTest
{
    class Program
    {
        static void Main(string[] args)
        {
            

            //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //sw.Start();
            
            ////sdfds2();
            ////Console.WriteLine("Stopwatch: " + sw.Elapsed.Milliseconds); // 一种测量方法运行时长的方法
            //Task<int> t1 = sdfds1();
            //t1.Wait();
            ////Add(t1.Result);  // 最大的问题：调用的方法中，需要用到异步的结果，不是也要等待吗？如何有一种智能的解决方法，跳出需要用到一步结果的方法，执行其他方法？todo
            //                 // 还有Unity中的协程方法，再看看，好像以前都是在协程中等待结果，再继续运行，外部调用方法中，是如何运行的，需要用到协程结果又是如何运行的
            //for (int i = 0; i < 6000000; i++) ;
            //Console.WriteLine("Stopwatch1: " + sw.Elapsed.Milliseconds); // 一种测量方法运行时长的方法
            //for (int i = 0; i < 6000000; i++) ;
            //Console.WriteLine("Stopwatch2: " + sw.Elapsed.Milliseconds); // 一种测量方法运行时长的方法
            //for (int i = 0; i < 6000000; i++) ;
            //Console.WriteLine("Stopwatch3: " + sw.Elapsed.Milliseconds); // 一种测量方法运行时长的方法
            //for (int i = 0; i < 6000000; i++) ;
            //Console.WriteLine("Stopwatch4: " + sw.Elapsed.Milliseconds); // 一种测量方法运行时长的方法
            //for (int i = 0; i < 6000000; i++) ;
            //Console.WriteLine("Stopwatch5: " + sw.Elapsed.Milliseconds); // 一种测量方法运行时长的方法
            //for (int i = 0; i < 6000000; i++) ;
            //Console.WriteLine("Stopwatch6: " + sw.Elapsed.Milliseconds); // 一种测量方法运行时长的方法
            //Add(t1.Result);
            //atAsync();
            
             // 异步得到的结果如何使用呢，何时有值？todo
            // 避免阻塞？todo 如何做到的
            //Task<int> re = Count("http://www.baidu.com", "https://msdn.microsoft.com/zh-cn/library/system.io.textreader(v=vs.110).aspx");
            //Console.WriteLine("111");
            //Console.WriteLine("Main: {0}Finished",re.IsCompleted?"":"Not");
            //Console.WriteLine("11111");
            //Console.WriteLine("Result{0}", re.Result);
           
            // todo 两种调用方法？区别

            //CancellationTokenSource cts1 = new CancellationTokenSource();
            //CancellationToken ct1 = cts1.Token;
            //Task<int> task1 = GetInt(ct1);

            //Thread.Sleep(3000);
            //cts1.Cancel();
            ////Console.WriteLine(task1.Result);
            //Console.WriteLine(ct1.IsCancellationRequested);
            //Console.WriteLine(task1.IsCompleted);


            int num1 = 1000;
            int num2 = 1000;
            Task<int> t1 = FindSeriesSum(num1);
            for (int i = 0; i < num2; i++)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("Sum{0}",t1.Result);

            Console.ReadLine();
        }

        public static async Task<int> sdfds1()
        {
            WebClient wc1 = new WebClient();
            string str = await wc1.DownloadStringTaskAsync(new Uri("http://baidu.com")); // 最里面的步骤是怎么样的?todo
            Console.WriteLine("11");                                                     // await后面的方法，如何定义的？todo 自己的方法一般采用 Task.Run
            return str.Length;
        }
        public static  int sdfds2()
        {
            WebClient wc1 = new WebClient();
            string str =  wc1.DownloadString(new Uri("http://baidu.com"));
            return str.Length;
        }

        public static void Add(int x)
        {
            x++;
            Console.WriteLine(x);
        }

        public static async Task atAsync()
        {
            WebClient wc1 = new WebClient();
            string str = await wc1.DownloadStringTaskAsync(new Uri("http://baidu.com"));
        }
        public static async Task<int> iwoAsync()
        {
            WebClient wc1 = new WebClient();
            int s = await Task.Run(new Func<int>(sdf));
            return s;
        }

        public static void sdf(int x)
        {
            x++;
        }
        public static int sdf()
        {
            return 1;
        }

        private static async Task<int> Count(string url1,string url2)
        {
            WebClient wc1 = new WebClient();
            WebClient wc2 = new WebClient();
            Task<string> task1 = wc1.DownloadStringTaskAsync(url1);
            Task<string> task2 = wc2.DownloadStringTaskAsync(url2);
            //Console.WriteLine("5555");
            //Task<string> task3 = GetStr();
            ////task3.Wait();
            //Console.WriteLine("000");
            //task2.Wait(1);
            //Console.WriteLine("34534534");
            //string str1 = await task2;

            //Console.WriteLine("222");
            //Console.WriteLine(task2.Result);
            //await task2;
            //Console.WriteLine("222");
            //string str2 = await task3;
            ////
            //Console.WriteLine("333");
            List<Task<string>> tasks = new List<Task<string>>();
            tasks.Add(task1);
            tasks.Add(task2);
            await Task.WhenAll(tasks);
            await task1;
            Console.WriteLine("T1{0}Finished", task1.IsCompleted ? "" : "Not");
            Console.WriteLine("T2{0}Finished", task2.IsCompleted ? "" : "Not");
            //Console.WriteLine("T3{0}Finished", task3.IsCompleted ? "" : "Not");
            //string str2 = await task2;
            //Console.WriteLine(task3.IsCompleted);
            ////string str3 = await task3;
            //Console.WriteLine(task3.IsCompleted);

            return 1; // todo
        }

        private static async Task<string> GetStr()
        {
            Console.WriteLine("1111111");
            int length = await Task.Run(new Func<int>(Loop));
            Console.WriteLine("GetStr");
            return length.ToString();
        }

        [DebuggerStepThrough]
        private static int Loop()
        {
            for (int i = 0; i < 1000000000; i++) ;
            return 2;
        }

        private static async Task<int> GetInt(CancellationToken ct)
        {
            if (ct.IsCancellationRequested)
            {
                Console.WriteLine("1111111");
                return 0;
            }
            await Task.Run(()=> cyc(ct),ct);
            return 1;
        }

        private static void cyc(CancellationToken ct)
        {
            Console.WriteLine("22222");
            for (int i = 0; i < 5; i++)
            {
                if (ct.IsCancellationRequested)
                {
                    Console.WriteLine("333");
                    return;
                }
                Thread.Sleep(1000);
                Console.WriteLine("44444");
            }
        }

        private static async void return1()
        {
            await Task.Run(() => Loop());
        }

        private static async Task<int> FindSeriesSum(int length)
        {
            int sum = 0;
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine("FindSeriesSum  "+i);
                if ( i % 10 == 0)
                {
                    Console.WriteLine("----------------------------------"+i);
                    await Task.Yield();
                }
            }
            return sum;
        }

    }
}
