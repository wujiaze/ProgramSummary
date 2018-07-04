using System;
using System.IO;
using System.Security.AccessControl;

namespace UpFileStreamTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("测试***************");
            CreateFileConfig config = new CreateFileConfig() { IsAsync = true, CreateUrl = "D:\\Desktop\\A.txt" };
            CopyFileConfig copyFileConfig = new CopyFileConfig(true, "D:\\Desktop\\A.txt", "D:\\Desktop\\B.txt");
            UpFileSingleTest.Create(config);
            UpFileSingleTest.Copy(copyFileConfig);
            UpFileSingleTest.CopyBigFile("E:\\电影\\捉丶妖丶记 HD1080P.mp4", "D:\\Desktop\\2.mp4", 10,1024*100);
            Console.WriteLine("完成");
            Console.Read();
        }
    }
}
