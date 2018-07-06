using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryRWTest
{
    // 一般不和FileStream一起用
    // 更多的是和MemoryStream
    class Program
    {
        static void Main(string[] args)
        {
            /*  BinaryWriter 是对流进行操作
             *  构造函数
             *   第一类: protect 用于重构  BinaryWriter
             *     1.1 BinaryWriter()
             *   第二类
             *     2.1 BinaryWriter(Stream output, Encoding encoding, bool leaveOpen)   //leaveOpen: BinaryWriter 关闭是否关闭底层流
             *     2.2 BinaryWriter(       output,          encoding,      fasle    )
             *     2.3 BinaryWriter(       output,          UTF8    ,      false    )
             */
            /*
             * 属性
             * BaseStream
             */
            /*
             * 方法
             * Write ：内部全部转换成 byte 数组,但是和 StreamWrite 不同，BinaryWrite 采用的是移位的转化方式，性能更好
             *  Write(bool value)       Write(char ch)          Write(string value)
             *  Write(float value)      Write(double value)     Write(Decimal value)     
             *  Write(byte value)       Write(sbyte value)      Write(short value)      Write(ushort value)
             *  Write(int value)        Write(uint value)       Write(long value)       Write(ulong value)
             *  
             *  Write(byte[] buffer)    Write(byte[] buffer, int index, int count)
             *  Write(char[] chars)     Write(char[] chars, int index, int count)
             *
             * Seek：   内部采用对应流的Seek方法
             * Flush:   内部采用对应流的Flush方法
             * Close/Dispose  在内部是一样的
             */
            Console.WriteLine("-------------- BinaryWriter ----------------");
            using (FileStream stream = new FileStream("D:\\Desktop\\Binaray.txt",FileMode.OpenOrCreate))
            {
                using (BinaryWriter writer = new BinaryWriter(stream,Encoding.UTF8,false))
                {
                    FileStream baseStream = (FileStream)writer.BaseStream;              //这样也能通过流来操作，但是就不符合 BinaryWriter 的作用了
                    writer.Write(true);
                    writer.Write("abc");
                    char[] chararr = new char[]{'&','|'};
                    writer.Write(chararr,1,1);
                    byte[] byteArr = new byte[]{100,101,102};
                    writer.Write(byteArr,1,1);                                          // BinaryWrite 方法写入，可能看上去会有点乱码，但是只要用  BinaryReader 来读，就没有问题
                }
            }
            /*  BinaryReader 是对流进行操作
             *  构造函数
             *  BinaryReader(Stream input, Encoding encoding, bool leaveOpen)
             *  BinaryReader(       input,          encoding,      false    )
             *  BinaryReader(       input,          UTF8,          false    )
             */
            /*
             *  属性
             *  BaseStream
             */
            /*
             * 方法
             *  Read 跟 Write 方法是一一对应的
             *  ReadBoolean()   ReadChar()      ReadString()
             *  ReadSingle()    ReadDouble()    ReadDecimal()   
             *  ReadByte()      ReadSByte()     ReadInt16()     ReadUInt16()
             *  ReadInt32()     ReadUInt32()    ReadInt64()     ReadUInt64()
             *  ReadBytes(int count)    Read(byte[] buffer, int index, int count)  
             *  ReadChars(int count)    Read(char[] buffer, int index, int count)
             *
             *  Read        作用类似于 ReadChar，读取字符，但是返回的不是 char 类型，而是对应 char 的 int 类型
             *  PeekChar    在Read的方法之后，重新将Position设置为原来的位置   
             *
             *  Close/Dispose
             *  
             */
            Console.WriteLine("-------------- BinaryReader ----------------");
            using (FileStream stream = new FileStream("D:\\Desktop\\Binaray.txt", FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, false))
                {
                    // 注意读取的顺序，一定要和写入的顺序一致
                    Console.WriteLine(reader.ReadBoolean());
                    Console.WriteLine(reader.ReadString());
                    char[] charArr = reader.ReadChars(1);
                    Console.WriteLine(charArr[0]);              // 只能手动输入 char 的长度
                    byte[] byteArr = new byte[3];
                    int readNum = reader.Read(byteArr, 0, 1);   // 只能手动输入 byte 的长度
                    Console.WriteLine(byteArr[0]);
                }
            }

            Console.ReadLine();

        }


    }
}
