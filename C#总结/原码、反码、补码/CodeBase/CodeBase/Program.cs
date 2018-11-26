/*
 *      前提基础：             http://www.cnblogs.com/zhangziqiu/archive/2011/03/30/ComputerCode.html#!comments
 *          内存中对数值的存储：存储数值的补码
 *          内存中对数值的计算：只有加法：所以采用补码表示
 *          ±0：都表示为正数 0
 *
 *      以下的码：都是二进制表示
 *              
 *      原码：[-x,x]
 *           第一位表示符号，其余位表示数值
 *
 *      反码：[-x,x]
 *           正数的反码 --> 正数本身
 *           负数的反码 --> 原码的符号位不变,原码的其余位取反
 *
 *      补码：[-(x+1),x]
 *           原因：由于反码计算会出现 ±0,所以出现了补码计算，补码计算解决了±0，并且还增加了一个数字表示，但是这个数字只有补码的表示，没有原码及反码表示
 *                  
 *           正数的补码 --> 正数本身
 *           负数的补码 --> 原码的符号位不变,原码的其余位取反，再 +1
 *
 *      计算：
 *          原码 + 原码 = 原码    计算时，按照正常的来算，但是最后只取后8位
 *          反码 + 反码 = 反码    计算时，按照正常的来算，但是最后只取后8位
 *          补码 + 补码 = 补码    计算时，按照正常的来算，但是最后只取后8位
 *
 *      转换:
 *          原码的反码 = 反码
 *          反码的反码 = 原码
 *
 *          原码的补码 = 补码
 *          补码的补码 = 原码
 *
 *      有符号的数：根据正负，满足以上规律
 *      无符号的数：全部作为正数，满足以上规律
 *
 *
 *      工具： byte
 *          因为byte能表示 0000 0000 ~ 1111 1111 ,所以用它可以作为工具
 *
 *      适用类型：
 *         整型：byte sbyte short ushort int uint long ulong
 *         布尔型 bool
 *         字符型 char
 */
using System;

