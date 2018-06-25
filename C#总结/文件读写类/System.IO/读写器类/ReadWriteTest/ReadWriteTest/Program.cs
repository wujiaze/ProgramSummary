
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ReadWriteTest
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 读写器 (文本读写器)
             * 所谓的文本类型，不仅仅指的是字符串形式，它包含了记事本上使用任何语言(英语，中文，c# ，javascript,jquery,xml,xaml,sql,c++……)
             * 所以就出现了  TextReader 和 TextWriter  抽象类 定义了读写Text的一系列操作（通过连续的字符），并且是非托管类型，需要手动Dispose释放资源
             * TextReader：表示有序字符系列的读取器
             * TextWriter：
             * 一般采用using 的语句形式，就不需要手动释放资源
             */


            /* 字符串读写器   一般来说，基本用不到，主要是学习 各种方法，为 StreamReader 打下基础
             * StringReader  实现 TextReader ，使其从字符串读取。
             * StringWriter  
            */


            #region  读取 
            //string stringText = "中文qwer1234@#￥%……&*\nzxcv1234中文";
            //Console.WriteLine("--------------Read & Peek-------------------");
            //using (StringReader reader = new StringReader(stringText))
            //{
            //    while (reader.Peek() != -1)                                // 若一下个字符不存在，则返回-1
            //    {
            //        Console.WriteLine("Peek = {0}", (char)reader.Peek());  // Peek: 仅返回下一个字符(ASCII编码)，可以强制转换成字符
            //        Console.WriteLine("Read = {0}", (char)reader.Read());  // Read：读取下一个字符(ASCII编码)，读取位置 前进 一位
            //    }
            //}
            //Console.WriteLine("--------------Read(buffer,index,count)-------------------");
            //using (StringReader reader = new StringReader(stringText))
            //{
            //    char[] charArr = new char[5];
            //    while (reader.Peek() != -1) 
            //    {
            //        int readLength = reader.Read(charArr, 0, charArr.Length); // 返回的是读取的长度，读取的内容存入在buffer中，读取位置 前进 读取的长度
            //                                                                  // 如果 reader 中的长度不足 charArr.Length 的长度，则只读取 reader 中剩余的长度
            //        for (int i = 0; i < readLength; i++)
            //        {
            //            Console.WriteLine("Char{0} = {1}", i, charArr[i]);
            //        }
            //    }
            //}
            //Console.WriteLine("--------------ReadAsync--------------------");
            //using (StringReader reader = new StringReader(stringText))
            //{
            //    char[] charArr = new char[5];
            //    while (reader.Peek() != -1)
            //    {
            //        Task<int> task = reader.ReadAsync(charArr, 0, charArr.Length); // 异步操作，读取位置 前进 读取的长度
            //        task.Wait();                                                   // 这里就直接阻塞等待，毕竟主要是测试
            //        for (int i = 0; i < task.Result; i++)
            //        {
            //            Console.WriteLine("Char{0} = {1}", i, charArr[i]);
            //        }
            //    }
            //}
            //Console.WriteLine("------------ReadBlock----------------------");
            //using (StringReader reader = new StringReader(stringText))
            //{
            //    char[] charArr = new char[5];
            //    int lenth = reader.ReadBlock(charArr, 0, charArr.Length); // ReadBlock 和 Read(buffer, index, count) 的方法作用是一样的，读取位置前进读取的长度
            //    for (int i = 0; i < lenth; i++)
            //    {
            //        Console.WriteLine("Char{0} = {1}", i, charArr[i]);
            //    }
            //}
            //Console.WriteLine("------------ReadBlockAsync----------------------");
            //using (StringReader reader = new StringReader(stringText))
            //{
            //    char[] charArr = new char[5];
            //    Task<int> task = reader.ReadBlockAsync(charArr, 0, charArr.Length); // 异步操作，读取位置 前进 读取的长度
            //    task.Wait();
            //    for (int i = 0; i < task.Result; i++) 
            //    {
            //        Console.WriteLine("Char{0} = {1}", i, charArr[i]);
            //    }
            //}
            //Console.WriteLine("----------------ReadLine------------------");
            //using (StringReader reader = new StringReader(stringText))
            //{
            //    while (reader.Peek() != -1)
            //    {
            //        Console.WriteLine(reader.ReadLine());       // 一行一行读取，并且读取位置 前进 一行的长度
            //    }
            //}
            //Console.WriteLine("----------------ReadLineAsync------------------");
            //using (StringReader reader = new StringReader(stringText))
            //{
            //    while (reader.Peek() != -1)
            //    {
            //        Task<string> task = reader.ReadLineAsync(); // 一行一行读取，并且读取位置 前进 一行的长度
            //        task.Wait();
            //        Console.WriteLine(task.Result);
            //    }
            //}

            //Console.WriteLine("--------------ReadToEnd--------------------");
            //using (StringReader reader = new StringReader(stringText))
            //{
            //    while (reader.Peek() != -1)
            //    {
            //        Console.WriteLine(reader.ReadToEnd());       // 所有字符串整体读取，并且读取位置前进整体的长度
            //    }
            //}
            //Console.WriteLine("--------------ReadToEnd--------------------");
            //using (StringReader reader = new StringReader(stringText))
            //{
            //    while (reader.Peek() != -1)
            //    {
            //        Task<string> task = reader.ReadToEndAsync(); // 所有字符串整体读取，并且读取位置前进整体的长度
            //        task.Wait();
            //        Console.WriteLine(task.Result);
            //    }
            //}


            #endregion

            #region 写入  
            // 构造函数1   StringWriter();
            // 在内部是采用了 StringWriter((IFormatProvider) CultureInfo.CurrentCulture)
            // 构造函数2  public StringWriter(IFormatProvider formatProvider)
            // todo



            #endregion

            /* 流读写器  读写器中的主要成员
             * StreamReader 实现一个 TextReader，使其以一种特定的编码从字节流中读取字符。
             * 关键在于构造函数
             * 其他方法和 StringReader 类似
             *
             */


            #region 读取 
            // StreamReader.DefaultBufferSize =1024

            // 构造函数1 StreamReader(Stream stream)   
            // 在内部是使用了 StreamReader(stream, Encoding.UTF8, true, StreamReader.DefaultBufferSize, false)  

            // 构造函数2 public StreamReader(Stream stream,bool detectEncodingFromByteOrderMarks)
            // 在内部是使用了 StreamReader(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks, StreamReader.DefaultBufferSize, false)  

            // 构造函数3 public StreamReader(Stream stream,Encoding encoding)   
            // 在内部是使用了 StreamReader(stream, encoding, true, StreamReader.DefaultBufferSize, false)  

            // 构造函数4 public StreamReader(Stream stream,Encoding encoding,bool detectEncodingFromByteOrderMarks)
            // 在内部是使用了 StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, StreamReader.DefaultBufferSize, false)

            // 构造函数5 public StreamReader(Stream stream,Encoding encoding,bool detectEncodingFromByteOrderMarks,int bufferSize)
            // 在内部是使用了 StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, false)

            // 构造函数6 public StreamReader(Stream stream,Encoding encoding,bool detectEncodingFromByteOrderMarks,int bufferSize,bool leaveOpen)


            // 构造函数7   public StreamReader(string path)
            // 构造函数8   public StreamReader(string path,bool detectEncodingFromByteOrderMarks)
            // 构造函数9   public StreamReader(string path,Encoding encoding)
            // 构造函数10  public StreamReader(string path,Encoding encoding,bool detectEncodingFromByteOrderMarks)
            // 构造函数11  public StreamReader(string path,Encoding encoding,bool detectEncodingFromByteOrderMarks,int bufferSize)

            /* 总结
             * 上面5个构造函数的 path ，在内部最后都生成文件流
             * (Stream) new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan, Path.GetFileName(path), false, false, checkHost)
             * 从而，也是使用了 1~5 的构造函数 ，最后都是归为 6 这个构造函数来说明
             * 具体的文件流的构造函数，在流那一篇进行解释
             */

            /* 总结 所有的构造函数都是从构造函数6中分化出来的，所以这里就选择构造函数6来解释*/

            // 重点
            // encoding ：选择流的编码格式      默认为 Encoding.UTF8
            // detectEncodingFromByteOrderMarks： true：则表示会根据文件流的文本自行判断编码格式； false：则表示直接根据 encoding 来判断编码格式  默认为 true
            // bufferSize : 读写器的内部缓冲区
            // leaveOpen true:表示在释放StreamReader资源之后，Stream还是打开状态 ，默认是 false ：释放读写器，同时也释放底层流

            // 这里采用文件流，当然也可以是其他流                                    TODO 流那一篇解释完毕，再来看看其他流在这里是否跟文件流是一致的

            // 属性 BaseStream        返回的就是读取的流
            //      CurrentEncoding   返回的
            //      EndOfStream
            // 方法 DiscardBufferedData()
            //      其余方法和StringReader 的基本一致

            string ABCUTF = "D:\\Desktop\\ABCUTF.txt";
            string ABCANSI = "D:\\Desktop\\ABCANSI.txt";

            using (FileStream stream = new FileStream(ABCUTF, FileMode.OpenOrCreate))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.Default, true, 1024, false))
                {
                    Stream temp = reader.BaseStream;                        // BaseStream，获取读写器的底层流
                    Console.WriteLine(temp.Equals(stream));
                    Console.WriteLine(reader.CurrentEncoding);              //  获取当期的编码
                    if (!reader.EndOfStream)                                // 是否读到了流的结尾
                    {
                        Console.WriteLine(reader.ReadToEnd());              // 整个读取流，并显示为字符串
                    }
                }
            }

            Console.WriteLine("----------------- 内部缓冲区 && DiscardBufferedData() -----------------------------");
            using (FileStream stream = new FileStream(ABCANSI, FileMode.OpenOrCreate))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8, true, 1024, false))  //内部缓存区的大小，TODO 暂时不知道用处，使用默认值就可以了
                {
                    Console.WriteLine(reader.BaseStream.Length);
                    for (int i = 0; i < 10; i++)
                    {
                        reader.Read();
                    }
                    reader.DiscardBufferedData();                                           // 当 reader 读取过 流，此时 DiscardBufferedData 清楚内部缓存区，就相当于读到了最后（实际是因为没有了内容）
                    Console.WriteLine(reader.EndOfStream);
                }
            }
            Console.WriteLine("------------------------leaveOpen-------------------------");
            using (FileStream stream = new FileStream(ABCANSI, FileMode.OpenOrCreate))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8, true, 1024, true))  // leaveOpen:true
                {
                    Console.WriteLine("1");
                }
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8, true, 1024, false)) // leaveOpen:false
                {
                    Console.WriteLine("2");
                }
            }



            #endregion

           

            #region 写入



            #endregion


            Console.ReadLine();
        }
       
    }
}
