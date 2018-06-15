
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ReadWriteTest
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 读写器 
             * 文本读写器
             * 所谓的文本类型，不仅仅指的是字符串形式，它包含了记事本上使用任何语言(英语，中文，c# ，javascript,jquery,xml,xaml,sql,c++……)
             * 所以就出现了  TextReader 和 TextWriter  抽象类 定义了读写Text的一系列操作（通过连续的字符），并且是非托管类型，需要手动Dispose释放资源
             * TextReader：表示有序字符系列的读取器
             * TextWriter：
             */

            /* 流读写器 */
            // 读取

            // 写入

            /* 字符串读写器   一般来说，基本用不到
             * StringReader 顾名思义: 针对字符串创建实例
             * StringWriter 
            */

            string stringText = "中文qwer1234@#￥%……&*\nzxcv1234中文";
            Console.WriteLine("----------------Peek------------------");
            // 读取  
            using (StringReader reader = new StringReader(stringText))
            {
                while (reader.Peek()!=-1)
                {
                    Console.WriteLine("Peek = {0}", (char)reader.Peek());  // Peek: 仅返回下一个字符(ASCII编码)，可以强制转换成字符
                    Console.WriteLine("Read = {0}", (char)reader.Read());  // Read：读取下一个字符(ASCII编码)，读取位置前进一位
                }
                //char[] charArr1 = new char[2];
                //reader.ReadAsync(charArr1, 0, charArr1.Length);
            }

            Console.WriteLine("--------------Read--------------------");
            using (StringReader reader = new StringReader(stringText))
            {
                char[] charArr = new char[5];
                while (reader.Peek() != -1)                                   // 最后一位返回-1
                {
                    int readLength = reader.Read(charArr, 0, charArr.Length); // Read(Char[], Int32, Int32),返回的是读取的长度，读取的内容在Char[]中，读取位置前进读取的长度
                    for (int i = 0; i < readLength; i++)
                    {
                        Console.WriteLine("Char{0} = {1}",i, charArr[i]);
                    }
                }
            }
            Console.WriteLine("------------ReadBlock----------------------");
            using (StringReader reader = new StringReader(stringText))
            {
                char[] CharArr = new char[5];
                while (reader.Peek() != -1)
                {
                    int lenth = reader.ReadBlock(CharArr, 0, CharArr.Length); // ReadBlock 和Read的方法作用是一样的，读取位置前进读取的长度
                    for (int i = 0; i < lenth; i++)
                    {
                        Console.WriteLine("Char{0} = {1}", i, CharArr[i]);
                    }
                }
            }
            Console.WriteLine("----------------ReadLine------------------");
            using (StringReader reader = new StringReader(stringText))
            {
                while (reader.Peek() != -1)
                {
                    Console.WriteLine(reader.ReadLine());       // 一行一行读取，并且读取位置前进一行的长度
                }
            }
            Console.WriteLine("--------------ReadToEnd--------------------");
            using (StringReader reader = new StringReader(stringText))
            {
                while (reader.Peek() != -1)
                {
                    Console.WriteLine(reader.ReadToEnd());       // 所有字符串整体读取，并且读取位置前进整体的长度
                }
            }
            Console.WriteLine("--------------ReadAsync--------------------");
            using (StringReader reader = new StringReader(stringText))
            {
                char[] charArr = new char[5];
                while (reader.Peek() != -1)
                {
                   Task<int> task =  reader.ReadAsync(charArr, 0, charArr.Length); // 异步操作
                    
                    for (int i = 0; i < task.Result; i++)
                    {
                        Console.WriteLine("Char{0} = {1}", i, charArr[i]);
                    }
                }
            }
            // 写入 todo
            StringWriter sw =new StringWriter();

            Console.ReadLine();
        }
       
    }
}