namespace CodeBase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------有符号的数值----------");
            ComfirmComplement();
            ComfirmZero();
            Console.WriteLine("------sbyte------");
            // 有符号
            sbyte sbPlus = 127; // 原码 0111 1111 
            sbyte sbMins = -127;// 原码 1111 1111
            sbyte sbAdd = -128; // 没有原码
            sbyte sbZero = 0;   // 原码 0000 0000
            unsafe
            {
                sbyte* p1 = &sbPlus;
                sbyte* p2 = &sbMins;
                sbyte* p3 = &sbAdd;
                sbyte* p4 = &sbZero;
                Console.WriteLine("{0:X}", *p1);// 得到 0x7F,从原码 0111 1111 --> 0111 1111 说明内存存储数据采用了补码
                Console.WriteLine("{0:X}", *p2);// 得到 0x81,从原码 1111 1111 --> 1000 0001 说明内存存储数据采用了补码 
                Console.WriteLine("{0:X}", *p3);// 得到 0x80,只有补码 1000 0000 
                Console.WriteLine("{0:X}", *p4);// 得到 0x00,从原码 0000 0000 --> 0000 0000 说明内存存储数据采用了补码 
            }

            Console.WriteLine("-------short-------");
            short shPlus = 32767;    // 原码 0111 1111 1111 1111  16进制 0x7FFF
            short shMins = -32767;   // 原码 1111 1111 1111 1111  16进制 0xFFFF   
            short shAdd = -32768;
            short shZero = 0;
            unsafe
            {
                short* p1 = &shPlus;
                short* p2 = &shMins;
                short* p3 = &shAdd;
                short* p4 = &shZero;
                Console.WriteLine("{0:X}", *p1);// 得到 0x7FFF,从原码 0111 1111 1111 1111 --> 0111 1111 1111 1111 说明内存存储数据采用了补码
                Console.WriteLine("{0:X}", *p2);// 得到 0x8001,从原码 1111 1111 1111 1111 --> 1000 0000 0000 0001 说明内存存储数据采用了补码 
                Console.WriteLine("{0:X}", *p3);// 得到 0x8000,只有补码 1000 0000 0000 0000 
                Console.WriteLine("{0:X}", *p4);// 得到 0x0000,从原码 0000 0000 0000 0000 --> 0000 0000 0000 0000 说明内存存储数据采用了补码 
            }

            Console.WriteLine("--------int-------");
            int intPlus = 2147483647;
            int intMins = -2147483647;
            int intAdd = -2147483648;
            int intZero = 0;
            unsafe
            {
                int* p1 = &intPlus;
                int* p2 = &intMins;
                int* p3 = &intAdd;
                int* p4 = &intZero;
                Console.WriteLine("{0:X}", *p1);// 得到 0x7FFFFFFF,从原码 0111 1111 1111 1111 1111 1111 1111 1111 --> 0111 1111 1111 1111 1111 1111 1111 1111 说明内存存储数据采用了补码
                Console.WriteLine("{0:X}", *p2);// 得到 0x80000001,从原码 1111 1111 1111 1111 1111 1111 1111 1111 --> 1000 0000 0000 0000 0000 0000 0000 0001 说明内存存储数据采用了补码 
                Console.WriteLine("{0:X}", *p3);// 得到 0x80000000,只有补码 1000 0000 0000 0000 0000 0000 0000 0000 
                Console.WriteLine("{0:X}", *p4);// 得到 0x00000000,从原码 0000 0000 0000 0000 0000 0000 0000 0000 --> 0000 0000 0000 0000 0000 0000 0000 0000 说明内存存储数据采用了补码 
            }
            Console.WriteLine("--------long-------");
            long longPlus = 9223372036854775807;
            long longMins = -9223372036854775807;
            long longAdd = -9223372036854775808;
            long longZero = 0;
            unsafe
            {
                long* p1 = &longPlus;
                long* p2 = &longMins;
                long* p3 = &longAdd;
                long* p4 = &longZero;
                Console.WriteLine("{0:X}", *p1);// 得到 0x7FFFFFFF FFFFFFFF
                Console.WriteLine("{0:X}", *p2);// 得到 0x80000000 00000001
                Console.WriteLine("{0:X}", *p3);// 得到 0x80000000 00000000
                Console.WriteLine("{0:X}", *p4);// 得到 0x00000000 00000000
            }


            Console.WriteLine("-----------无符号的数值-----------");
            Console.WriteLine("--byte---");
            // 无符号  byte 
            byte bPlus = 255;  //原码 1111 1111
            byte bMins = 0;    //原码 0000 0000
            unsafe
            {
                byte* p1 = &bPlus;
                byte* p2 = &bMins;
                Console.WriteLine("{0:X}", *p1); // 0xFF,作为正数，补码 = 原码
                Console.WriteLine("{0:X}", *p2); // 0x00,作为正数，补码 = 原码
            }

            Console.WriteLine("----ushort----");
            ushort usPlus = 65535;  //原码 1111 1111 1111 1111
            ushort usMins = 0;      //原码 0000 0000 0000 0000
            unsafe
            {
                ushort* p1 = &usPlus;
                ushort* p2 = &usMins;
                Console.WriteLine("{0:X}", *p1); // 0xFFFF,作为正数，补码 = 原码
                Console.WriteLine("{0:X}", *p2); // 0x0000,作为正数，补码 = 原码
            }

            Console.WriteLine("----uint----");
            uint uintPlus = 4294967295; // 原码 1111 1111 1111 1111 1111 1111 1111 1111
            uint uintMins = 0;          // 原码 0000 0000 0000 0000 0000 0000 0000 0000
            unsafe
            {
                uint* p1 = &uintPlus;
                uint* p2 = &uintMins;
                Console.WriteLine("{0:X}", *p1); // 0xFFFFFFFF,作为正数，补码 = 原码
                Console.WriteLine("{0:X}", *p2); // 0x00000000,作为正数，补码 = 原码
            }

            Console.WriteLine("----ulong----");
            ulong ulongPlus = 18446744073709551615;
            ulong ulongMins = 0;
            unsafe
            {
                ulong* p1 = &ulongPlus;
                ulong* p2 = &ulongMins;
                Console.WriteLine("{0:X}", *p1); // 0xFFFFFFFFFFFFFFFF,作为正数，补码 = 原码
                Console.WriteLine("{0:X}", *p2); // 0x0000000000000000,作为正数，补码 = 原码
            }



            Console.WriteLine("-----------bool-----------");
            // 无符号  byte 
            bool booltrue = true;      //原码 0000 0001
            bool boolfalse = false;    //原码 0000 0000
            unsafe
            {
                byte* p1 = (byte*)&booltrue;
                byte* p2 = (byte*)&boolfalse;
                Console.WriteLine("{0:X}", *p1); // 0x01,作为正数，补码 = 原码
                Console.WriteLine("{0:X}", *p2); // 0x00,作为正数，补码 = 原码
            }

            Console.WriteLine("-----------char-----------");
            // 无符号  byte 
            char c1 = 'Z';      //原码 0000 0000 0111 1010
            unsafe
            {
                byte* p2 = (byte*)&c1;
                for (int i = 0; i < sizeof(char); i++)
                {
                    Console.WriteLine("Address: {0},Value: {1:X}",(int)p2, *p2); // 0x005A,作为正数，补码 = 原码
                    p2++;
                }
               
            }
            Console.Read();
        }

        /// <summary>
        /// 证明 内存存储数据采用补码
        /// </summary>
        private static void ComfirmComplement()
        {
            Console.WriteLine("----证明内存存储数据采用补码----");
            sbyte sbValue = -1;
            unsafe
            {
                sbyte* p = &sbValue;
                Console.WriteLine("-1在内存中的存储表示:  {0:X}", *p);  // 得到 FF,从 1000 0001 --> 1111 1111 说明内存存储数据采用了补码
            }
            Console.WriteLine("\n----证明内存存储数据采用补码----");
            short value = -1;
            unsafe
            {
                short* p = &value;
                Console.WriteLine("-1在内存中的存储表示:{0:X}", *p);  // 得到 FFFF,从 1000 0000 0000 0001 --> 1111 1111 1111 1111 说明内存存储数据采用了补码
                byte* pByte = (byte*)p;
                for (int i = 0; i < sizeof(short); i++)
                {
                    Console.WriteLine("每个地址的二进制表示 {0:X}", *pByte); // 进一步证明 内存存储数据采用了补码
                    pByte++;
                }
            }
        }

        /// <summary>
        /// 证明内存中±0都表示为正数0
        /// </summary>
        private static void ComfirmZero()
        {
            Console.WriteLine("\n----证明内存中±0都表示为正数0----");
            sbyte sbValueZero = 0;
            sbyte sbValueZero2 = -0;
            unsafe
            {
                sbyte* p1 = &sbValueZero;
                Console.WriteLine("0在内存中的存储表示:  {0:X}", *p1);  // 得到 0
                sbyte* p2 = &sbValueZero2;
                Console.WriteLine("-0在内存中的存储表示:  {0:X}", *p2);  // 得到 0
            }
        }
    }
}
