using System;
namespace TimerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //   System.Threading.Timer ： 暂时不是很懂 ？？？？？？？？
            //System.Threading.Timer tm = new System.Threading.Timer();
           




            //   System.Timers.Timer :在设定的间隔之后触发事件，可以选择重复事件
            System.Timers.Timer aTimer = new System.Timers.Timer(); // 
            aTimer.Interval = 1000; // 设置 atimer 触发的时间间隔是 1000ms
            aTimer.Elapsed += AtimerEvent; // 设置 aTimer 触发的事件
            aTimer.Enabled = true; // 设置 aTimer.Elapsed 是否可用
            aTimer.AutoReset = false; // 设置事件触发的模式 true：循环 false:单次
            Console.WriteLine("{0:HH:mm:ss.fffff}",DateTime.Now);
            aTimer.Start(); //  开始执行 aTimer 方法 :间隔 1000 毫秒开始执行事件
            //aTimer.Stop(); // 暂停Atimer 方法
            //aTimer.Close();// 释放 Atimer 资源
            //aTimer.Dispose();// 释放 Atimer 的 Component 资源
            Console.Read();
        }

        public static void AtimerEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("{0:HH:mm:ss.ffff}",e.SignalTime);
            Console.WriteLine("lalalalala");
        }
    }
}
