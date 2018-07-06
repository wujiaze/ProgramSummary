
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//MemoryStream 是内存流,为系统内存提供读写操作，由于 MemoryStream 是通过 byte 无符号字节数组组成的，内部采用 Buffer.InternalBloackCopy，内存位移的方法，性能出色
//所以它担当起了一些其他流进行数据交换时的中间工作
// 比如：FileStream 主要对文件的一系列操作，属于比较高层的操作，但是 MemoryStream 却很不一样，它更趋向于底层内存的操作，这样能够达到更快的速度和性能，也是他们的根本区别，很多时候，操作文件都需要 MemoryStream 来实际进行读写，最后放入到相应的 FileStream 中,不仅如此，在诸如 XmlWriter 的操作中也需要使用到 MemoryStream 提高读写速度。

namespace MemoryStreamTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // 在使用构造函数初始化时，内部的 byte数组 根据不同的构造函数被赋值
            // 而流真正的内容，是 byte数组的一部分，或者说是从 byte数组中取出来的一部分,流的内容通过Read方法获取
            /* 构造函数
             *  第一类:内部根据参数初始化了一个 byte 数组 ，这个数组是可扩展的！
             *  1.1 MemoryStream(int capacity)        最大容量是 2048-20 M    true           true 
             *  1.2 MemoryStream(); 内部是MemoryStream(0),最大容量是1024 M    true           true
             *  
             *  第二类 ：内部是将 buffer 赋给了 byte 数组 ，根据起止位置，这个数组是不可扩展的！
             *  2.1 MemoryStream(byte[] buffer, int index, int count, bool writable, bool publiclyVisible)
             *  2.2 MemoryStream(       buffer,     index,     count,      writable,      false)
             *  2.3 MemoryStream(       buffer,     index,     count,      true,          false)
             *  第三类：内部是将 buffer 整个赋给了 byte 数组，这个数组是不可扩展的！
             *  3.1 MemoryStream(byte[] buffer, bool writable)                             false
             *  3.2 MemoryStream(       buffer,      true)
             *
             *  一类的流能读写整个内部byte数组
             *  二类的流能读写的位置在buffer数组的 index 和 count之间,index在流中同时也代表 Position = 0
             *  三类的流只能读写 buffer 数组
             */


            /* 属性
             *  CanRead             MemoryStream 为true
             *  CanSeek             MemoryStream 为true
             *  CanWrite            根据传入的参数     
             *  Capacity(独有)      内部 byte 数组的缓存大小， 手动最大是 int.MaxValue ，自动最大在 int.MaxValue/2 ~ int.MaxValue 之间  
             *  Length              流实际的长度
             *  Position            当前流的位置
             *  CanTimeout          MemoryStream 为false
             *  ReadTimeout         MemoryStream 不支持       
             *  WriteTimeout        MemoryStream 不支持 
             */

            // 测试构造函数 1.1
            Console.WriteLine("----------- 测试构造函数 1.1 ------------");
            using (MemoryStream stream = new MemoryStream(1024))
            {
                Console.WriteLine("stream.CanWrite " + stream.CanWrite);
                Console.WriteLine("stream.Capacity " + stream.Capacity);
                Console.WriteLine("stream.Length " + stream.Length);
                Console.WriteLine("stream.Position " + stream.Position);
            }

            // 测试构造函数 2.1
            Console.WriteLine("----------- 测试构造函数 2.1 ------------");
            byte[] bytes = new byte[10];
            using (MemoryStream stream = new MemoryStream(bytes,5,4,false,false))
            {
                Console.WriteLine("stream.CanWrite " + stream.CanWrite);
                Console.WriteLine("stream.Position " + stream.Position);
                Console.WriteLine("stream.Capacity " + stream.Capacity);
                Console.WriteLine("stream.Length " + stream.Length);
            }


            /* 方法
             *
             *  Read            从流中读取 流的byte数组 内容
             *  ReadAsync
             *  ReadByte
             *
             *  Write           写入 流的byte数组
             *  WriteAsync
             *  WriteByte
             *  WriteTo(独有)
             *
             *  BeginRead
             *  EndRead
             *  BeginWrite
             *  EndWrite
             *
             *  Seek
             *  SetLength
             *
             *  ToArray
             *  CopyTo
             *  CopyToAsync
             *
             *  GetBuffer(独有)      获取内部完整的byte数组
             *  TryGetBuffer(独有)   造函数3.1/第一类构造函数 publiclyVisible:true, 则获取内部的关于流的那一部分byte数组
             *  
             *  Flush               方法内部是空的
             *  FlushAsync          方法内部执行了 空的Flush
             *  
             *  Close               使用using就好
             *  Dispose             使用using就好
             */

            // GetBuffer 
            Console.WriteLine("-------------- GetBuffer --------------");
            using (MemoryStream stream = new MemoryStream(10))  //第一类构造函数，内部数组可扩展
            {
                byte[] tempbytes1 = stream.GetBuffer();         //说明 获取的就是空数组
                Console.WriteLine("内部可扩展数组的长度： "+tempbytes1.Length);
                foreach (byte tempbyte in tempbytes1)
                {
                    Console.WriteLine(tempbyte);
                }
              
                Console.WriteLine("****");
                byte[] bytes2 = new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9,10};
                stream.Write(bytes2,0,10);
                byte[] tempbytes2 = stream.GetBuffer();         // 写入了内容，撑满了整个缓存数组
                Console.WriteLine("内部可扩展数组的长度： " + tempbytes2.Length);
                foreach (byte tempbyte in tempbytes2)
                {
                    Console.WriteLine(tempbyte);
                }
                
                Console.WriteLine("***");
                byte[] bytes3= new byte[] { 100,101};
                for (int i = 0; i < 2; i++)
                {
                    stream.Write(bytes3, 0, 2);
                }
                byte[] tempbytes3 = stream.GetBuffer();     // 写入的内容超越了数组长度，会自动扩大数组长度，空白的地方为0
                Console.WriteLine("内部可扩展数组的长度： " + tempbytes3.Length);
                foreach (byte tempbyte in tempbytes3)
                {
                    if (tempbyte == 0)
                        continue; 
                    Console.WriteLine(tempbyte);
                }
            }

            // TryGetBuffer   获取的是定义构造函数时,固定的那一段 byte数组 的内容是流能够获取的
            Console.WriteLine("-------------- TryGetBuffer --------------");
            byte[] testByte1 = new byte[] { 1, 2, 3, 4, 5 };
            using (MemoryStream stream = new MemoryStream(testByte1, 1, 2, true, true))
            {
                Console.WriteLine("stream.Position: "+ stream.Position);
                Console.WriteLine("stream.Length: " + stream.Length);
                Console.WriteLine("stream.Capacity: " + stream.Capacity);
                ArraySegment<byte> arrayByte;
                stream.TryGetBuffer(out arrayByte);
                byte[] tempbytes2 = arrayByte.ToArray();
                Console.WriteLine("内部不可扩展数组的长度： " + tempbytes2.Length);
                foreach (byte tempbyte in tempbytes2)
                {
                    Console.WriteLine(tempbyte);
                }
                // 写入内容之后
                for (int i = 0; i < stream.Length; i++)
                {
                    stream.WriteByte(testByte1[0]);
                }
                stream.TryGetBuffer(out arrayByte);
                tempbytes2 = arrayByte.ToArray();
                Console.WriteLine("内部不可扩展数组的长度： " + tempbytes2.Length);
                foreach (byte tempbyte in tempbytes2)
                {
                    Console.WriteLine(tempbyte);
                }
            }

            // Write    
            Console.WriteLine("-------------- Write --------------");
            using (MemoryStream stream = new MemoryStream())            //写入流
            {
                byte[] writeBytes = new byte[]{1,2,3,4,5};
                stream.Write(writeBytes,1,2);
                byte[] getBytes = stream.GetBuffer();
                Console.WriteLine("getBytes.Length: "+ getBytes.Length);
                foreach (byte b in getBytes)
                {
                    if (b == 0) continue;
                    Console.WriteLine(b);
                }
            }
            // WriteAsync
            Console.WriteLine("-------------- WriteAsync --------------");
            using (MemoryStream stream = new MemoryStream())            //写入流
            {
                byte[] writeBytes = new byte[] { 1, 2, 3, 4, 5 };
                Task task = stream.WriteAsync(writeBytes, 1, 2,CancellationToken.None); // 默认就是 CancellationToken.None
                // 简单的用法
                task.Wait();
                byte[] getBytes = stream.GetBuffer();
                Console.WriteLine("getBytes.Length: " + getBytes.Length);
                foreach (byte b in getBytes)
                {
                    if (b == 0) continue;
                    Console.WriteLine(b);
                }
            }
            // WriteByte
            Console.WriteLine("-------------- WriteByte --------------");
            using (MemoryStream stream = new MemoryStream())            //写入流
            {
                byte[] writeBytes = new byte[] { 1, 2, 3, 4, 5 };
                stream.WriteByte(writeBytes[4]);
                byte[] getBytes = stream.GetBuffer();
                Console.WriteLine("getBytes.Length: " + getBytes.Length);
                foreach (byte b in getBytes)
                {
                    if (b == 0) continue;
                    Console.WriteLine(b);
                }
            }
            // WriteTo
            Console.WriteLine("-------------- WriteTo --------------");
            using (MemoryStream stream = new MemoryStream())            // 这种方法，比直接使用 FileStream 会快一些，性能好一些
            {                    
                // MemoryStream  FileStream 谁先谁后，根据意思，在这里无关的
                using (FileStream fileStream = new FileStream("D:\\Desktop\\1.txt",FileMode.OpenOrCreate))
                {
                    string str = "Hello 你好！";
                    byte[] writeBytes = Encoding.UTF8.GetBytes(str);
                    stream.Write(writeBytes,0, writeBytes.Length);
                    stream.WriteTo(fileStream);                      // 将此内存流的整个内容写入到另一个流中。与Position的位置无关  
                    Console.WriteLine(stream.Position);
                    stream.Position = 0;
                    stream.WriteTo(fileStream);                     // 测试说明，也不会影响 源流的Position，只会影响目标流的Position ， 对比  CopyTo 
                    Console.WriteLine(stream.Position);
                    Console.WriteLine(fileStream.Length);
                }
            }
            // CopyTo    //CopyToAsync   跟WriteAsync 类似
            Console.WriteLine("-------------- CopyTo --------------");
            using (MemoryStream stream = new MemoryStream())
            {
                string str = "Hello 你好！";
                byte[] writeBytes = Encoding.UTF8.GetBytes(str);
                stream.Write(writeBytes, 0, writeBytes.Length);
                using (FileStream fileStream = new FileStream("D:\\Desktop\\2.txt", FileMode.OpenOrCreate))
                {
                    stream.Position = 0;                            //  从当前流的Position开始读取一段写入目标流 ，所以跟 Position 位置有关
                    stream.CopyTo(fileStream);
                    Console.WriteLine(fileStream.Position);         //  影响 源流 和 目标流
                    Console.WriteLine(stream.Position);
                }
            }
           
            //Read    Seek   //ReadAsync   跟WriteByte差不多的用法
            Console.WriteLine(" -------------- Read & Seek & CopyTo --------------");
            using (FileStream fileStream = new FileStream("D:\\Desktop\\1.txt", FileMode.OpenOrCreate))
            {
                using (MemoryStream stream = new MemoryStream())            // 这种方法，比直接使用 FileStream 会快一些，性能好一些
                {
                    fileStream.ReadByte();
                    int oldLength = (int)stream.Length;
                    fileStream.CopyTo(stream,(int)fileStream.Length);       // 是从当前流的position位置开始，赋值给另一个流的Position的位置,读取的长度超过源流的长度，则只复制源流的长度
                    Console.WriteLine(stream.Position); 
                    int newLength = (int)stream.Length;
                    stream.Seek(-(newLength-oldLength), SeekOrigin.Current);// 得到内容的流的Position也是会前进复制过来的长度，当需要读取这段内容时，可以重新设定Position位置
                    byte[] readBytes = new byte[stream.Length];
                    stream.Read(readBytes, 0, readBytes.Length);
                    Console.WriteLine(Encoding.UTF8.GetString(readBytes));
                }
            }
            

            //ReadByte
            Console.WriteLine("-------------- ReadByte --------------");
            using (FileStream fileStream = new FileStream("D:\\Desktop\\1.txt", FileMode.OpenOrCreate))
            {
                using (MemoryStream stream = new MemoryStream())            // 这种方法，比直接使用 FileStream 会快一些，性能好一些
                {
                    fileStream.ReadByte();
                    int oldLength = (int)stream.Length;
                    fileStream.CopyTo(stream);                              // 是从当前流的position位置开始，赋值给另一个流的Position的位置
                    int newLength = (int)stream.Length;
                    stream.Seek(-(newLength - oldLength), SeekOrigin.Current);// 得到内容的流的Position也是会前进复制过来的长度，当需要读取这段内容时，可以重新设定Position位置
                    char c = (char)stream.ReadByte();
                    Console.WriteLine(c);
                }
            }

            //SetLength
            Console.WriteLine("-------------- SetLength --------------");
            using (MemoryStream stream = new MemoryStream())           
            {
                Console.WriteLine(stream.Length);
                stream.SetLength(10);                   // 经过测试表明：当设置的值大于流原来的长度，则加长 流的长度，但是不改变 流的Position
                Console.WriteLine(stream.Length);
                Console.WriteLine(stream.Position);
                byte[] tempbytes = stream.GetBuffer();
                foreach (byte tempbyte in tempbytes)
                {
                    if(tempbyte==0)continue;
                    Console.WriteLine(tempbyte);
                }

                Console.WriteLine("写入");
                stream.WriteByte(testByte1[0]);
                stream.WriteByte(testByte1[0]);
                Console.WriteLine(stream.Length);
                Console.WriteLine(stream.Position);
                Console.WriteLine("缩小");
                stream.SetLength(1);                        // 测试表明:当设置的值 小于 流原来的长度，则删除原来的流尾部多出来的数据 ,并且Position就是新的流的末尾
                Console.WriteLine(stream.Length);
                Console.WriteLine(stream.Position);
                Console.WriteLine("读取");
                Console.WriteLine(stream.ReadByte());      // 当超过数组边界时，就返回 -1
                Console.WriteLine(stream.Length);
                Console.WriteLine(stream.Position);
                tempbytes = stream.GetBuffer();
                Console.WriteLine("遍历");
                foreach (byte tempbyte in tempbytes)
                {
                    if (tempbyte == 0) continue;
                    Console.WriteLine(tempbyte);
                }
            }


            //ToArray
            Console.WriteLine("-------------- ToArray --------------");
            using (MemoryStream stream = new MemoryStream())
            {
                stream.WriteByte(testByte1[3]);
                stream.WriteByte(testByte1[4]);
                Console.WriteLine(stream.GetBuffer().Length);
                Console.WriteLine(stream.Length);
                byte[] temp = stream.ToArray();                 //将流的内容全部拷贝到数组，跟Position的位置无关
                foreach (byte b in temp)
                {
                    Console.WriteLine(b);
                }
            }

            // 注意：以下的I/O方法已经快过时了，推荐  Async 的方法    并且不能用using语句
            // BeginWrite  EndWrite
            Console.WriteLine("-------------- BeginWrite &  EndWrite --------------");
            MemoryStream stream1 = new MemoryStream();
            string s = "abc123你好！+";
            byte[] tempBytes = Encoding.UTF8.GetBytes(s);
            stream1.BeginWrite(tempBytes, 0, tempBytes.Length, BeginWriteCallBack, stream1);

            // BeginRead   EndRead
            Console.WriteLine("-------------- BeginRead &  EndRead --------------");
            MemoryStream memoryStream = new MemoryStream();
            FileStream stream2 = new FileStream("D:\\Desktop\\1.txt", FileMode.Open);
            stream2.CopyTo(memoryStream);
            memoryStream.Position = 0;
            byte[] buffer = new byte[50];
            MyClass m1 = new MyClass(memoryStream, buffer, stream2);
            memoryStream.BeginRead(buffer, 0, (int)memoryStream.Length, BeginReadCallBack, m1);

            Console.ReadLine();
        }
        /// <summary>
        /// 写入完成的回调
        /// </summary>
        /// <param name="ar"></param>
        private static void BeginWriteCallBack(IAsyncResult ar)
        {
            MemoryStream stream = ar.AsyncState as MemoryStream;
            if(stream==null)return;
            byte[] bytes = stream.ToArray();
            stream.EndWrite(ar);                            //这一步是关闭流，所以需要通过流完成的事，需要在这一步之前
            stream.Close();
            string str = Encoding.UTF8.GetString(bytes);
            Console.WriteLine("BeginWriteCallBack: "+str);

        }

        /// <summary>
        /// 读取完成的回调
        /// </summary>
        /// <param name="ar"></param>
        private static void BeginReadCallBack(IAsyncResult ar)
        {
            MyClass m1 = ar.AsyncState as MyClass;
            if(m1==null)return;
            string str = Encoding.UTF8.GetString(m1.bytes);
            m1.stream.EndRead(ar);
            m1.stream.Close();
            m1.FileStream.Close();
            Console.WriteLine("BeginReadCallBack: " + str);
        }

        private class MyClass
        {
            public FileStream FileStream;
            public MemoryStream stream;
            public byte[] bytes;

            public MyClass(MemoryStream stream, byte[] bytes, FileStream fileStream)
            {
                this.FileStream = fileStream;
                this.stream = stream;
                this.bytes = bytes;
            }
        }

    }
}
