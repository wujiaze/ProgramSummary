using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace asyncTest
{
    // 异步方法定义：若在方法未执行完毕，就返回调用方法的称为异步方法
    // async/await 是同一个线程中的异步方法
    // 使用场景总结：适用于在后台完成的不相关的又耗时的小任务，比如：（部分）UI的绘制，（部分）文件的读写
    // ********* 相比于Unity的协程，优点在于：可以在需要的时候阻塞线程等待结果，协程无法控制何时获取返回值*******
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://baidu.com";
            /*  异步方法的三种返回类型和一些注意事项  */
            ReturnVoidAsync(url);                                           // 术语：调用并忘记，即调用了异步方法，却不需要与之交互
            Console.WriteLine("调用方法继续执行其他代码1");

            Task oneTask = ReturnTaskAsync(url);                            // 调用异步方法，遇见 await 立即返回
                                                                            // 使用场景：不需要返回值，但是需要确定异步方法的状态时使用
            Console.WriteLine("调用方法继续执行其他代码2");
            Console.WriteLine("oneTask 是否完成：" + oneTask.IsCompleted);
                    
            Task<int> twoTask = ReturnIntAsync(url);                        // 使用场景：需要返回值和异步方法状态
            Console.WriteLine("调用方法继续执行其他代码3");
            Console.WriteLine("twoTask 是否完成：" + twoTask.IsCompleted);
            Console.WriteLine("Result: " + twoTask.Result);                 // 当调用方法需要用到异步方法的结果时，采用 Result 属性
                                                                            // 此时，如果经过 执行其他代码 ，异步方法已经完成，则直接获取结果；如果 异步方法还未完成，则调用方法阻塞在这里，等待异步方法的结果
                                                                            //所以，结果就是异步方法可以提高程序效率，但不能无限制的提升效率

            /*  异步方法的控制流和多个await  */
            Console.WriteLine("------------------------");

            Task<int> CountTask = CountCharAsync(url);
            Console.WriteLine("调用方法继续执行其他代码4");
            Console.WriteLine("Result: " + CountTask.Result);

            Task<int> MutilTask = MultiAwaitAsync(url);
            Console.WriteLine("调用方法继续执行其他代码5");
            Console.WriteLine("Result: " + MutilTask.Result);

            /* BCL中的异步方法和自定义awaitable类型 */
            Console.WriteLine("------------------------");

            Task<int> BCLTask = ReturnBCLAsync(url);
            Console.WriteLine("调用方法继续执行其他代码6");

            ReturnActionAsync();
            Console.WriteLine("调用方法继续执行其他代码7");

            Task<int> FuncIntTask = ReturnFuncIntAsync();
            Console.WriteLine("调用方法继续执行其他代码7");
            Console.WriteLine("Result " + FuncIntTask.Result);

            /* Lambda和匿名方法作为异步对象 */
            Task voidLambdaTask = ReturnVoidLambda(url);
            Console.WriteLine("调用方法继续执行其他代码8");
            Task<int> FuncIntLambdaTask = ReturnFuncIntLambda(url);
            Console.WriteLine("调用方法继续执行其他代码9");
            Console.WriteLine("Result " + FuncIntLambdaTask.Result);

            /* 异步方法的取消 */
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            Task<int> Tokentask = TokenAsync(url, token);
            cts.Cancel();
            Console.WriteLine("调用方法继续执行其他代码10");
            Console.WriteLine("Result " + Tokentask.Result);

            /* 调用方法的同步等待 */
            Task<int> waitTask = WaitAsync(url);
            Console.WriteLine("调用方法继续执行其他代码11");
            waitTask.Wait();                                 // 等待 waitTask 任务完成，才能执行后面的语句
            Task.WaitAll();                                  // 等待 任务组里所有的任务完成，才能执行后面的语句
            Task.WaitAny();                                  // 等待 任务组的任一任务完成，就执行后面的任务
                                                             //另外还有两种重载：1、设置超时时间 2、设置取消令牌
            Console.WriteLine("调用方法继续执行其他代码12");
            Console.WriteLine("Result " + waitTask.Result);

            /* 异步方法的异步等待 */
            Task<int> AsyncWaitTask =
                AsyncWaitAsync("https://msdn.microsoft.com/zh-cn/library/system.timers.timer.close(v=vs.110).aspx");
            Console.WriteLine("调用方法继续执行其他代码13");
            Console.WriteLine("是否完成： " + AsyncWaitTask.IsCompleted);
            Console.WriteLine("Result " + AsyncWaitTask.Result);

            /* 异步方法的延迟触发 */
            Console.WriteLine("---------------------------");
            DelayAsync();
            Console.WriteLine("调用方法继续执行其他代码14");


            /* 异步方法的异常处理 */
            Task ExceptionTask = ExceptionAsync();
            ExceptionTask.Wait();
            Console.WriteLine("Status " + ExceptionTask.Status);            // RanToCompletion：原因 1、Task 没有取消 2、没有未处理的异常
            Console.WriteLine("IsFaulted: " + ExceptionTask.IsFaulted);     // False:原因是没有未处理的异常，因为异常都处理了

            ExceptionNewThreadAsync();


            Console.ReadLine();
        }

        #region 异步方法的三种返回类型和一些注意事项

        // 返回值为 void
        private static async void ReturnVoidAsync(string url)               // 异步方法的参数不能带有 ref out ；异步方法关键字 async
        {
            // 异步方法应该以 Async 结尾
            Console.WriteLine("异步方法 ReturnVoidAsync()----await之前的部分");
            WebClient wc1 = new WebClient();
            string str = await wc1.DownloadStringTaskAsync(new Uri(url));   // 使用BCL中定义方法，来做测试，这是同一个线程的异步，没有新线程
            Console.WriteLine("await 部分执行完毕 ---进入后续部分");
        }

        //  返回值为 Task
        private static async Task ReturnTaskAsync(string url)
        {
            Console.WriteLine("异步方法 ReturnTaskAsync()----await之前的部分");
            WebClient wc1 = new WebClient();
            string str = await wc1.DownloadStringTaskAsync(new Uri(url));
            Console.WriteLine("await 部分执行完毕 ---进入后续部分");
        }

        // 返回值为 Task<T>
        private static async Task<int> ReturnIntAsync(string url)
        {
            Console.WriteLine("异步方法 ReturnIntAsync()----await之前的部分");
            WebClient wc1 = new WebClient();
            string str = await wc1.DownloadStringTaskAsync(new Uri(url));
            Console.WriteLine("await 部分执行完毕 ---进入后续部分");
            return str.Length;                                              // 返回值是 Task<T> 的 T 类型或可以转换为T类型的类型
        }

        #endregion

        #region 异步方法的控制流和多个await

        private static async Task<int> CountCharAsync(string url) // 控制流:调用方法调用异步方法，未遇见 await 时，正常的执行
        {
            Console.WriteLine("异步方法 CountCharAsync()----await之前的部分"); // await 之前的应该只包含 无需长时间处理 的代码
            WebClient wc1 = new WebClient();
            string str =
                await wc1.DownloadStringTaskAsync(
                    new Uri(url)); // 当遇见第一次 await 时，会执行三个步骤：1、创建空闲任务 ：即Task用来执行 await后面的方法
                                   //                                     2、创建后续部分 ：即将后续部分“包”起来，直到 空闲任务 完成之后再执行
                                   //                                     3、立即返回调用方法中，继续执行调用方法中的流程
            Console.WriteLine("await 部分执行完毕 ---进入后续部分");
            return str.Length;      // 后续部分：需要设置 Task 的 Result 属性，即返回 T类型;当调用方法需要使用时，可以通过 Result 获取
        }

        private static async Task<int> MultiAwaitAsync(string url)
        {
            Console.WriteLine("异步方法 MultiAwaitAsync()----await之前的部分");
            WebClient wc1 = new WebClient();
            string str = await wc1.DownloadStringTaskAsync(new Uri(url));
            Console.WriteLine("第一个 await 执行完毕，进入第二个 await");
            string str2 =
                await wc1.DownloadStringTaskAsync(new Uri(url));        // 当进入后续部分又遇见 await 时，会执行两个步骤：1、创建空闲任务  2、创建后续部分
            Console.WriteLine("第二个 await 执行完毕 ---进入后续部分");
            return str2.Length;
        }


        #endregion

        #region BCL中的异步方法和自定义awaitable类型

        // BCL中的异步方法
        private static async Task<int> ReturnBCLAsync(string url)
        {
            Console.WriteLine("异步方法 ReturnBCLAsync()----await之前的部分");
            WebClient wc1 = new WebClient();
            string str = await wc1.DownloadStringTaskAsync(new Uri(url)); // await 后面的方法，必须是awaitable 类型
            Console.WriteLine(
                "await 部分执行完毕 ---进入后续部分"); // awaitable 类型是指: 包含以下成员 GetAwaiter() IsComplete{get;} OnCompleted(Action); void GetResult()/T GetResult() 的类型
            return str.Length; // 不过，一般来说，我们不需要构建自己的 awaitable ，而是采用 Task 类的方法
            // 其余一些，就采用BCL内部的awaitable 方法 ，例如 DownloadStringTaskAsync ，这是在同一线程中执行委托方法
        }

        // 自定义awaitable类型
        // *******特别注意一点：Task.Run 是新开线程来执行委托方法*******
        private static async void ReturnActionAsync()
        {
            Console.WriteLine("异步方法 ReturnActionAsync()----await之前的部分");
            await Task.Run(new Action(ActionMethod)); // Run 的第一种委托 Action ，无返回值，所以异步方法的返回值可以 设置为 void 或 Task
            Console.WriteLine("await 部分执行完毕 ---进入后续部分");
        }

        public static void ActionMethod()
        {
            Console.WriteLine("自己的 ActionMethod 委托方法，由于是新开线程，所以打印顺序不定");
        }

        private static async Task<int> ReturnFuncIntAsync()
        {
            Console.WriteLine("异步方法 ReturnFuncIntAsync()----await之前的部分");
            int length = await Task.Run(new Func<int>(FuncReturnIntMethod)); // Run 的第二种委托 Func<TResult>,无参有返
            Console.WriteLine("await 部分执行完毕 ---进入后续部分");
            return length;
        }

        public static int FuncReturnIntMethod()
        {
            Console.WriteLine("自己的 FuncReturnIntMethod 委托方法，由于是新开线程，所以打印顺序不定");
            return 1;
        }
        // 第三种委托  Func<Task>  和 第四种委托 Func<Task<TResult>>  比较少用，而且就是调用新的异步方法

        #endregion

        #region Lambda和匿名方法作为异步对象

        // 上面的 自定义awaitable类型 无法解决 参数传入，可以使用Lambda 表达式来完成
        private static async Task ReturnVoidLambda(string url) // Lambda表达式的 Action             
        {
            Console.WriteLine("异步方法 ReturnVoidLambda()----await之前的部分");
            await Task.Run(() => { Console.WriteLine("awaitable 方法中传入参数 str： " + url); });
            Console.WriteLine("await 部分执行完毕 ---进入后续部分");
        }

        private static async Task<Int32> ReturnFuncIntLambda(string url) // Lambda 表达式的 Func<TResult>
        {
            Console.WriteLine("异步方法 ReturnFuncIntLambda()----await之前的部分");
            int result = await Task.Run(() =>
            {
                Console.WriteLine("awaitable 方法中传入参数 str： " + url);
                return url.Length;
            });
            Console.WriteLine("await 部分执行完毕 ---进入后续部分");
            return result;
        }




        #endregion

        #region 异步方法的取消

        // BCL的异步方法 和 Task.Run 方法 都有取消任务的 令牌（token），这里以 Task.Run 为例
        private static async Task<int>
            TokenAsync(string url, CancellationToken token) // 注意:本方法仅仅是为了展示 取消异步方法的 操作，实际应用有很大的不同
        {
            int result = await Task.Run(() =>
            {
                Console.WriteLine("awaitable 方法中传入参数 str： " + url);
                return 1;
            });
            Thread.Sleep(1000); // 注释这条，则令牌还未取消，不执行 -1； 不注释这条，则令牌取消，执行-1
            if (token.IsCancellationRequested)
            {
                return -1;
            }

            return result;
        }


        #endregion

        #region 调用方法的同步等待

        private static async Task<int> WaitAsync(string url)
        {
            WebClient wc1 = new WebClient();
            string str = await wc1.DownloadStringTaskAsync(new Uri(url));
            return str.Length;
        }

        #endregion

        #region 异步方法的异步等待

        private static async Task<int> AsyncWaitAsync(string url)
        {
            WebClient wc1 = new WebClient();
            Task<string> tempTask = wc1.DownloadStringTaskAsync(new Uri(url));
            await Task.WhenAll(
                tempTask); // await：异步等待 Task.WhenAll()：异步等待所有任务完成  合起来就是 调用方法异步等待 异步方法异步等待任务完成（不会阻塞调用线程和异步方法）
            //Task.WaitAny();
            Console.WriteLine("AsyncWaitAsync 方法 异步等待");
            return tempTask.IsCompleted ? 0 : -1;
        }


        #endregion

        #region 异步方法的延迟触发

        //不会阻塞调用线程
        private static async void DelayAsync()
        {
            Console.WriteLine("异步方法 DelayAsync()----await之前的部分");
            await Task.Delay(3000);                                   // 还可以添加 取消令牌
            Console.WriteLine("await 部分执行完毕 ---进入后续部分");
        }

        #endregion


        #region 异步方法的异常处理
        // 同一个线程中，异步方法的异常捕获
        private static async Task ExceptionAsync()
        {
            Console.WriteLine("异步方法 ExceptionAsync()----await之前的部分");
            try
            {
                await Task.Delay(500);
                throw new Exception("任务并行编码中产生异常");
            }
            catch (Exception e)
            {
                Console.WriteLine("处理异常： " + e.Message);
            }

            Console.WriteLine("await 部分执行完毕 ---进入后续部分");
        }
        // 新线程中捕获异常并处理
        private static async void ExceptionNewThreadAsync()
        {
            Console.WriteLine("异步方法 ExceptionNewThreadAsync()----await之前的部分");
            await Task.Run(new Action(ExceptionMethod));
            Console.WriteLine("await 部分执行完毕 ---进入后续部分");
        }

        private static void ExceptionMethod()
        {
            try
            {
                throw new Exception("任务并行编码中产生的未知异常");
            }
            catch (Exception e)
            {
                Console.WriteLine("处理异常： " + e.Message);
            }
        }

        // 新线程的异常在主线程中处理 TODO 等学好了多线程编程，再看
       

        

       

        #endregion

    }
}
