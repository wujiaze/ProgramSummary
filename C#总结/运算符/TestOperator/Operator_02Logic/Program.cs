/*
 *      逻辑运算符
 *          定义：对一个数的二进制表示(补码形式)，针对 每一位 进行操作，不影响 该位左右两边的位,也不会溢出
 *              补码形式：因为内存中都是以补码的形式存在
 *      一、& 按位与      
 *          定义：比较两个操作位，都为1时返回1，其余情况返回0
 *          操作：
 *          bool型: 适用类型 bool
 *               两个bool值，转换成补码进行按位与
 *          int型 : 适用类型 byte sbyte short ushort char int uint
 *               将两个数转换成 int 类型，再转换成补码形式，最后进行按位与
 *          long型：适用类型 long ulong
 *              两个long值，转换成补码进行按位与
 *      二、| 按位或
 *          定义：比较两个操作位，有一个为1时返回1，其余情况返回0
 *          操作：
 *          bool型: 适用类型 bool
 *               两个bool值，转换成补码进行按位或
 *          int型 : 适用类型 byte sbyte short ushort char int uint
 *               将两个数转换成 int 类型，再转换成补码形式，最后进行按位或
 *          long型：适用类型 long ulong
 *              两个long值，转换成补码进行按位或
 *      三、^ 按位异或
 *          定义：比较两个操作数，不一样时返回1，一样时返回0
 *          操作：
 *          bool型: 适用类型 bool
 *               两个bool值，转换成补码进行按位异或
 *          int型 : 适用类型 byte sbyte short ushort char int uint
 *               将两个数转换成 int 类型，再转换成补码形式，最后进行按位异或
 *          long型：适用类型 long ulong
 *              两个long值，转换成补码进行按位异或
 *      四、~ 按位非
 *          定义：按位取反
 *          操作:
 *          int型 : 适用类型 byte sbyte short ushort char int uint
 *               将两个数转换成 int 类型，再转换成补码形式，最后进行按位异或
 *          long型：适用类型 long ulong
 *              两个long值，转换成补码进行按位异或
 */

using System;

namespace Operator_02Logic
{
    class Program
    {
        static void Main(string[] args)
        {
            // 按位与运算 测试证明
            //ComfirmBitAnd();
            //ComfirmBitOr();
            //ComfirmBitXor();
            //ComfirmBitNot();

            // 按位运算的实际应用(例子)
            //BitMask();
            //BitCheck();
            //BitOpen2One();
            //BitClose2Zero();
            BitSwitch();
            Console.Read();
        }

