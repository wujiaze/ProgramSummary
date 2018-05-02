using System;
using System.IO;


namespace FileInfoTest
{
    class Program
    {
        // Path 类：https://msdn.microsoft.com/zh-cn/library/system.io.path(v=vs.110).aspx
        // 主要对文件的路径，文件的文件名进行查找、更新、判断等操作
        // 一般来说，路径的字符串前面都加上 @ 符号，没有意义但看的清楚
        static void Main(string[] args)
        {
            string path = @"D:/Desktop/fileinfo.txt";
            FileInfo fi1 = new FileInfo(path); // 打开或创建新文件

            // 对fi1文件初始化，再写入
            using (StreamWriter sw = fi1.CreateText())
            {
                sw.WriteLine("Hello");
                sw.WriteLine("And");
                sw.WriteLine("Welcome1");
            }
            // 对fi1文件，在末尾追加写入
            using (StreamWriter sw = fi1.AppendText())
            {
                sw.WriteLine("Hello");
                sw.WriteLine("And");
                sw.WriteLine("Welcome1");
            }
            Console.Read();
        }
    }
}
