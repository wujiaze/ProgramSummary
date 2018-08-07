using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogDemo4
{
    class Program
    {
        static void Main(string[] args)
        {
            LogHelper.LogDebug("你好！");
            LogHelper.LogInfo("你好！");
            LogHelper.LogWarn("你好！");
            LogHelper.LogError("你好！");
            LogHelper.LogFatal("你好！");
            Console.ReadLine();
        }
    }
}
