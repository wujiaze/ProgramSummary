using System;
namespace Log4netDemo
{
    class Program
    {
       
        private static readonly log4net.ILog myLogger = log4net.LogManager.GetLogger("People_A");
        private static readonly log4net.ILog myLogger2 = log4net.LogManager.GetLogger("People_B");
        static void Main(string[] args)
        {
            myLogger2.Warn("通过Nmae来获取不同的记录器");
            // 初始化log
            //log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));// 文件路径在bin/DeBug/log4net.config
            myLogger.Warn("这是一个警告日志");
            myLogger.Info("单击了按钮");
            myLogger.Debug("用Log4Net写入数据库日志");
            myLogger.Error("这是一个错误日志");
            myLogger.Fatal("这是一个致命的错误日志");
           
            Console.Read();
        }
    }
}
