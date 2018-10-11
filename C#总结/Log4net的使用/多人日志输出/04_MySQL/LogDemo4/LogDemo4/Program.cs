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
            LogHelper.LogDebug(LogPeople.A, "你好！");
            LogHelper.LogInfo(LogPeople.A, "你好！");
            LogHelper.LogWarn(LogPeople.B, "你好！");
            LogHelper.LogError(LogPeople.A, "你好！");
            LogHelper.LogFatal(LogPeople.B, "你好！");
            Console.ReadLine();
        }
    }
}
