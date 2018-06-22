using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DirectoryTest
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Path静态类 */
            string directPath = Path.Combine("D:\\", "Desktop", "MyDirectory"); // 根据操作系统类型，连接字符串得到路径
            string targetDirectPath = Path.Combine("D:\\", "Desktop", "Target");
            Path.GetFileName(directPath);       // 根据路径，返回文件名及其扩展名
            Path.GetDirectoryName(directPath);  // 根据路径，返回目录名



            // 测试总结：静态类用于简单的目录操作，比如创建，移动，删除。
            // 但是，若是对目录采取的操作比较多，建议使用实例类 DirectoryInfo，这样就只创建了一份实例,使用静态会多次搜寻，相比较更耗性能


            /* 目录静态类 Directory */

            DirectoryInfo directort = Directory.CreateDirectory(directPath);    // 创建目录，若存在则不执行
            bool exits =  Directory.Exists(directPath);                         // 判断是否存在目录
            string[] dirArr = Directory.GetDirectories(directPath);             // 获取指定目录下的所有子目录（不包括自身）
            string[] fileArr = Directory.GetFiles(directPath);                  // 获取指定目录下的所有文件
            Directory.Move(directPath, targetDirectPath);                       // 移动目录（还可以用来重命名目录）：实际上是创建一个新目录，移动源目录的内容，删除源目录。 注意：新建的目录不能已存在
            Directory.Delete(targetDirectPath);                                 // 只删除空目录
            //Directory.Delete(targetDirectPath, true);                          // 删除目录下的所有子目录和文件


            /* FileSystemInfo 抽象类 是 DirectoryInfo 和 FileInfo 的基类 */
            /* 目录实例类 DirectoryInfo */   
            // DirectoryInfo   内部的方法跟 静态类 Directory 类似

            DirectoryInfo di = new DirectoryInfo(directPath);       // 根据路径创建实例
            di.Create();                                            // 创建目录,若存在则不创建    
            Console.WriteLine(di.FullName);                         // 目录的完整路径名字
            Console.WriteLine(di.Exists);                           // 目录是否存在
            FileInfo[] fiArray = di.GetFiles();                     // 获取文件列表
            DirectoryInfo[] diArray = di.GetDirectories();          // 获取当前目录的子目录列表
            FileSystemInfo[] fsiArray = di.GetFileSystemInfos();    // 获取当前目录的子目录和文件列表
            di.MoveTo(targetDirectPath);                            // 不仅仅是目录移动，对应目录的 DirectoryInfo 实例也对应新的目录
            di.Refresh();                                           // 刷新对象的状态
            di.Delete();                                            // 删除空目录
            //di.Delete(true);                                        // 删除非空目录





            /* 流 */
            Console.ReadLine();
        }
    }
}
