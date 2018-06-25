using System;

namespace EnvironmentTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetDocumentFolder());
            GetPathByEnvir();
            Console.Read();
        }

        private static string GetDocumentFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //返回Windows中一组特定的文件夹(系统定义好的)
        }

        private static void GetPathByEnvir()
        {
            Environment.SetEnvironmentVariable("A", "1");     // 设置默认的环境变量（即 Process）
            string a = Environment.GetEnvironmentVariable("A"); // 获取默认的环境变量
            Console.WriteLine("A    " + a);
            Environment.SetEnvironmentVariable("A", null);    // 设置为null 表示删除环境变量
            a = Environment.GetEnvironmentVariable("A");
            Console.WriteLine("A    " + a);

            Environment.SetEnvironmentVariable("B", "2", EnvironmentVariableTarget.User);  // 设置 环境变量的用户变量
            string path1 = Environment.GetEnvironmentVariable("B", EnvironmentVariableTarget.User);      // 返回
            Console.WriteLine("B     " + path1);

            Environment.SetEnvironmentVariable("C", "3", EnvironmentVariableTarget.Process);  // 设置 进程变量
            string path2 = Environment.GetEnvironmentVariable("C", EnvironmentVariableTarget.Process);    
            Console.WriteLine("C     " + path2);

            Environment.SetEnvironmentVariable("D", "4", EnvironmentVariableTarget.Machine); // 设置 环境变量的系统变量，但是这里权限不够，TODO 如何获取权限
            string path3 = Environment.GetEnvironmentVariable("D", EnvironmentVariableTarget.Machine);     
            Console.WriteLine("D     " + path3);
        }
    }
}
