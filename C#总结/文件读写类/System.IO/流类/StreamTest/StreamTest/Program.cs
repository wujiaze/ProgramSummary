using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StreamTest
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Stream流，定义：提供字节序列的一般视图
             * 字节序列， 定义：字节对象都被存储为连续的字节序列；字节按照一定的顺序进行排序组成了字节序列
             *
             * Stream 是抽象类，为子类提供了许多方法和属性，以下是自定义流继承Stream，必须实现重写的方法和属性，即比较重要的成员
             *
             * 属性：
             * CanRead： 只读属性，判断该流是否可读
             * CanWrite: 只写属性，判断该流是否可写
             * CanSeek： 只读属性，判断啊该流是否支持跟踪查找
             * Length：  只读属性，表示该流的整个长度
             * Position：可读可写属性，表示当前在该流中的位置
             * 
             * 方法：
             *
             * Read(byte[] buffer, int offset, int count)： 从流中的offset位置读取count长度的字节，存入内存buffer数组
             * Write(byte[] buffer, int offset, int count)：将内存buffer数组，在流的offset位置开始，写入count的长度
             * Seek(long offset, SeekOrigin origin)：       重新设定流中的位置
             *                                              Origin.Begin: 流开始的位置  Origin.Current：流当前的位置  Origin.End： 流结束的位置
             *                                              offset ：相对于上述位置的偏移量
             * SetLength(long value)                        设置当前流的长度 TODO
             * Flush()                                      清除该流的所有缓冲数据，并将缓冲数据写入基础设施(PC,移动设备等等，一般可以理解为文件) todo
             *
             */


            // Todo 流的异步操作
            // todo Close Dispose 使用using就可以了
            //// 内存流
            //MemoryStream ms   = new MemoryStream();
            //// 文件流
            FileStream fs = new FileStream("", FileMode.OpenOrCreate);
            //// 缓存流
            //BufferedStream bs = new BufferedStream(ms);
            // 网络流
            //NetworkStream
            // 压缩流
            //DeflateStream
            // 加密流
            //CryptoStream
        }


    }
}
