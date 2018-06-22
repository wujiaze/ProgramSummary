using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace Begin_End_Invoke_Test
{
    // 适用场景：新开线程，处理所有任务，新线程可以返回给主线程（回调）； 主线程 可以阻塞等待 新线程的 结果。     即 基本所有的情况都适用
    class Program
    {
        static void Main(string[] args)
        {
            // 前提：1、委托对象 2、委托对象的调用列表有且只有一个方法(以下就称为 引用方法)  就可以采用异步调用来执行这个引用方法
            // BeginInvoke：异步方法
            // BeginInvoke : 1、从线程池获取一个线程，用来执行引用方法
            //               2、参数列表的组成: 1、引用方法需要的参数
            //                                2、两个额外的参数-----callback参数和state参数     --回调模式中解释   
            //               3、返回一个实现 IAsyncResult 接口的对象的引用，这个引用在内部实际是 AsyncResult 类的对象的引用。这个引用包含了异步方法的当前状态和信息。
            //               4、调用 BeginInvoke 方法，BeginInvoke 立即返回，调用线程继续执行之后的程序
            // EndInvoke : 1、通过 IAsyncResult 接口的引用，找到 异步方法 相关联的线程
            //             2、当调用 EndInvoke 时，若新线程中的引用方法 已经执行完毕：1、清理线程并释放线程资源(所以有BeginInvoke,就必须有EndInvoke)
            //                                                                    2、将引用方法的返回值 设置为 EndInvoke 方法的返回值
            //             3、当调用 EndInvoke 时，若新线程中的引用方法 还未执行完毕：1、调用 EndInvoke 方法的线程会停止并等待 （这样虽然也会阻塞线程，但是比直接调用委托要提早执行后面的步骤）就跟async/await是一样的
            //                                                                    2、等待结果，然后 清理并释放线程资源，设置返回值
            //             4、 如果委托对应的引用方法出现异常，则在调用 EndInvoke 方法时，抛出异常，但是这个异常需要在引用方法中处理


            // 模式1： 等待--直到完成模式 ( wait-until-done ) 
            MyDel myDel1 = new MyDel(Sum);
            IAsyncResult iar1 = myDel1.BeginInvoke(1, 2, null, null);       // 在此模式中，AsyncCallBack 和 objec 为null 就可以了
            Console.WriteLine("主线程继续执行后面的程序");
            Console.WriteLine("当需要异步方法中的结果时，调用EndInvoke");
            long result1 = myDel1.EndInvoke(iar1);                          // 等待 引用方法 完成
            Console.WriteLine("After EndInvoke  result:{0} ", result1);


            // 模式2：轮询模式(polling)
            MyDel2 myDel2 = Sum2;
            IAsyncResult iar2 = myDel2.BeginInvoke(5, null, null);
            Console.WriteLine("主线程继续执行后面的程序");
            while (!iar2.IsCompleted)
            {
                Console.WriteLine("Not Done");
            }
            Console.WriteLine("完成引用方法");
            string result2 = myDel2.EndInvoke(iar2);
            Console.WriteLine("Result:{0}", result2);


            // 模式3：回调模式(CallBack)
            // 解释参数 AsyncCallBack
            MyDel3 myDel3 = Sum3;
            myDel3.BeginInvoke(5, CallBack, null);      // 由于使用了回调函数，所以这个就不需要对返回值进行处理
            Console.WriteLine("主线程继续执行后面的程序");
            // 解释参数 object：这个参数实际上是预留的，可以传输我们想要传入回调方法的参数
            // 1、一般的对象 
            string strobj = "1111";
            myDel3.BeginInvoke(5, CallBack2, strobj);

            //2、当没有什么需要传入的参数时，可以将委托对象传入，这样回调可以方便一些
            myDel3.BeginInvoke(5, CallBack3, myDel3);



            // 实际应用时的特殊 ref
            MyDelRef myDelRef = Add;
            myDelRef.BeginInvoke(ref refX, RefCallBack, null);
            MyDelOut myDelOut = Out;
            myDelOut.BeginInvoke(out outY, OutCallBack, null);

            Console.Read();
        }

    


        #region wait-until-done 模式
        delegate long MyDel(int first, int second);
        static long Sum(int x, int y)
        {
            Console.WriteLine("---------------新线程执行Sum方法-------------------");
            return x + y;
        }
        #endregion


        #region polling 模式
        delegate string MyDel2(int first);
        static string Sum2(int x)
        {
            Console.WriteLine("---------------新线程执行Sum2方法-------------------");
            Thread.Sleep(1);
            return x++.ToString();
        }
        #endregion

        #region callback 模式
        delegate string MyDel3(int first);
        static string Sum3(int first)
        {
            Console.WriteLine("---------------新线程执行Sum3方法-------------------");
            Thread.Sleep(10);
            return first.ToString();
        }

        private static void CallBack(IAsyncResult ar)
        {
            Console.WriteLine("-------执行异步回调方法---------");
            AsyncResult re = ar as AsyncResult;                     // IAsyncResult 接口的引用在内部是 AsyncResult 类的引用
            MyDel3 del = re.AsyncDelegate as MyDel3;                 // 通过 AsyncResult 类对象，获取委托对象
            string result = del.EndInvoke(ar);                       // 委托对象，调用 EndInvoke 等待引用方法的完成 
            Console.WriteLine("----EndInvoke完成----");               // 此时，需要注意的是：结果是新线程的结果，主线程的UI需要使用的话，还需要转一个委托
        }


        private static void CallBack2(IAsyncResult ar)
        {
            Console.WriteLine("-------执行异步回调方法---------");
            Console.WriteLine("传入的参数" + ar.AsyncState as string); // 传入的参数，在回调函数中 以AsyncState(object) 形式展现，具体使用时，需要强制转换
            AsyncResult re = (AsyncResult)ar;
            MyDel3 del = (MyDel3)re.AsyncDelegate;
            string result = del.EndInvoke(ar);                                       
            Console.WriteLine("----EndInvoke完成----");
        }
        private static void CallBack3(IAsyncResult ar)
        {
            Console.WriteLine("-------执行异步回调方法---------");
            MyDel3 del = ar.AsyncState as MyDel3;                                    // 一般使用时，就采用这一步即可
            AsyncResult re = (AsyncResult)ar;
            MyDel3 del2 = (MyDel3)re.AsyncDelegate;
            Console.WriteLine("两种方法获取的委托是否是同一个：" + del.Equals(del2));
            string result = del.EndInvoke(ar);
            Console.WriteLine("----EndInvoke完成----");
        }
        #endregion

        #region 带有ref out 的引用方法

        static int refX = 1;
        static int outY;
        // ref
        delegate int MyDelRef(ref int x);
        static int Add(ref int x)
        {
            x++;
            return x;
        }
        private static void RefCallBack(IAsyncResult iar)
        {
            AsyncResult ar = (AsyncResult)iar;
            MyDelRef myDelRef = (MyDelRef)ar.AsyncDelegate;
            myDelRef.EndInvoke(ref refX, iar);
        }
        //out
        delegate int MyDelOut(out int y);
        static int Out(out int y)
        {
            y = 0;
            return 4;
        }
        private static void OutCallBack(IAsyncResult iar)
        {
            AsyncResult ar = (AsyncResult) iar;
            MyDelOut myDelRef = (MyDelOut) ar.AsyncDelegate;
            int result = myDelRef.EndInvoke(out outY, iar);
        }
        #endregion
    }
}
