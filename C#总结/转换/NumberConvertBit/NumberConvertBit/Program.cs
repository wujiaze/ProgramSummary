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
 *      本地转换
 *      网络转换
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
            program.JudgeSystem();

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
            double doubleplus = 15.63;
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
            doubleplus = -15.63;
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


            long longtype = BitConverter.DoubleToInt64Bits(doubleplus);
            Console.WriteLine(longtype);
            //// 将本地的int数值
            //int cc = 1;
            //Console.WriteLine(IPAddress.HostToNetworkOrder(cc));


            //int value = 9;
            //byte[] bytes = BitConverter.GetBytes('中');  // 小端Unicode形式，若需要写入文件需要添加 小端头 0xFF 0xFE
            //Console.WriteLine(bytes.Length);
            //foreach (byte b in bytes)
            //{
            //    Console.WriteLine(b);
            //}
            //Console.WriteLine(BitConverter.ToString(bytes));

            //Console.WriteLine("-------------");
            //byte[] intBytes = BitConverter.GetBytes(1);
            //foreach (var intByte in intBytes)
            //{
            //    Console.WriteLine(intByte);
            //}
            //Console.WriteLine("-------------");
            //byte[] bbb = Encoding.Unicode.GetPreamble();
            //foreach (byte b in bbb)
            //{
            //    Console.WriteLine(b);
            //}
            //byte[] bytes2 = Encoding.Unicode.GetBytes("中");
            //Console.WriteLine(bytes2.Length);
            //foreach (byte b in bytes2)
            //{
            //    Console.WriteLine(b);
            //}
            //using (FileStream fs = File.Create(@"D:\Desktop\t.txt"))
            //{
            //    fs.Write(bbb, 0, bbb.Length);
            //    Console.WriteLine(fs.Position);

            //    fs.Write(bytes, 0, bytes.Length);
            //    Console.WriteLine(fs.Position);
            //}
            //Console.WriteLine("-------------");
            //byte[] str1bytes = Encoding.ASCII.GetBytes("1");
            //foreach (var str1Byte in str1bytes)
            //{
            //    Console.WriteLine(str1Byte);
            //}
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
    }
}
