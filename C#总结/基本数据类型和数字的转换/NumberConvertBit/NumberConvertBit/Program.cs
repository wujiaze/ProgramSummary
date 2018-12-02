/*
 *      BitConverter  将基本数据类型转换为一个字节数组以及将一个字节数组转换为基本数据类型
 *
 *      一、转换方式：根据本地的大小端环境
 *
 *      二、适用基本数据类型
 *                 整型：short、ushort、int、unit、long、ulong
 *               布尔型：bool           一个字节
 *               字符型：char           两个字节
 *               浮点型：float 四个字节、double 八个字节
 *
 *      三、工具： byte
 *          因为byte能表示 0000 0000 ~ 1111 1111 ,所以用它可以作为工具
 *
 *      四、小端环境原理：
 *          byte数组，随着索引的增加,地址随之增加(todo 大端系统测试)
 *          小端系统:低地址存放低位字节，高地址存放高位字节
 *
 *      五、大小端转换
 *      本地转换 ：X86CPU是小端系统，所以本地的数据保存都遵循 低地址保存低字节 高地址保存高字节
 *
 *      网络转换 ：网络都是大端，所以根据本地的大小端 以及对方的大小端 来判断到底需要不需要进行大小端转换
 *               对方是小端  网络是大端  己方是小端  -- 刚好"抵消"，不需要大小端转换，但是标准的做法是 双方都做一下转换，这样就可以跨平台一劳永逸
 *               对方是小端  网络是大端  己方是大端  -- 需要进行大小端转换
 *               对方是大端  网络是大端  己方是小端  -- 需要进行大小端转换
 *               对方是大端  网络是大端  己方是大端  -- 不需要进行大小端转换
 *        总结: 1、对方和己方的大小端相同时，无需对传输的字节数组进行大小端转换 (负数)
 *              2、对方和己方的大小端不同时，需要由其中一方转换，约定是使数据在网络以大端传输，所以一般是小端系统转换成大端
 *                 2.1、大小端转换方法:
 *                          详见 NumberNetConvertTool 
 *
 *      六、存储
 *          不能直接将 数组转换成byte数组，写入文本，还是需要使用字符串，采用字符编码写入文本，或者使用序列化的方法
 *
 *      七、字符串为什么不需要大小端转换
 *          1、各种字符，在所有的编码方式中，都有一套内部的方法(编码方案)，不受外界环境的影响
 *          2、数字字符串，在大部分编码中都是单字节表示(内部的方法)，只有UTF16是双字节表示，所以UTF16有大小端，但是也不用手动转，提供了方法
 *          总结：字符串，不需要考虑字节顺序
 */

using System;
using System.IO;

