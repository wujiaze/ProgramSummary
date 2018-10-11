using System;
using System.IO;
using System.Text;


namespace BufferedStreamTest
{
    class Program
    {
        // 作用: BufferedStream 是一种对流对象的缓冲，可以减小I/O开销，提高性能
        static void Main(string[] args)
        {
            // BufferedStream 从本质上来说就是一个装饰流的流类
            /*
             * 构造函数
             *  BufferedStream(Stream stream, int bufferSize)
             *  BufferedStream(       stream,     4096      )
             */

            /* 属性 : 由于是装饰类，所以获取的属性，实际是被装饰的流的属性
            *  CanRead
            *  CanSeek
            *  CanTimeout
            *  CanWrite
            *  Length
            *  Position
            *  ReadTimeout
            *  WriteTimeout
            */
            Console.WriteLine("-------------- 属性 ---------------");
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BufferedStream bufferedStream = new BufferedStream(memoryStream))
                {
                    Console.WriteLine(bufferedStream.CanRead);
                    Console.WriteLine(bufferedStream.CanSeek);
                    Console.WriteLine(bufferedStream.CanTimeout);
                    Console.WriteLine(bufferedStream.CanWrite);
                    Console.WriteLine(bufferedStream.Length);
                    Console.WriteLine(bufferedStream.Position);
                    byte bt = 100;
                    for (int i = 0; i < 5000; i++)
                    {
                        bufferedStream.WriteByte(bt);
                    }
                    Console.WriteLine(bufferedStream.Length);
                    Console.WriteLine(bufferedStream.Position);
                    Console.WriteLine(memoryStream.GetBuffer().Length);     // 测试说明 流的长度，也是跟 被装饰的流 的类型有关
                }
            }
            /*
             * 方法
             * 自身的方法
             *  BeginRead / EndRead
             *  BeginWrite / EndWrite
             *
             *  继承Stream ，采用被装饰的流的方法
             *  CopyTo / CopyToAsync
             *  Close/Dispose
             *  Flush/FlushAsync
             *
             *  重写方法，还是被装饰的流的方法
             *  Read/ReadAsync/ReadByte
             *  Write/WriteAsync/WriteByte
             *  Seek
             *  SetLength
             */
            // BeginWrite / EndWrite
            Console.WriteLine("-------------- BeginWrite/ EndWrite ---------------");
            FileStream fileStream = new FileStream("D:\\Desktop\\1.txt",FileMode.Create);
            BufferedStream bufferedStream1 = new BufferedStream(fileStream);
            string str = "abc123你好！+";
            byte[] tempBytes = Encoding.UTF8.GetBytes(str);
            bufferedStream1.BeginWrite(tempBytes, 0, tempBytes.Length, BeginWriteCallBack, bufferedStream1);


            // BeginRead / EndRead   
            Console.WriteLine("-------------- BeginRead / EndRead ---------------");
            byte[] bytes = new byte[] { 100, 101, 102, 103, 104 };
            MemoryStream memoryStream1 = new MemoryStream(bytes);
            Console.WriteLine(memoryStream1.Position);
            BufferedStream bufferedStream2 = new BufferedStream(memoryStream1);
            Console.WriteLine(bufferedStream2.Length);
            Console.WriteLine(bufferedStream2.Position);
            bufferedStream2.BeginRead(tempBytes1, 0, (int)bufferedStream2.Length, BeginReadCallBack, bufferedStream2);

           

            Console.ReadLine();
        }

        private static void BeginWriteCallBack(IAsyncResult ar)
        {
            BufferedStream stream = ar.AsyncState as BufferedStream;
            if (stream == null) return;
            stream.EndWrite(ar);
            stream.Close();                                             // 实际上执行的是 被装饰的流的Close 方法，所以只需要关闭 BufferedStream 的对象，就关闭了 底层流的对象
            Console.WriteLine("BeginWriteCallBack");
        }


        static byte[] tempBytes1 = new byte[5];
        private static void BeginReadCallBack(IAsyncResult ar)
        {
            BufferedStream stream = ar.AsyncState as BufferedStream;
            if (stream == null) return;
            stream.EndRead(ar);                            //这一步是关闭流，所以需要通过流完成的事，需要在这一步之前
            stream.Close();                             // 实际上执行的是 被装饰的流的Close 方法，所以只需要关闭 BufferedStream 的对象，就关闭了 底层流的对象
            Console.WriteLine(Encoding.UTF8.GetString(tempBytes1));

        }

       





    }
}
