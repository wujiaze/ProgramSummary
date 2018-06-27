using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
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
             * TextWriter：对连续字符系列处理的编写器
             * 一般采用using 的语句形式，就不需要手动释放资源,using语句结束，会自动释放资源
             */


            /* 字符串读写器   一般来说，基本用不到，主要是学习 各种方法，为 StreamReader 打下基础
             * StringReader  实现 TextReader ，使其从字符串读取。
             * StringWriter  处理字符串的编写器
            */


            #region  读取 
            string stringText = "中文qwer1234@#￥%……&*\nzxcv1234中文";
            Console.WriteLine("--------------Read & Peek-------------------");
            using (StringReader reader = new StringReader(stringText))
            {
                while (reader.Peek() != -1)                                // 若一下个字符不存在，则返回-1
                {
                    Console.WriteLine("Peek = {0}", (char)reader.Peek());  // Peek: 仅返回下一个字符(ASCII编码)，可以强制转换成字符
                    Console.WriteLine("Read = {0}", (char)reader.Read());  // Read：读取下一个字符(ASCII编码)，读取位置 前进 一位
                }
            }
            Console.WriteLine("--------------Read(buffer,index,count)-------------------");
            using (StringReader reader = new StringReader(stringText))
            {
                char[] charArr = new char[5];
                while (reader.Peek() != -1)
                {
                    int readLength = reader.Read(charArr, 0, charArr.Length); // 返回的是读取的长度，读取的内容存入在buffer中，读取位置 前进 读取的长度
                                                                              // 如果 reader 中的长度不足 charArr.Length 的长度，则只读取 reader 中剩余的长度
                    for (int i = 0; i < readLength; i++)
                    {
                        Console.WriteLine("Char{0} = {1}", i, charArr[i]);
                    }
                }
            }
            Console.WriteLine("--------------ReadAsync--------------------");
            using (StringReader reader = new StringReader(stringText))
            {
                char[] charArr = new char[5];
                while (reader.Peek() != -1)
                {
                    Task<int> task = reader.ReadAsync(charArr, 0, charArr.Length); // 异步操作，读取位置 前进 读取的长度
                    task.Wait();                                                   // 这里就直接阻塞等待，毕竟主要是测试
                    for (int i = 0; i < task.Result; i++)
                    {
                        Console.WriteLine("Char{0} = {1}", i, charArr[i]);
                    }
                }
            }
            Console.WriteLine("------------ReadBlock----------------------");
            using (StringReader reader = new StringReader(stringText))
            {
                char[] charArr = new char[5];
                int lenth = reader.ReadBlock(charArr, 0, charArr.Length); // ReadBlock 和 Read(buffer, index, count) 的方法作用是一样的，读取位置前进读取的长度
                for (int i = 0; i < lenth; i++)
                {
                    Console.WriteLine("Char{0} = {1}", i, charArr[i]);
                }
            }
            Console.WriteLine("------------ReadBlockAsync----------------------");
            using (StringReader reader = new StringReader(stringText))
            {
                char[] charArr = new char[5];
                Task<int> task = reader.ReadBlockAsync(charArr, 0, charArr.Length); // 异步操作，读取位置 前进 读取的长度
                task.Wait();
                for (int i = 0; i < task.Result; i++)
                {
                    Console.WriteLine("Char{0} = {1}", i, charArr[i]);
                }
            }
            Console.WriteLine("----------------ReadLine------------------");
            using (StringReader reader = new StringReader(stringText))
            {
                while (reader.Peek() != -1)
                {
                    Console.WriteLine(reader.ReadLine());       // 一行一行读取，并且读取位置 前进 一行的长度
                }
            }
            Console.WriteLine("----------------ReadLineAsync------------------");
            using (StringReader reader = new StringReader(stringText))
            {
                while (reader.Peek() != -1)
                {
                    Task<string> task = reader.ReadLineAsync(); // 一行一行读取，并且读取位置 前进 一行的长度
                    task.Wait();
                    Console.WriteLine(task.Result);
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
            Console.WriteLine("--------------ReadToEnd--------------------");
            using (StringReader reader = new StringReader(stringText))
            {
                while (reader.Peek() != -1)
                {
                    Task<string> task = reader.ReadToEndAsync(); // 所有字符串整体读取，并且读取位置前进整体的长度
                    task.Wait();
                    Console.WriteLine(task.Result);
                }
            }


            #endregion


            #region 写入  
            Console.WriteLine("----------------- StringWriter -----------------------------");
            // 构造函数1   StringWriter();
            // 在内部是采用了 StringWriter(new StringBuilder(), (IFormatProvider) CultureInfo.CurrentCulture)

            // 构造函数2  StringWriter(IFormatProvider formatProvider)                                   // 关于 IFormatProvider 接口，在另一篇中会详细解释，这里就采用默认的
            // 在内部是采用了 StringWriter(new StringBuilder(), formatProvider)

            // 构造函数3 StringWriter(StringBuilder sb)
            // 在内部采用了 StringWriter(sb, (IFormatProvider) CultureInfo.CurrentCulture)

            // 构造函数4 StringWriter(StringBuilder sb, IFormatProvider formatProvider)  完整的构造函数


            // 属性 ：Encoding         // StringWriter 的默认编码格式是 Unicode
            //        FormatProvider    //一般本地化程序，只需要使用默认的就可以了
            //        NewLine
            /* 方法：Flush              // 首先说明，StringWriter 无法将内容写入文件，只能将内容写入  StringBuilder对象 
             *      FlushAsync          // 其次，Flush 和  FlushAsync 内容是空的，内部没有重写 ，即这个方法是不存在的
             *      GetStringBuild
             *      Write               是将内容写入 StringBuilder 对象的字符串中
             *      WriteAsync
             *      WriteLine
             *      WriteLineAsync
             */


            // 方法  Write
            // 第 1 类方法
            //  Write(char value)        Write(string)
            //  内部是使用了 _sb.Append(value);               // 总结，所有的的方法最后都使用了_sb.Append(value);  

            // 第 2 类方法
            //  Write(bool value)       Write(object value)
            //  内部使用了 Write(string)

            // 第 3 类方法
            // Write(char[] buffer)   Write(char[] buffer, int index, int count)
            // 内部是使用了 Write(char value)

            // 第 4 类方法
            // Write(int value)     Write(uint value)       Write(long value)       Write(ulong value)
            // Write(float value)   Write(double value)     Write(Decimal value) 
            // 内部是使用了Write(string) ，具体就是===>> Write(value.ToString(this.FormatProvider))

            // 第 5 类方法
            //  Write(string format, object arg0)                               Write(string format, object arg0, object arg1)
            //  Write(string format, object arg0, object arg1, object arg2)     Write(string format, params object[] arg)
            // 内部是使用了Write(string) ，具体就是===>> Write(string.Format(this.FormatProvider, format, argx))               // 即根据提供的格式化类对象 FormatProvider，对当前写入的内容 argx，采用需要的格式 format
            using (StringWriter stringWriter = new StringWriter(new StringBuilder(), (IFormatProvider)CultureInfo.CurrentCulture))
            {
                Console.WriteLine(stringWriter.FormatProvider);    // 格式化对象器
                Console.WriteLine(stringWriter.Encoding);           // 写入的编码格式 StringWriter 的默认编码格式是 Unicode
                stringWriter.Write('A');                         //第 1 类方法       
                stringWriter.Write("你好");                       //第 1 类方法
                stringWriter.Write(false);                      //第 2 类方法
                stringWriter.Write(new char[]{'B','C'},1,1);        //第 3 类方法
                stringWriter.Write(12.56);                          // 第 4 类方法
                stringWriter.Write("{0:N}",9);                      //第 5 类方法
                Console.WriteLine(stringWriter.GetStringBuilder().ToString()); // 获取 StringBuilder 对象的方法
                Console.WriteLine(stringWriter.Encoding);
                stringWriter.Flush();
            }

            // 方法 WriteAsync
            // 就是一般 Write 方法，加一个一般的异步操作
            using (StringWriter stringWriter = new StringWriter()) //与上面的构造函数实际意义是一样的
            {
                Task task = stringWriter.WriteAsync('a');
                task.Wait();                                       
            }
            // 方法 WriteLine
            //  WriteLine()       
            //  内部是使用了 Write(char[] buffer)   具体是===>> Write(this.CoreNewLine)    CoreNewLine 由属性 NewLine 确定  默认是\r\n
            //  其余的 方法 都是使用对应的 Write 方法 ，加WriteLine()
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.WriteLine(stringWriter.NewLine);                     // 通过打断点，可以知道 newline 的信息
                stringWriter.WriteLine("Hello你好！");
                Console.WriteLine(stringWriter.GetStringBuilder().ToString());
            }
            // 方法 WriteLineAsync
            // 就是一般 WriteLine 方法，加一个一般的异步操作
            using (StringWriter stringWriter = new StringWriter())
            {
                Task task = stringWriter.WriteLineAsync('a');
                task.Wait();
            }

            #endregion

            /* 流读写器  读写器中的主要成员
             * StreamReader 实现一个 TextReader，使其以一种特定的编码从字节流中读取字符。
             * 关键在于构造函数
             * 其他方法和 StringReader 类似
             *
             * StreamWriter 通过特定的编码和流的方式对数据进行处理的编写器
             */


            #region 读取 
            Console.WriteLine("----------------- StreamReader -----------------------------");
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
            Console.WriteLine("-------------- StreamWriter -------------------");
            // 构造函数1    StreamWriter(Stream stream, Encoding encoding, int bufferSize, bool leaveOpen)

            /* 构造函数2    StreamWriter(Stream stream)
             * 内部使用了   StreamWriter(stream, StreamWriter.UTF8NoBOM, 1024, false)            //  StreamWriter.UTF8NoBOM : 表示UTF8

             * 构造函数3    StreamWriter(Stream stream, Encoding encoding)
             * 内部使用了    StreamWriter(stream, encoding, 1024, false)

             * 构造函数4    StreamWriter(Stream stream, Encoding encoding, int bufferSize)
             * 内部使用了    StreamWriter(stream, encoding, bufferSize, false)
             */

            // 构造函数5    StreamWriter(string path, bool append, Encoding encoding, int bufferSize)
            // 内部通过 path 和  append  创建了文件流 FileStream, 同时也创建了文件（若文件已存在，则覆盖原文件）
            // 其中 mode 参数：FileMode mode = append ? FileMode.Append : FileMode.Create;   checkHost ：内部赋值 true
            // (Stream)new FileStream(path, mode, FileAccess.Write, FileShare.Read, 4096, FileOptions.SequentialScan, Path.GetFileName(path), false, false, checkHost); 
            // 最后使用了 构造函数1  StreamWriter(stream, encoding, bufferSize,false)

            /* 构造函数6    StreamWriter(string path)
             * 内部使用了    StreamWriter(path, false, StreamWriter.UTF8NoBOM, 1024)

             * 构造函数7    StreamWriter(string path, bool append)
             * 内部使用了    StreamWriter(path, append, StreamWriter.UTF8NoBOM, 1024)

             * 构造函数8    StreamWriter(string path, bool append, Encoding encoding)
             * 内部使用了    StreamWriter(path, append, encoding, 1024)
             */

            /* 属性
             *  AutoFlush
             *  BaseStream
             *  Encoding
             *  NewLine
             *  FormatProvider
             */
            // 采用默认的构造函数测试
            string filePath = "D:\\Desktop\\File1.txt";
            using (StreamWriter streamWriter = new StreamWriter(filePath, false, Encoding.UTF8, 1024)) 
            {
                Console.WriteLine(streamWriter.Encoding);                   // StreamWriter 的默认编码格式是 UTF8
                Console.WriteLine(streamWriter.NewLine);                    // 当前默认是 \r\n
                Console.WriteLine(streamWriter.AutoFlush);                  // 默认是 false
                Console.WriteLine(streamWriter.BaseStream);                 // 可以知道，内部确实创建了一个文件流，以及一个文件
                Console.WriteLine(streamWriter.FormatProvider);             // 默认为 (IFormatProvider) CultureInfo.CurrentCulture ，这里就是 zh-CN ，跟 StringWrite 不同，无法自己修改
            }
            /* 方法：
             * Flush                       
             * FlushAsync
             * Write               
             * WriteAsync
             * WriteLine
             * WriteLineAsync
             */


            /* 方法 Write
             * 大致上和 StringWrite 是一致的
             * 全部都是追加在末尾，符合流的特性
             */
            // 采用构造函数6
            filePath = "D:\\Desktop\\File2.txt";
            using (StreamWriter streamWriter = new StreamWriter(filePath)) 
            {
                // 注意：写入的内容在内部被缓存在 字符数组 charBuffer ，然后 转存到 字节数组 byteBuffer，从中可以看出，流 是对字节数组进行操作
                streamWriter.Write('B');                             //第 1 类方法 
                streamWriter.Write("你好");                            //第 1 类方法
                streamWriter.Write(false);                              //第 2 类方法
                streamWriter.Write(new char[] { 'B', 'C' }, 1, 1);        //第 3 类方法
                streamWriter.Write(12.56);                              // 第 4 类方法
                streamWriter.Write("{0:N}", 9);                         //第 5 类方法
            }

            // 采用构造函数5
            filePath = "D:\\Desktop\\File3.txt";
            using (StreamWriter streamWriter = new StreamWriter(filePath,false,Encoding.UTF8,128)) 
            {
                for (int i = 0; i < 130; i++)                           // buffersize 缓存大小，在内部赋值给了 字符数组 charBuffer
                {                                                       // 但是，在这里似乎看不到效果，不过感觉合理就可以了 TODO 以后有必要再看看
                    streamWriter.Write('A');        
                }
                Thread.Sleep(3000);
            }
            // 方法 WriteAsync
            filePath = "D:\\Desktop\\File4.txt";
            using (StreamWriter streamWriter = new StreamWriter(filePath)) 
            {
                Task task = streamWriter.WriteAsync("abc");                   // 就是一般的 Write 方法 ，加 一般的异步操作
                task.Wait();
            }
            // 方法 WriteLine
            filePath = "D:\\Desktop\\File5.txt";
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.WriteLine('A');                                // 就是在一般的 Write 方法后面，添加  WriteLine() 方法
                streamWriter.WriteLine("你好！");
                streamWriter.WriteLine();
            }
            // 方法 WriteLineAsync
            filePath = "D:\\Desktop\\File6.txt";
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                Task task = streamWriter.WriteLineAsync("ABC");            // 就是一般的 WriteLine 方法 ，加 一般的异步操作
                task.Wait();
            }

            // 采用构造函数1
            filePath = "D:\\Desktop\\File7.txt";
            using (FileStream stream = new FileStream(filePath,FileMode.OpenOrCreate,FileAccess.Write,FileShare.Read,4096,false))
            {
                using (StreamWriter streamWriter = new StreamWriter(stream,Encoding.UTF8,128,true))         // leaveopen :true 表示当编写器关闭时，不关闭底层流
                {
                    streamWriter.AutoFlush = true;
                    streamWriter.Write("ABC");                                                              //  AutoFlush: true ，则每次调用 Write 系列方法，都会立即写入文本
                    Thread.Sleep(3000);
                }
                using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8, 128, true))
                {
                    streamWriter.Write("123");
                    streamWriter.Flush();                                                                     //  Flush 立即写入文本
                    Thread.Sleep(3000);
                    streamWriter.Write("456");                                                                //  没用 Flush ，则会在关闭 StreamWriter 对象时，写入文本
                    Thread.Sleep(3000);
                }
                using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8, 128, true))
                {
                    streamWriter.Write("ABC");
                    Task task = streamWriter.FlushAsync();                                                      // 就是 Flush 方法，加上一个异步方法
                    task.Wait();
                    Thread.Sleep(3000);
                }
            }

            Console.WriteLine("程序完结");
            #endregion


            Console.ReadLine();
        }
       
    }
}
