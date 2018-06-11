using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTest
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 测试总结 */
            // File 静态类用于简单的文件操作，比如创建文件，读写，移动。
            // 但是，若是对文件采取的操作比较多，建议使用实例类FileInfo，这样就只创建了一份实例,使用静态会多次搜寻，相比较更耗性能

            #region 文件静态类 File
            /* 文件静态类 File */
            string filepath = Path.Combine("D:\\", "Desktop", "filepath");          // 根据操作系统类型，连接字符串得到路径
            string targetPath = Path.Combine("D:\\", "Desktop", "targetPath");
            string Txtfilepath = Path.Combine("D:\\", "Desktop", "Txtfilepath.txt");
            string fileCopyPath = Path.Combine("D:\\", "Desktop", "fileCopyPath");
            FileStream fs = File.Create(filepath);                                  // 创建或覆盖指定文件或(Text文件)，创建了文件流，没有关闭文件 TODO 文件自身的ASCII码，但是会根据写入文本的格式改变
            fs.Dispose();
            FileStream fstxt = File.Create(Txtfilepath);
            fstxt.Dispose();
            bool exits = File.Exists(filepath);                                     // 判断是否存在指定文件
            File.Move(filepath, targetPath);                                        // 移动文件。 目标文件名不能已存在 (还可以用来重命名文件) TODO 注意：实际上是创建一个新文件，的、复制源文件的内容到新文件，删除源文件。 
            File.Copy(Txtfilepath, fileCopyPath);                                   // 复制文件(添加bool值，也可以用来重命名文件)，同时可以改变文件类型
            File.Delete(Txtfilepath);                                               // 删除指定文件(Text文件)


            /* 读取文件和写入文件 */  // TODO 以下8个方法：对于文件和文本文件是一样的
            string filePath1 = Path.Combine("D:\\", "Desktop", "filePath1");
            string textPath1 = Path.Combine("D:\\", "Desktop", "textPath1.txt");
            string str = "中文English123！@#￥%……&*（）";
            byte[] temp = Encoding.UTF8.GetBytes(str);                              // 将字符以UTF-8的编码方式写成byte数组
            File.WriteAllBytes(filePath1, temp);                                    // 创建/覆盖文件，并写入字节数组，并关闭文件
            byte[] t = File.ReadAllBytes(filePath1);                                // 打开并读取文件内容，并关闭文件

            string textArr = "中文English123！@#￥%……&*（）\r\n sdhnfksajf2021230未开";
            File.WriteAllText(filePath1, textArr, Encoding.UTF8);                   // 创建/覆盖文件，以指定的编码写入字符串，然后关闭文件。TODO 注意： 从本质上来说和 WriteAllBytes 没有任何区别，一个是自己转换成 byte数组 ，一个是自己定义好转换格式系统帮忙转换
            string tmp = File.ReadAllText(filePath1, Encoding.UTF8);                // 打开并读取文件内容，并关闭文件。TODO 注意：这里的编码并没有太大的作用，实际上是根据文件自身的格式来获取的
            Console.WriteLine(tmp);

            string[] strArr = new string[] { "我", "/*/-！@#￥%……&*（）", "sdfsdfsd" };
            File.WriteAllLines(textPath1, strArr, Encoding.UTF8);                   // 跟 WriteAllText 类似 ，不过是按行写入
            string[] kk = File.ReadAllLines(textPath1, Encoding.UTF8);              // 跟 ReadAllText 类似 ，不过是按行读取

            /* 写入文件：追加文本 */
            string[] tmpArray = new string[] { "新的一行", "新的第二行" };
            File.AppendAllLines(filePath1, tmpArray, Encoding.UTF8);                // 创建/打开文件，以指定的编码追加文本（一行一行追加），然后关闭文件。 TODO 第二个参数：可枚举的数据
            string tmpTex = "appendalltext";
            File.AppendAllText(filePath1, tmpTex, Encoding.UTF8);                   // 创建/打开文件，以指定的编码追加文本（增加整个字符串）），然后关闭文件。


            /*  文件流的形式 */
            // FileMode.OpenOrCreate  配合可读可写   FileAccess.ReadWrite
            // FileMode.Append        只能写权限 FileAccess.Write
            string filepath2 = Path.Combine("D:\\", "Desktop", "filepath2");
            FileStream fs1 = File.Open(filepath2, FileMode.Append, FileAccess.Write);// 打开文件，采用文件流的形式,没有关闭文件,文件流的在这的规则：只写，追加
            using (fs1)                                                             //使用结束后自动释放资源
            {
                string str1 = "str1 : 中文English123！@#￥%……&*（）";
                byte[] temp1 = Encoding.UTF8.GetBytes(str1);                        // 将字符串以 UTF-8 的编码为 字节数组
                fs1.Write(temp1, 0, temp1.Length);                                  // 写入byte数组，根据定义的流的性质，是追加文本
            }
            FileStream fs2 = File.OpenWrite(filepath2);                             // 以写的权限(不可读),以文件流的形式打开文件,没有关闭文件，,文件流的在这的规则：只写，覆盖
            using (fs2)
            {
                string strarr = "str2  中文English123！@#￥%……&*（）\r\n str2  sdhnfksajf2021230未开";  // 可以通过这个方式来进行换行
                byte[] WriteByte = Encoding.UTF8.GetBytes(strarr);
                fs2.Write(WriteByte, 0, WriteByte.Length);                          // TODO 这里是文件流的写入指的是：将 WriteByte 数组写入文件的开头，覆盖 WriteByte 的长度，后面若是还有原来的文本，则不会清除，这也符合流的特性
            }                                                                       // 跟 WriteAllText （以当前的字符串完全重写原文件） 是不同的概念

            FileStream fs3 = File.OpenRead(filepath2);                              // 以读的权限(不可写)，以文件流的形式打开文件,没有关闭文件
            using (fs3)
            {
                byte[] ReadByte = new byte[(int)fs3.Length];
                int length = fs3.Read(ReadByte, 0, (int)fs3.Length);                // 将 文件的内容以文件流打开，文件流中是字节数组
                string str2 = Encoding.UTF8.GetString(ReadByte);                    // 此时这些数组可以根据自定义的 编码格式 进行转换成字符串或者其他
                Console.WriteLine(str2);
            }


            /* StreamWriter 和 StreamReader 的形式 */
            string Txtfilepath2 = Path.Combine("D:\\", "Desktop", "Txtfilepath2.txt");
            StreamWriter sw1 = File.CreateText(Txtfilepath2);                       // 创建或打开指定Text文件，创建了“流写器”，没有关闭文件  TODO 创建的文件自身是ASCII码，但是会根据写入文本的格式改变
            using (sw1)                                                             // 通过这种方式创建的了"流写器"，并没有创建原本文件内容的“流”，意味着写入时是全部覆盖的，不像 FileStream 写入时还能追加文本
            {
                string sw1str = "我的博客";                                          // 当写入中文时，会改变文件的编码格式为 UTF8
                sw1.Write(false);                                                  // 写入
                sw1.WriteLine(sw1str);                                              // 行写入
                sw1.WriteLine(sw1str);
                Console.WriteLine(sw1.Encoding);
            }
            StreamWriter sw = File.AppendText(Txtfilepath2);                        // 追加UTF-8格式的文本文件，创建了“流写器”，没有关闭文件
            using (sw)                                                              // 通过这种方式创建的了"流写器"，并没有创建原本文件内容的“流”，即只能写在文件最后面，前面的内容不可更改,是一种“一次性”的写入。跟 FileStream 还是有所区别
            {
                sw.Write('k');
                sw.WriteLine("sdjflasj");
            }
            StreamReader sr = File.OpenText(Txtfilepath2);                          // 打开UTF-8格式的文本文件，创建了原本文件内容的“流读器”，没有关闭文件 TODO:若打开的文件不是UTF8编码，则会是乱码
            using (sr)
            {
                Console.WriteLine(sr.Read());                                       // 读取当前流位置的byte，即文件内容都转换成了byte类型，同时流的位置前进
                Console.WriteLine(sr.ReadLine());                                     // 读取一行流，并且转换成字符串，同时流的位置前进
                Console.WriteLine(sr.ReadToEnd());                                  // 从当前位置读取到末尾，并且转换成字符串，同时流的位置前进
            }


            #endregion

            #region 文件实例类 FileInfo
            /* FileSystemInfo 抽象类 是 FileInfo 的基类 */  // TODO  FileInfo 的方法和File类中基本一致，推荐使用 FileInfo，另外 FileInfo 还能获取 DirectoryInfo 的信息
            /* 文件实例类 FileInfo */
            string filepath3 = Path.Combine("D:\\", "Desktop", "filepath3");
            string Txtfilepath3 = Path.Combine("D:\\", "Desktop", "Txtfilepath3.txt");
            FileInfo fi = new FileInfo(filepath3);
            FileStream fs4 = fi.Create(); // 创建文件
            fs4.Dispose();
            fi = new FileInfo(Txtfilepath3);
            StreamWriter sw4 = fi.CreateText(); //创建text文件，格式：ACSII码
            sw4.Dispose();
           

            #endregion



            Console.ReadLine();
        }
    }
}
