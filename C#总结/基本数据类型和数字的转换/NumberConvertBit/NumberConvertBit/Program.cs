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
 *      四、原理：
 *          byte数组，随着索引的增加,地址随之增加
 *          小端系统:低地址存放低位字节，高地址存放高位字节
 *
 *
 *      本地转换 ：X86CPU是小端系统，所以本地的数据保存都遵循 低地址保存低字节 高地址保存高字节
 *      网络转换 ：网络都是大端，所以根据本地的大小端来判断到底需要不需要进行大小端转换
 *          大小端转换:
 *      本地存储于字符串的区别  发送字符串，不需要考虑字节顺序
 */

using System;

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
            program.TestTostring();
            //// 将本地的int数值
            //int cc = 1;
            //Console.WriteLine(IPAddress.HostToNetworkOrder(cc));
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
            int intMins = -1111111111; // 原码 1100 0010 0011 1010 0011 0101 1100 0111 补码 1011 1101 1100 0101 1100 1010 0011 1001 --> 16进制表示 0xBD C5 CA 39
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
            long longMins = -1111111111; // 原码  1000 0000 0000 0000 0000 0000 0000 0000 0100 0010 0011 1010 0011 0101 1100 0111 补码 1111 1111 1111 1111 1111 1111 1111  1111 1011 1101 1100 0101 1100 1010 0011 1001 --> 16进制表示 0xFF FF FF FF BD C5 CA 39
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
            bool b2 = false;// 由测试可知 false 在内存中的补码为 0000 0000 ,原码 0000 0000
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
            float fmins = -13.0625f;// 0xC1510000
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
            doubleplus = -15.63;// 0xC02F428F5C28F5C3
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
            long longtype = BitConverter.DoubleToInt64Bits(1);// double 1 的二进制表示 0 011 1111 1111 0000000....16进制表示 0x3FF0000...
                                                              // 将double的码直接作为 int 的补码
                                                              // 获得 int 的原码 = 0x3FF0000... = (2^62-1)-（2^52-1）=2^62 - 2^52 = 4607182418800017408
            Console.WriteLine("{0:X}", longtype);
            Console.WriteLine(longtype);

            Console.WriteLine("-----DoubleToInt64Bits 负数-----");
            longtype = BitConverter.DoubleToInt64Bits(-1);// double -1 的二进制表示 1 011 1111 1111 0000000....16进制表示 0xBFF0000...
                                                          // 将double的码直接作为 int 的补码
                                                          // 获得 int 的原码 =  1100 0000 0001 0000.....   16进制表示 0xC010000...  = - (2^62)+（2^52）=2^62 - 2^52 = -4616189618054758400
            Console.WriteLine("{0:X}", longtype);
            Console.WriteLine(longtype);
        }

        // 测试从byte数组获得数字， 这里以 int 为例
        private void TestGetNumber()
        {
            // 正数
            byte[] bytes = new byte[]{1,2,3,4};//二进制表示 00000001 00000010 00000011 00000100 
            //在小端系统中，低字节存储在低地址 高字节存储在高地址
            //1 是 bytes数组的低地址 --> 转换到int数字的低地址中
            //4 是 bytes数组的高地址 --> 转换到int数字的高地址中
            // int 的内存表示就应该是(补码) 00000100000000110000001000000001 =原码 =67305985  16进制表示0x4030201 
            int value = BitConverter.ToInt32(bytes,0);
            Console.WriteLine("Value: {0}, 16进制： {1:X}",value,value);
            bytes = new byte[] { 1, 2, 3, 132 };//二进制表示 00000001 00000010 00000011 10000100 
            //在小端系统中，低字节存储在低地址 高字节存储在高地址
            //1   是 bytes数组的低地址 --> 转换到int数字的低地址中
            //132 是 bytes数组的高地址 --> 转换到int数字的高地址中
            //int 的内存表示就应该是(补码) 1000 0100 0000 0011 0000 0010 0000 0001 16进制表示0x84030201 > 原码 1111 1011 1111 1100 1111 1101 1111 1111 = -2080177663  
            value = BitConverter.ToInt32(bytes, 0);
            Console.WriteLine("Value: {0}, 16进制： {1:X}", value, value);

            // 别的类型也一样
        }

        private void TestTostring()
        {
            byte[] bytes = new byte[] { 1, 2, 3, 4 };
            string str = BitConverter.ToString(bytes);  // 将byte的值，转换成 16进制 0x01 0x02 0x03 0x04 ,并按顺序写出
            Console.WriteLine(str);
        }

        /// <summary>
        /// 网络应用测试
        /// </summary>
        private void NetWorkApply()
        {
            // 第一种 Array.Resay
            // 第二种IPAddress.HostToNetworkOrder()
            // 第三种IPAddress.NetworkToHostOrder()
        }
    }
}