namespace NumberConvertBit
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            //program.JudgeSystem();
            //program.TestGetBytes();
            //program.TestGetNumber();
            //program.TestTostring();
            program.NetWorkApply();
            Console.Read();
        }



        /// <summary>
        /// 判断系统的大端小端
        /// </summary>
        private void JudgeSystem()
        {
            bool result = BitConverter.IsLittleEndian;
            if (result)
                Console.WriteLine("小端系统");
            else
                Console.WriteLine("大端系统");
        }

        /// <summary>
        /// 测试各类数据转换成Byte数组
        /// </summary>

        private void TestGetBytes()
        {
            Console.WriteLine("-------short------");
            short s = -11111; // 原码 1010 1011 0110 0111 补码 1101 0100 1001 1001 ,补码16进制表示 0xD499 ，也是内存中的存储值
            byte[] bytes1 = BitConverter.GetBytes(s);
            for (int i = 0; i < bytes1.Length; i++)
            {
                unsafe
                {
                    fixed (byte* p1 = &bytes1[i])
                    {
                        // byte数组，随着索引的增加,地址随之增加
                        // 小端系统:低地址存放低位字节 99 ，高地址存放高位字节 D4
                        Console.WriteLine("Address:{0}, Value:{1:X}", (int)p1, bytes1[i]);
                    }
                }
            }

            Console.WriteLine("-------ushort------");
            ushort us = 11111; // 16进制表示 0x2B67  原码 0010 1011 0110 0111  = 补码
            byte[] bytes2 = BitConverter.GetBytes(us);
            for (int i = 0; i < bytes2.Length; i++)
            {
                unsafe
                {
                    fixed (byte* p1 = &bytes2[i])
                    {
                        // byte数组，随着索引的增加,地址随之增加
                        // 小端系统:低地址存放低位字节 67 ，高地址存放高位字节 2B
                        Console.WriteLine("Address:{0}, Value:{1:X}", (int)p1, bytes2[i]);
                    }
                }
            }

            Console.WriteLine("-------int------");
            int intMins =
                -1111111111; // 原码 1100 0010 0011 1010 0011 0101 1100 0111 补码 1011 1101 1100 0101 1100 1010 0011 1001 --> 16进制表示 0xBD C5 CA 39
            byte[] intBytes = BitConverter.GetBytes(intMins);
            for (int i = 0; i < intBytes.Length; i++)
            {
                unsafe
                {
                    fixed (byte* p1 = &intBytes[i])
                    {
                        // byte数组，随着索引的增加,地址随之增加
                        // 小端系统:低地址存放低位字节 39 ，高地址存放高位字节 BD
                        Console.WriteLine("Address:{0}, Value:{1:X}", (int)p1, intBytes[i]);
                    }
                }
            }

            Console.WriteLine("-------uint------");
            uint uintMins = 1111111111; // 原码 0100 0010 0011 1010 0011 0101 1100 0111 补码 = 自身 --> 16进制表示 0x423A35C7
            byte[] uintBytes = BitConverter.GetBytes(uintMins);
            for (int i = 0; i < uintBytes.Length; i++)
            {
                unsafe
                {
                    fixed (byte* p1 = &uintBytes[i])
                    {
                        // byte数组，随着索引的增加,地址随之增加
                        // 小端系统:低地址存放低位字节 C7 ，高地址存放高位字节 42
                        Console.WriteLine("Address:{0}, Value:{1:X}", (int)p1, uintBytes[i]);
                    }
                }
            }

            Console.WriteLine("-------long------");
            long
                longMins = -1111111111; // 原码  1000 0000 0000 0000 0000 0000 0000 0000 0100 0010 0011 1010 0011 0101 1100 0111 补码 1111 1111 1111 1111 1111 1111 1111  1111 1011 1101 1100 0101 1100 1010 0011 1001 --> 16进制表示 0xFF FF FF FF BD C5 CA 39
            byte[] longBytes = BitConverter.GetBytes(longMins);
            for (int i = 0; i < longBytes.Length; i++)
            {
                unsafe
                {
                    fixed (byte* p1 = &longBytes[i])
                    {
                        // byte数组，随着索引的增加,地址随之增加
                        // 小端系统:低地址存放低位字节 39 ，高地址存放高位字节 FF
                        Console.WriteLine("Address:{0}, Value:{1:X}", (int)p1, longBytes[i]);
                    }
                }
            }

            Console.WriteLine("-------ulong------");
            ulong ulongMins = 1111111111; // 16进制表示 0x00 00 00 00 42 3A 35 C7
            byte[] ulongBytes = BitConverter.GetBytes(ulongMins);
            for (int i = 0; i < ulongBytes.Length; i++)
            {
                unsafe
                {
                    fixed (byte* p1 = &ulongBytes[i])
                    {
                        // byte数组，随着索引的增加,地址随之增加
                        // 小端系统:低地址存放低位字节 C7 ，高地址存放高位字节 00
                        Console.WriteLine("Address:{0}, Value:{1:X}", (int)p1, ulongBytes[i]);
                    }
                }
            }

            Console.WriteLine("-------bool------");
            bool b1 = true; // 由测试可知 true  在内存中的补码为 0000 0001 ,原码 0000 0001
            bool b2 = false; // 由测试可知 false 在内存中的补码为 0000 0000 ,原码 0000 0000
            byte[] boolBytes1 = BitConverter.GetBytes(b1);
            byte[] boolBytes2 = BitConverter.GetBytes(b2);
            for (int i = 0; i < boolBytes1.Length; i++)
            {
                unsafe
                {
                    fixed (byte* p1 = &boolBytes1[i])
                    {
                        Console.WriteLine("Address:{0}, Value:{1:X}", (int)p1, boolBytes1[i]);
                    }

                    fixed (byte* p1 = &boolBytes2[i])
                    {
                        Console.WriteLine("Address:{0}, Value:{1:X}", (int)p1, boolBytes2[i]);
                    }
                }
            }

            Console.WriteLine("-------char------");
            char c1 = '中'; // ASCII对应 :90  原码: 0000 0000 0101 1010 补码=原码 16进制:0x005A
            byte[] charBytes = BitConverter.GetBytes(c1);
            for (int i = 0; i < charBytes.Length; i++)
            {
                unsafe
                {
                    fixed (byte* p1 = &charBytes[i])
                    {
                        // byte数组，随着索引的增加,地址随之增加
                        // 小端系统:低地址存放低位字节 5A ，高地址存放高位字节 00
                        Console.WriteLine("Address:{0}, Value:{1:X}", (int)p1, charBytes[i]);
                    }
                }
            }

            Console.WriteLine("-------float 正数------");
            float fplus = 13.0625f; // 0x41510000 来源详见原码、反码、补码
            float fmins = -13.0625f; // 0xC1510000
            byte[] fplusBytes = BitConverter.GetBytes(fplus);
            byte[] fminsBytes = BitConverter.GetBytes(fmins);
            for (int i = 0; i < fplusBytes.Length; i++)
            {
                unsafe
                {
                    fixed (byte* p1 = &fplusBytes[i])
                    {
                        // byte数组，随着索引的增加,地址随之增加
                        // 小端系统:低地址存放低位字节 00 ，高地址存放高位字节 41
                        Console.WriteLine("Address:{0}, Value:{1:X}", (int)p1, fplusBytes[i]);
                    }
                }
            }

            Console.WriteLine("------float 负数-----");
            for (int i = 0; i < fminsBytes.Length; i++)
            {
                unsafe
                {
                    fixed (byte* p1 = &fminsBytes[i])
                    {
                        // byte数组，随着索引的增加,地址随之增加
                        // 小端系统:低地址存放低位字节 00 ，高地址存放高位字节 41
                        Console.WriteLine("Address:{0}, Value:{1:X}", (int)p1, fminsBytes[i]);
                    }
                }
            }


            Console.WriteLine("------double   正数-----");
            double doubleplus = 15.63; // 0x402F428F5C28F5C3
            byte[] doubleplusBytes = BitConverter.GetBytes(doubleplus);
            for (int i = 0; i < doubleplusBytes.Length; i++)
            {
                unsafe
                {
                    fixed (byte* p1 = &doubleplusBytes[i])
                    {
                        // byte数组，随着索引的增加,地址随之增加
                        // 小端系统:低地址存放低位字节 00 ，高地址存放高位字节 41
                        Console.WriteLine("Address:{0}, Value:{1:X}", (int)p1, doubleplusBytes[i]);
                    }
                }
            }

            Console.WriteLine("-----double 负数-----");
            doubleplus = -15.63; // 0xC02F428F5C28F5C3
            doubleplusBytes = BitConverter.GetBytes(doubleplus);
            for (int i = 0; i < doubleplusBytes.Length; i++)
            {
                unsafe
                {
                    fixed (byte* p1 = &doubleplusBytes[i])
                    {
                        // byte数组，随着索引的增加,地址随之增加
                        // 小端系统:低地址存放低位字节 00 ，高地址存放高位字节 41
                        Console.WriteLine("Address:{0}, Value:{1:X}", (int)p1, doubleplusBytes[i]);
                    }
                }
            }

            // 将Double转换成有符号的Int数
            Console.WriteLine("-----DoubleToInt64Bits 正数-----");
            long longtype =
                BitConverter.DoubleToInt64Bits(1); // double 1 的二进制表示 0 011 1111 1111 0000000....16进制表示 0x3FF0000...
            // 将double的码直接作为 int 的补码
            // 获得 int 的原码 = 0x3FF0000... = (2^62-1)-（2^52-1）=2^62 - 2^52 = 4607182418800017408
            Console.WriteLine("{0:X}", longtype);
            Console.WriteLine(longtype);

            Console.WriteLine("-----DoubleToInt64Bits 负数-----");
            longtype = BitConverter
                .DoubleToInt64Bits(-1); // double -1 的二进制表示 1 011 1111 1111 0000000....16进制表示 0xBFF0000...
            // 将double的码直接作为 int 的补码
            // 获得 int 的原码 =  1100 0000 0001 0000.....   16进制表示 0xC010000...  = - (2^62)+（2^52）=2^62 - 2^52 = -4616189618054758400
            Console.WriteLine("{0:X}", longtype);
            Console.WriteLine(longtype);
        }

        // 测试从byte数组获得数字， 这里以 int 为例
        private void TestGetNumber()
        {
            // 正数
            byte[] bytes = new byte[] { 1, 2, 3, 4 }; //二进制表示 00000001 00000010 00000011 00000100 
            //在小端系统中，低字节存储在低地址 高字节存储在高地址
            //1 是 bytes数组的低地址 --> 转换到int数字的低地址中
            //4 是 bytes数组的高地址 --> 转换到int数字的高地址中
            // int 的内存表示就应该是(补码) 00000100000000110000001000000001 =原码 =67305985  16进制表示0x4030201 
            int value = BitConverter.ToInt32(bytes, 0);
            Console.WriteLine("Value: {0}, 16进制： {1:X}", value, value);
            bytes = new byte[] { 1, 2, 3, 132 }; //二进制表示 00000001 00000010 00000011 10000100 
            //在小端系统中，低字节存储在低地址 高字节存储在高地址
            //1   是 bytes数组的低地址 --> 转换到int数字的低地址中
            //132 是 bytes数组的高地址 --> 转换到int数字的高地址中
            //int 的内存表示就应该是(补码) 1000 0100 0000 0011 0000 0010 0000 0001 16进制表示0x84030201 > 原码 1111 1011 1111 1100 1111 1101 1111 1111 = -2080177663  
            value = BitConverter.ToInt32(bytes, 0);
            Console.WriteLine("Value: {0}, 16进制： {1:X}", value, value);

            // 别的类型也一样
        }

        /// <summary>
        /// 测试 BitConverter.ToString 方法
        /// </summary>
        private void TestTostring()
        {
            byte[] bytes = new byte[] { 1, 2, 3, 4 };
            string str = BitConverter.ToString(bytes); // 将byte的值，转换成 16进制 0x01 0x02 0x03 0x04 ,并按顺序写出
            Console.WriteLine(str);
        }

        private void NetWorkApply()
        {
            int intplus = 100000;// 原码 0x 00 01 86 A0
            Console.WriteLine("---发送方---");
            Console.WriteLine("方法1");
            byte[] result = NumberNetConvertTool.NetSendConvert1(intplus);
            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine("{0:X}", result[i]);
            }
            Console.WriteLine("方法2");
            result = NumberNetConvertTool.NetSendConvert2(intplus);
            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine("{0:X}", result[i]);
            }
            Console.WriteLine("---接受方---");
            Console.WriteLine("方法1");
            int intResult = NumberNetConvertTool.NetReceiveConvert1(result);
            Console.WriteLine(intResult);
            Console.WriteLine("方法2");
            result = NumberNetConvertTool.NetSendConvert2(intplus);
            intResult = NumberNetConvertTool.NetReceiveConvert2(result);
            Console.WriteLine(intResult);
        }
    }
}
