/*
 *      Environment：提供有关当前环境和平台的信息以及操作它们的方法
 *
 */
using System;

namespace EnvironmentTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // CommandLine              程序名称 + 启动参数 args
            Console.WriteLine("CommandLine  " + Environment.CommandLine);
            // CurrentDirectory         程序的完全目录
            Console.WriteLine("CurrentDirectory " + Environment.CurrentDirectory);
            // CurrentManagedThreadId   当前托管线程Id
            Console.WriteLine("CurrentManagedThreadId   " + Environment.CurrentManagedThreadId);
            // ExitCode                 程序退出时，返回给操作系统的退出代码，0表示正常退出，条件：Main 函数的返回类型是 int
            Console.WriteLine("ExitCode " + Environment.ExitCode);
            // HasShutdownStarted       true:表示程序正在关闭，公共语言运行时CLR正在关闭
            Console.WriteLine("HasShutdownStarted   " + Environment.HasShutdownStarted);
            // Is64BitOperatingSystem   是否64为系统
            Console.WriteLine("Is64BitOperatingSystem   " + Environment.Is64BitOperatingSystem);
            // Is64BitProcess           是否是64为进程
            Console.WriteLine("Is64BitProcess   " + Environment.Is64BitProcess);
            // MachineName              计算机名字
            Console.WriteLine("MachineName  " + Environment.MachineName);
            // NewLine                  此环境下的换行字符串，对于非 Unix 平台为包含“\r\n”的字符串，对于 Unix 平台则为包含“\n”的字符串
            Console.WriteLine("NewLine  " + Environment.NewLine);
            // OSVersion                操作系统版本信息
            Console.WriteLine("OSVersion    " + Environment.OSVersion.VersionString);
            // ProcessorCount           处理器核数 * 线程数  4*2 =8    
            Console.WriteLine("ProcessorCount   " + Environment.ProcessorCount);
            // StackTrace               执行此方法进行的 堆栈跟踪信息，时间的逆序
            Console.WriteLine("StackTrace   " + Environment.StackTrace);
            // SystemDirectory          系统文件的目录
            Console.WriteLine("SystemDirectory  " + Environment.SystemDirectory);
            // SystemPageSize           系统内存页的字节数：不知道什么用
            Console.WriteLine("SystemPageSize   " + Environment.SystemPageSize);
            // TickCount                距离上次启动到现在的毫秒数
            Console.WriteLine("TickCount   " + Environment.TickCount / 1000f / 3600f);
            // UserDomainName           当前用户的网络域名
            Console.WriteLine("UserDomainName   " + Environment.UserDomainName);
            // UserInteractive          当前进程是否是交互模式:一般为没有图形界面与用户交互
            Console.WriteLine("UserInteractive  " + Environment.UserInteractive);
            // UserName                 当前用户名
            Console.WriteLine("UserName " + Environment.UserName);
            // Version                  当前公共语言运行时的版本号
            Console.WriteLine("Version  " + Environment.Version);
            // WorkingSet               当前程序所占用的物理内存
            Console.WriteLine("WorkingSet   " + Environment.WorkingSet / 1024 / 1024);
            // ExpandEnvironmentVariables  根据环境变量名或系统变量名，得到变量名对应的值或者路径
            Console.WriteLine(Environment.ExpandEnvironmentVariables("%OneDrive%"));    // 用户变量
            Console.WriteLine(Environment.ExpandEnvironmentVariables("%JAVA_HOME%"));   // 系统变量

            // GetCommandLineArgs       返回包含当前进程的命令行自变量的字符串数组
            string[] strs = Environment.GetCommandLineArgs();
            foreach (string str in strs)
            {
                Console.WriteLine("CommandLineArgs  " + str);
            }
            // GetLogicalDrives         当前计算机的逻辑驱动器(即常说的 C盘D盘)
            string[] dirves = Environment.GetLogicalDrives();
            foreach (string dirve in dirves)
            {
                Console.WriteLine("dirve    " + dirve);
            }
            // GetFolderPath        获取由指定枚举的系统特殊文件夹的路径
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //返回Windows中一组特定的文件夹(系统定义好的)的目录
            Console.WriteLine("MyDocuments  path  " + folder);

            // EnvironmentVariable  环境变量
            GetPathByEnvir();
            //Environment.Exit(0);
            //Environment.FailFast("程序非正常退出");
            Console.Read();
        }


        /*
         *      环境变量：
         *      存储：用户变量
         *            系统变量
         *            进程变量
         *      GetEnvironmentVariable  默认从当前进程中寻找环境变量，进程中的环境变量：有 系统变量 、用户变量、父进程中的环境变量组成
         *      SetEnvironmentVariable  默认在进程中设置环境变量，    仅在进程中有效，进程结束就失效了
         *      若想修改、添加、保存      需要具体指出 EnvironmentVariableTarget.User 或 EnvironmentVariableTarget.Machine
         *
         */
        private static void GetPathByEnvir()
        {
            //Environment.SetEnvironmentVariable("A", "1");     // 设置默认的环境变量（即 Process）
            //string a = Environment.GetEnvironmentVariable("A"); // 获取默认的环境变量
            //Console.WriteLine("A    " + a);

            //Environment.SetEnvironmentVariable("A", null);    // 设置为null 表示删除环境变量
            //a = Environment.GetEnvironmentVariable("A");
            //Console.WriteLine("A    " + a);

            //Environment.SetEnvironmentVariable("B", "2", EnvironmentVariableTarget.User);  // 设置 环境变量的用户变量
            //string path1 = Environment.GetEnvironmentVariable("B", EnvironmentVariableTarget.User);      // 返回
            //Console.WriteLine("B     " + path1);

            //Environment.SetEnvironmentVariable("C", "3", EnvironmentVariableTarget.Process);  // 设置 进程变量
            //string path2 = Environment.GetEnvironmentVariable("C", EnvironmentVariableTarget.Process);
            //Console.WriteLine("C     " + path2);

            //Environment.SetEnvironmentVariable("D", "4", EnvironmentVariableTarget.Machine); // 设置 环境变量的系统变量，需要管理员权限
            //string path3 = Environment.GetEnvironmentVariable("D", EnvironmentVariableTarget.Machine);
            //Console.WriteLine("D     " + path3);


            // 从进程中获取全部的环境变量(父进程、用户环境变量、系统环境变量),可以设置 EnvironmentVariableTarget
            //foreach (DictionaryEntry dictionaryEntry in Environment.GetEnvironmentVariables())
            //{
            //    Console.WriteLine("{0}={1}", dictionaryEntry.Key, dictionaryEntry.Value);
            //}
        }


    }
}