        // 按位与运算 测试了sbyte bool char
        public static void ComfirmBitAnd()
        {
            /* 按位与 方便起见(有正负)，采用sbyte*/
            sbyte splus1 = 5;   // 原码 0000 0101    int形式--> 0000 0000 0000 0000 0000 0000 0000 0101
            sbyte splus2 = 7;   // 原码 0000 0111    int形式--> 0000 0000 0000 0000 0000 0000 0000 0111
            sbyte smins1 = -5;  // 原码 1000 0101    int形式--> 1000 0000 0000 0000 0000 0000 0000 0101
            sbyte smins2 = -7;  // 原码 1000 0111    int形式--> 1000 0000 0000 0000 0000 0000 0000 0111
            Console.WriteLine("-----按位与  正数------");
            // 正数
            // splus1 的int补码形式 ：0000 0000 0000 0000 0000 0000 0000 0101
            // splus2 的int补码形式 ：0000 0000 0000 0000 0000 0000 0000 0111
            //    按位与运算,得到补码：0000 0000 0000 0000 0000 0000 0000 0101  = 5
            int resultValue = splus1 & splus2;
            Console.WriteLine("IntValue:{0}", resultValue);
            // 又因为位运算不影响左右的位，所以可以直接将 int类型 显示转换成 sbyte类型(舍弃高地址，只取低地址)，而不影响结果
            //    即只取低地址的补码： 0000 0101
            //   sbyte有符号的新原码:  0000 0101  = 5
            sbyte resultSbyte = (sbyte)resultValue;
            Console.WriteLine("SbyteValue:{0}", resultSbyte);
            // 指针验证
            unsafe
            {
                byte* p = (byte*)&resultValue;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Adress: {0}, Value: {1:X}", (int)p, *p);
                    p++;
                }
            }
            Console.WriteLine("-----按位与  负数------");
            // 负数
            // smins1 的int补码形式 ：1111 1111 1111 1111 1111 1111 1111 1011
            // smins2 的int补码形式 ：1111 1111 1111 1111 1111 1111 1111 1001
            //    按位与运算,得到补码：1111 1111 1111 1111 1111 1111 1111 1001
            //                新原码：1000 0000 0000 0000 0000 0000 0000 0111  = -7
            resultValue = smins1 & smins2;
            Console.WriteLine("IntValue:{0}", resultValue);
            // 又因为位运算不影响左右的位，所以可以直接将 int类型 显示转换成 sbyte类型(舍弃高地址，只取低地址)，而不影响结果
            //    即只取低地址的补码： 1111 1001
            //   sbyte有符号的新原码:  1000 0111  = -7
            //  byte无符号的新原码=补码 1111 1001 = 249
            resultSbyte = (sbyte)resultValue;
            byte resByte = (byte)resultValue;
            Console.WriteLine("SbyteValue:{0}", resultSbyte);
            Console.WriteLine("ByteValue:{0}", resByte);
            // 指针验证
            unsafe
            {
                byte* p = (byte*)&resultValue;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Adress: {0}, Value: {1:X}", (int)p, *p);
                    p++;
                }
            }
            Console.WriteLine("-----按位与  正负------");
            // 正负
            // smins1 的int补码形式 ：1111 1111 1111 1111 1111 1111 1111 1011
            // splus2 的int补码形式 ：0000 0000 0000 0000 0000 0000 0000 0111
            //    按位与运算,得到补码：0000 0000 0000 0000 0000 0000 0000 0011
            //                新原码：1000 0000 0000 0000 0000 0000 0000 0011 = 3
            resultValue = smins1 & splus2;
            Console.WriteLine("IntValue:{0}", resultValue);
            // 又因为位运算不影响左右的位，所以可以直接将 int类型 显示转换成 sbyte类型(舍弃高地址，只取低地址)，而不影响结果
            //    即只取低地址的补码： 0000 0011
            //   sbyte有符号的新原码:  0000 0011  = 3
            resultSbyte = (sbyte)resultValue;
            Console.WriteLine("SbyteValue:{0}", resultSbyte);
            // 指针验证
            unsafe
            {
                byte* p = (byte*)&resultValue;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Adress: {0}, Value: {1:X}", (int)p, *p);
                    p++;
                }
            }
            Console.WriteLine("----bool---");
            // bool
            // true 原码 0000 0001， 补码 0000 0001
            // false 原码0000 0000， 补码 0000 0000
            //               按位与，补码 0000 0000
            //                     新原码 0000 0000  = 0 ，转换成bool型 =false
            bool reult = true & false;
            Console.WriteLine("BoolValue:{0}", reult);
            unsafe
            {
                byte* p = (byte*)&reult;
                for (int i = 0; i < sizeof(bool); i++)
                {
                    Console.WriteLine("Adress: {0}, Value: {1:X}", (int)p, *p);
                    p++;
                }
            }
            Console.WriteLine("------char-------");
            // char
            char c1 = 'A';      //原码 0000 0000 0100 0001  int型原码 0000 0000 0000 0000 0000 0000 0100 0001
            char c2 = '严';     //原码 0100 1110 0010 0101  int型原码 0000 0000 0000 0000 0100 1110 0010 0101
            Console.WriteLine((short)c1 + "    " + (short)c2);
            // c1 的int补码形式 ：0000 0000 0000 0000 0000 0000 0100 0001
            // c2 的int补码形式 ：0000 0000 0000 0000 0100 1110 0010 0101
            //    按位与运算,得到补码：0000 0000 0000 0000 0000 0000 0000 0001
            //                新原码：1000 0000 0000 0000 0000 0000 0000 0001 = 1
            resultValue = c1 & c2;
            Console.WriteLine("CharValue:{0}", resultValue);
            // 转换成 char,只取低地址的补码：0000 0000 0000 0001
            //                     新原码： 0000 0000 0000 0001  = 1  转换成char = 不认识的一个符号
            char resultChar = (char)resultValue;
            Console.WriteLine(resultChar);
            unsafe
            {
                byte* p = (byte*)&resultValue;
                for (int i = 0; i < sizeof(char); i++)
                {
                    Console.WriteLine("Adress: {0}, Value: {1:X}", (int)p, *p);
                    p++;
                }
            }
        }
        // 按位或运算 测试了 sbyte
        public static void ComfirmBitOr()
        {
            sbyte splus1 = 5;   // 原码 0000 0101    int形式--> 0000 0000 0000 0000 0000 0000 0000 0101
            sbyte splus2 = 7;   // 原码 0000 0111    int形式--> 0000 0000 0000 0000 0000 0000 0000 0111
            sbyte smins1 = -5;  // 原码 1000 0101    int形式--> 1000 0000 0000 0000 0000 0000 0000 0101
            sbyte smins2 = -7;  // 原码 1000 0111    int形式--> 1000 0000 0000 0000 0000 0000 0000 0111
            Console.WriteLine("-----按位或  正数------");
            // 正数
            // splus1 的int补码形式 ：0000 0000 0000 0000 0000 0000 0000 0101
            // splus2 的int补码形式 ：0000 0000 0000 0000 0000 0000 0000 0111
            //    按位或运算,得到补码：0000 0000 0000 0000 0000 0000 0000 0111  = 7
            int resultValue = splus1 | splus2;
            Console.WriteLine("IntValue:{0}", resultValue);
            // 又因为位运算不影响左右的位，所以可以直接将 int类型 显示转换成 sbyte类型(舍弃高地址，只取低地址)，而不影响结果
            //    即只取低地址的补码： 0000 0111
            //   sbyte有符号的新原码:  0000 0111  = 7
            sbyte resultSbyte = (sbyte)resultValue;
            Console.WriteLine("SbyteValue:{0}", resultSbyte);
            // 指针验证
            unsafe
            {
                byte* p = (byte*)&resultValue;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Adress: {0}, Value: {1:X}", (int)p, *p);
                    p++;
                }
            }
            Console.WriteLine("-----按位或  负数------");
            // 负数
            // smins1 的int补码形式 ：1111 1111 1111 1111 1111 1111 1111 1011
            // smins2 的int补码形式 ：1111 1111 1111 1111 1111 1111 1111 1001
            //    按位或运算,得到补码：1111 1111 1111 1111 1111 1111 1111 1011
            //                新原码：1000 0000 0000 0000 0000 0000 0000 0101  = -5
            resultValue = smins1 | smins2;
            Console.WriteLine("IntValue:{0}", resultValue);
            // 又因为位运算不影响左右的位，所以可以直接将 int类型 显示转换成 sbyte类型(舍弃高地址，只取低地址)，而不影响结果
            //    即只取低地址的补码： 1111 1011
            //   sbyte有符号的新原码:  1000 0101  = -5
            //  byte无符号的新原码=补码 1111 1011 = 251
            resultSbyte = (sbyte)resultValue;
            byte resByte = (byte)resultValue;
            Console.WriteLine("SbyteValue:{0}", resultSbyte);
            Console.WriteLine("ByteValue:{0}", resByte);
            // 指针验证
            unsafe
            {
                byte* p = (byte*)&resultValue;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Adress: {0}, Value: {1:X}", (int)p, *p);
                    p++;
                }
            }
            Console.WriteLine("-----按位或  正负------");
            // 正负
            // smins1 的int补码形式 ：1111 1111 1111 1111 1111 1111 1111 1011
            // splus2 的int补码形式 ：0000 0000 0000 0000 0000 0000 0000 0111
            //    按位或运算,得到补码：1111 1111 1111 1111 1111 1111 1111 1111
            //                新原码：1000 0000 0000 0000 0000 0000 0000 0001 = -1
            resultValue = smins1 | splus2;
            Console.WriteLine("IntValue:{0}", resultValue);
            // 又因为位运算不影响左右的位，所以可以直接将 int类型 显示转换成 sbyte类型(舍弃高地址，只取低地址)，而不影响结果
            //    即只取低地址的补码： 1111 1111
            //   sbyte有符号的新原码:  1000 0001  = -1
            resultSbyte = (sbyte)resultValue;
            Console.WriteLine("SbyteValue:{0}", resultSbyte);
            // 指针验证
            unsafe
            {
                byte* p = (byte*)&resultValue;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Adress: {0}, Value: {1:X}", (int)p, *p);
                    p++;
                }
            }
        }
        // 按位异或运算 测试了 sbyte
        public static void ComfirmBitXor()
        {
            sbyte splus1 = 5;   // 原码 0000 0101    int形式--> 0000 0000 0000 0000 0000 0000 0000 0101
            sbyte splus2 = 7;   // 原码 0000 0111    int形式--> 0000 0000 0000 0000 0000 0000 0000 0111
            sbyte smins1 = -5;  // 原码 1000 0101    int形式--> 1000 0000 0000 0000 0000 0000 0000 0101
            sbyte smins2 = -7;  // 原码 1000 0111    int形式--> 1000 0000 0000 0000 0000 0000 0000 0111
            Console.WriteLine("-----按位异或  正数------");
            // 正数
            // splus1 的int补码形式 ：0000 0000 0000 0000 0000 0000 0000 0101
            // splus2 的int补码形式 ：0000 0000 0000 0000 0000 0000 0000 0111
            //  按位异或运算,得到补码：0000 0000 0000 0000 0000 0000 0000 0010  = 2
            int resultValue = splus1 ^ splus2;
            Console.WriteLine("IntValue:{0}", resultValue);
            // 又因为位运算不影响左右的位，所以可以直接将 int类型 显示转换成 sbyte类型(舍弃高地址，只取低地址)，而不影响结果
            //    即只取低地址的补码： 0000 0010
            //   sbyte有符号的新原码:  0000 0010  = 2
            sbyte resultSbyte = (sbyte)resultValue;
            Console.WriteLine("SbyteValue:{0}", resultSbyte);
            // 指针验证
            unsafe
            {
                byte* p = (byte*)&resultValue;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Adress: {0}, Value: {1:X}", (int)p, *p);
                    p++;
                }
            }
            Console.WriteLine("-----按位异或  负数------");
            // 负数
            // smins1 的int补码形式 ：1111 1111 1111 1111 1111 1111 1111 1011
            // smins2 的int补码形式 ：1111 1111 1111 1111 1111 1111 1111 1001
            //  按位异或运算,得到补码：0000 0000 0000 0000 0000 0000 0000 0010
            //                新原码：0000 0000 0000 0000 0000 0000 0000 0010  = 2
            resultValue = smins1 ^ smins2;
            Console.WriteLine("IntValue:{0}", resultValue);
            // 又因为位运算不影响左右的位，所以可以直接将 int类型 显示转换成 sbyte类型(舍弃高地址，只取低地址)，而不影响结果
            //    即只取低地址的补码： 0000 0010
            //   sbyte有符号的新原码:  0000 0010  = 2
            //  byte无符号的新原码=补码 0000 0010 = 2
            resultSbyte = (sbyte)resultValue;
            byte resByte = (byte)resultValue;
            Console.WriteLine("SbyteValue:{0}", resultSbyte);
            Console.WriteLine("ByteValue:{0}", resByte);
            // 指针验证
            unsafe
            {
                byte* p = (byte*)&resultValue;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Adress: {0}, Value: {1:X}", (int)p, *p);
                    p++;
                }
            }
            Console.WriteLine("-----按位异或  正负------");
            // 正负
            // smins1 的int补码形式 ：1111 1111 1111 1111 1111 1111 1111 1011
            // splus2 的int补码形式 ：0000 0000 0000 0000 0000 0000 0000 0111
            //  按位异或运算,得到补码：1111 1111 1111 1111 1111 1111 1111 1100
            //                新原码：1000 0000 0000 0000 0000 0000 0000 0100 = -4
            resultValue = smins1 ^ splus2;
            Console.WriteLine("IntValue:{0}", resultValue);
            // 又因为位运算不影响左右的位，所以可以直接将 int类型 显示转换成 sbyte类型(舍弃高地址，只取低地址)，而不影响结果
            //    即只取低地址的补码： 1111 1100
            //   sbyte有符号的新原码:  1000 0100  = -4
            resultSbyte = (sbyte)resultValue;
            Console.WriteLine("SbyteValue:{0}", resultSbyte);
            // 指针验证
            unsafe
            {
                byte* p = (byte*)&resultValue;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Adress: {0}, Value: {1:X}", (int)p, *p);
                    p++;
                }
            }
        }
        // 按位异非运算 测试了 sbyte
        public static void ComfirmBitNot()
        {
            sbyte splus1 = 5;   // 原码 0000 0101    int形式--> 0000 0000 0000 0000 0000 0000 0000 0101
            sbyte splus2 = 7;   // 原码 0000 0111    int形式--> 0000 0000 0000 0000 0000 0000 0000 0111
            sbyte smins1 = -5;  // 原码 1000 0101    int形式--> 1000 0000 0000 0000 0000 0000 0000 0101
            sbyte smins2 = -7;  // 原码 1000 0111    int形式--> 1000 0000 0000 0000 0000 0000 0000 0111


            // splus1 的int补码形式：0000 0000 0000 0000 0000 0000 0000 0101
            //      按位取反后的补码：1111 1111 1111 1111 1111 1111 1111 1010
            //               新原码：1000 0000 0000 0000 0000 0000 0000 0110  = - 6
            int resultValue = ~splus1;
            Console.WriteLine("IntValue:{0}", resultValue);
            // 又因为位运算不影响左右的位，所以可以直接将 int类型 显示转换成 sbyte类型(舍弃高地址，只取低地址)，而不影响结果
            //    即只取低地址的补码： 1111 1010
            //   sbyte有符号的新原码:  1000 0110  = -6
            sbyte resultSbyte = (sbyte)resultValue;
            Console.WriteLine("SbyteValue:{0}", resultSbyte);
            // 指针验证
            unsafe
            {
                byte* p = (byte*)&resultValue;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Adress: {0}, Value: {1:X}", (int)p, *p);
                    p++;
                }
            }

            // smins2 的int补码形式 ：1111 1111 1111 1111 1111 1111 1111 1001
            //       按位取反后的补码：0000 0000 0000 0000 0000 0000 0000 0110
            //                新原码：0000 0000 0000 0000 0000 0000 0000 0110  = 6
            resultValue = ~smins2;
            Console.WriteLine("IntValue:{0}", resultValue);
            // 又因为位运算不影响左右的位，所以可以直接将 int类型 显示转换成 sbyte类型(舍弃高地址，只取低地址)，而不影响结果
            //    即只取低地址的补码： 0000 0110
            //   sbyte有符号的新原码:  0000 0110  = 6
            resultSbyte = (sbyte)resultValue;
            Console.WriteLine("SbyteValue:{0}", resultSbyte);
            // 指针验证
            unsafe
            {
                byte* p = (byte*)&resultValue;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Adress: {0}, Value: {1:X}", (int)p, *p);
                    p++;
                }
            }
        }

        // 按位与 -----> Mask掩码
        // 总结：掩盖的位为0，想暴露的位为1,创建Mask掩码，再用 & 运算
        public static void BitMask()
        {
            // 无符号byte，& 运算会先转换成int
            byte flags = 150;// int型原码 0000 0000 0000 0000 0000 0000 1001 0110  补码 0000 0000 0000 0000 0000 0000 1001 0110
            byte mask = 2;   // int型原码 0000 0000 0000 0000 0000 0000 0000 0010  补码 0000 0000 0000 0000 0000 0000 0000 0010
            flags = (byte) (flags & mask);   //                                    补码 0000 0000 0000 0000 0000 0000 0000 0010 ---->低地址补码 0000 0010 = 原码 = 2
            // 当然，光是计算出2是没有什么意义的
            // Mask掩码最大的用处是： 将其余位置掩盖(0&x),暴露出想暴露的位(1&x)的原值
            // 这里就只暴露出了 第二个位为1 ，其余的位的值全部被掩盖了
            Console.WriteLine("Result： {0}", flags);
            // sbyte
            sbyte flags2 = -98;// 原码 1000 0000 0000 0000 0000 0000 0110 0010  补码 1111 1111 1111 1111 1111 1111 1001 1110
            sbyte mask2 = 2;   // 原码 0000 0000 0000 0000 0000 0000 0000 0010  补码 0000 0000 0000 0000 0000 0000 0000 0010
            flags2 = (sbyte)(flags2 & mask2);//                                 补码 0000 0000 0000 0000 0000 0000 0000 0010 ----> 低地址补码 0000 0010 = 原码 = 2
            // 同样的掩盖了其余位置，只暴露了第二个位为1
            Console.WriteLine("Result： {0}", flags2);

            // int
            int flags3 = 10000;//         原码 0000 0000 0000 0000 0010 0111 0001 0000  = 补码
            byte mask3 = 2;// 先转换成int型原码 0000 0000 0000 0000 0000 0000 0000 0010  = 补码
            flags3 = flags3 & mask3;//    补码 0000 0000 0000 0000 0000 0000 0000 0000  = 原码
            // 同样的掩盖了其余位置，只暴露了第二个位为0
            Console.WriteLine("Result： {0}", flags3);
        }
        // 按位与 -----> 检查位
        public static void BitCheck()
        {
            byte flags = 150;// int型原码 0000 0000 0000 0000 0000 0000 1001 0110  补码 0000 0000 0000 0000 0000 0000 1001 0110
            byte mask = 2;   // int型原码 0000 0000 0000 0000 0000 0000 0000 0010  补码 0000 0000 0000 0000 0000 0000 0000 0010
            // (flags & mask) 暴露出flags的第二位的值，然后判断第二位的值是否为1
            // 因为其余位都被掩盖为0,所以一种简单的比较方法是：比较的数其余位也为 0，比较位设为1
            // 这样只需要 == 逐位比较，如果相等，说明比较位为1
            // 刚好就还能使用 mask ,true为1 false为0
            bool result = (flags & mask) == mask;
            Console.WriteLine(result);
        }

        // 按位或 -----> 打开位
        // 在不影响其他位的情况下，打开指定的位
        public static void BitOpen2One()
        {
            // 无符号byte，& 运算会先转换成int
            byte flags = 148;// int型原码 0000 0000 0000 0000 0000 0000 1001 0100  补码 0000 0000 0000 0000 0000 0000 1001 0100
            byte mask = 2;   // int型原码 0000 0000 0000 0000 0000 0000 0000 0010  补码 0000 0000 0000 0000 0000 0000 0000 0010
            flags = (byte)(flags | mask);   //                                    补码 0000 0000 0000 0000 0000 0000 1001 0110 ---->低地址补码 1001 0110 = 原码 = 150
            // 当然，光是计算出150是没有什么意义的
            // Mask打开位的用处是： 其余位保持不变，mask为1的对应的flags位被设置为1
            // 相当于开关一样
            Console.WriteLine("Result： {0}", flags);

            sbyte flags2 = 120;// int型原码 0000 0000 0000 0000 0000 0000 0111 1000  补码 0000 0000 0000 0000 0000 0000 0111 1000
            byte mask2 = 128;  // int型原码 0000 0000 0000 0000 0000 0000 1000 0000  补码 0000 0000 0000 0000 0000 0000 1000 0000
            flags2 = (sbyte) (flags2 | mask2); //                                    补码 0000 0000 0000 0000 0000 0000 1111 1000 ---->低地址补码 1111 1000 --原码 1000 1000 = -8
            // 再次证明了 打开位 的作用
            Console.WriteLine("Result： {0}", flags2);
        }
        // 按位与非 -----> 关闭位
        // 在不影响其他位的情况下，关闭指定的位
        public static void BitClose2Zero()
        {
            // 无符号byte，& 运算会先转换成int
            byte flags = 148;// int型原码 0000 0000 0000 0000 0000 0000 1001 0100  补码 0000 0000 0000 0000 0000 0000 1001 0100
            byte mask = 4;   // int型原码 0000 0000 0000 0000 0000 0000 0000 0100  补码 0000 0000 0000 0000 0000 0000 0000 0100
            flags = (byte)(flags & ~mask);                              // ~mask 的补码 1111 1111 1111 1111 1111 1111 1111 1011
                                                                        // &操作之后补码 0000 0000 0000 0000 0000 0000 1001 0000 ---->低地址补码 1001 0000 = 原码 = 144
            // 当然，光是计算出144是没有什么意义的
            // ~mask关闭位的用处是： 其余位保持不变，~mask为0的对应的flags位被设置为0
            // 然后，根据需要关闭的位，来设置 mask 的值
            // 相当于开关一样
            Console.WriteLine("Result： {0}", flags);
        }

        // 按位异或 -----> 切换位
        // 打开已关闭的位，关闭已打开的位
        public static void BitSwitch()
        {
            // 无符号byte，& 运算会先转换成int
            byte flags = 148;// int型原码 0000 0000 0000 0000 0000 0000 1001 0100  补码 0000 0000 0000 0000 0000 0000 1001 0100
            byte mask = 4;   // int型原码 0000 0000 0000 0000 0000 0000 0000 0100  补码 0000 0000 0000 0000 0000 0000 0000 0100
            flags = (byte)(flags ^ mask);//                               ^操作后的补码 0000 0000 0000 0000 0000 0000 1001 0000 ----> 低地址补码 1001 1011 = 无符号原码 = 144
            // 当然，光是计算出144是没有什么意义的
            // mask切换位的用处是： 其余位保持不变，mask为1的对应的flags位 1被设置为0，0被设置成1
            // 相当于开关一样
            Console.WriteLine("Result： {0}", flags);
            // 多切换几次，看效果
            flags = (byte)(flags ^ mask);
            Console.WriteLine("Result： {0}", flags);
            flags = (byte)(flags ^ mask);
            Console.WriteLine("Result： {0}", flags);
            flags = (byte)(flags ^ mask);
            Console.WriteLine("Result： {0}", flags);
        }
    }
}
