using System;

namespace TimerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // c# 总共有三个Timer类

            // 第一种 System.Threading.Timer 新开一个线程，定时执行回调函数 ，新线程由系统内部提供，简单且对资源要求不高
            // 下面采用最常用的构造函数
            // TimerCallBack : 回调函数
            // object ：即回调函数的传入参数，可以是任意需要传入的对象
            // duetime ：第一次开始回调函数的时间(ms)。 0：表示立即执行  System.Threading.Timeout.Infinite 表示 无限时间，即不会执行回调函数
            // period ：两次回调函数之间的间隔时间(ms)。 System.Threading.Timeout.Infinite 表示首次调用之后，不会在调用了，因为无限时间间隔
            Console.WriteLine("Start {0}", DateTime.Now);
            System.Threading.Timer timer1 = new System.Threading.Timer(MyCallBack, null, 0, 1000);  // TODO 可以用WaitHandle 类来控制何时结束 这就牵扯到 多线程 以后再说(自己也可以用int来判断，但是比较麻烦)
            Console.WriteLine("主线程继续执行1");
            System.Threading.Thread.Sleep(1500);
            Console.WriteLine("主线程继续执行2");
            timer1.Change(5000, 2000);          // 改变参数之后，重新开始调用回调函数
            //timer1.Dispose();                 // 在调用结束时，用来释放资源   



            // 第二种 System.Timers.Timer :在设定的间隔之后触发事件  基于服务器计时器功能的Timer，根据服务器系统时间进行运行的Timer(即可用于服务器)
            System.Timers.Timer aTimer = new System.Timers.Timer();  
            aTimer.Interval = 1000;                                         // 设置 atimer 触发的时间间隔是 1000ms
            aTimer.Elapsed += AtimerEvent;                                  // 设置 aTimer 触发的事件
            aTimer.AutoReset = true;                                        // 设置事件触发的模式 true：循环 false:单次
            Console.WriteLine("开始时间： {0:HH:mm:ss.fffff}", DateTime.Now);
            aTimer.Start();                                                 //  开始执行 aTimer 方法 :间隔 1000 毫秒开始执行事件
            Console.WriteLine("主线程继续执行");
            System.Threading.Thread.Sleep(2000);
            aTimer.Stop();                                                  // 暂停Atimer 方法
            System.Threading.Thread.Sleep(2000);
            aTimer.Start();
            

            // 第三种 System.Windows.Forms.Timer  这是一个必须和Windows窗体一起使用的Timer    TODO 以后用到WPF再说
            Console.Read();
        }
        
        #region System.Threading.Timer
        private static void MyCallBack(object state)
        {
            Console.WriteLine(state);
            Console.WriteLine("Current Time {0}", DateTime.Now);
        }
        #endregion

        #region System.Timers.Timer
        private static int index = 0;   
        public static void AtimerEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            index++;
            if (index == 15)
            {
                System.Timers.Timer temp = sender as System.Timers.Timer;   // 说明 传入的参数 sender 是本身
                temp.Close();                                               // 释放 Atimer 资源
                temp.Dispose();                                             // 释放 Atimer 的 Component 资源
            }
            Console.WriteLine("{0:HH:mm:ss.ffff}", e.SignalTime);
            Console.WriteLine("index ：" + index);
        }
        #endregion

    }
}
