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
            UpFileSingleTest.CopyBigFile("D:\\Desktop\\泰斗破坏神素材完整\\视频\\003-素材介绍，导入素材，开始界面的制作.mp4", "D:\\Desktop\\2.mov", 20);
            Console.Read();
        }
    }
}
