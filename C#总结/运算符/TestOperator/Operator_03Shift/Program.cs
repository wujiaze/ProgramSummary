/*
 *      运算符
 *      
 *      移位运算符,C#只提供了单向移位,是一种逻辑运算
 *          适用范围：byte sbyte short ushort char int uint long ulong
 *          采用补码形式
 *      一、<< 左移运算符    
 *          定义：向左进行逻辑位移，右侧空出位用0填充
 *          算术溢出：当左移舍弃的位上是1，说明溢出原来的类型取值范围
 *              int型:适用类型 byte sbyte short ushort char int uint
 *                    将数字转换成int类型，在转换成补码形式，最后向左位移
 *              long型：适用类型 long ulong
 *                      将数字转换成long类型，在转换成补码形式，最后向左位移
 *      二、>> 右移运算符
 *          无符号数
 *          定义：向右进行逻辑位移，右侧空出位用0填充
 *               不会溢出,最小为0，不会为负
 *               int型:适用类型 byte sbyte short ushort char int uint
 *                     将数字转换成int类型，在转换成补码形式，最后向右位移
 *              long型：适用类型 long ulong
 *                      将数字转换成long类型，在转换成补码形式，最后向右位移
 *          有符号
 *          正数：跟无符号数一致
 *          
 *          负数
 *          定义：向右进行逻辑位移，右侧空出位用1填充(c#,不同系统可能有0来填充)
 *               不会溢出,最小为-1，不会为0或正数
 *               int型:适用类型 byte sbyte short ushort char int uint
 *                     将数字转换成int类型，在转换成补码形式，最后向右位移
 *              long型：适用类型 long ulong
 *                      将数字转换成long类型，在转换成补码形式，最后向右位移
 */

using System;

namespace Operator_03Shift
{
    class Program
    {
        static void Main(string[] args)
        {
            //ComfirmShiftLeft();
            //ComfirmShiftRight();
            //OptimMul();
            BitExtract();
            Console.Read();
        }

        // <<  左移运算符
        private static void ComfirmShiftLeft()
        {
            Console.WriteLine("------左移 正数------");
            // 正数
            sbyte splus1 = 127;//原码 0111 1111   int形式--> 0000 0000 0000 0000 0000 0000 0111 1111
            int resultInt = splus1 << 1;//              补码 0000 0000 0000 0000 0000 0000 0111 1111
            //   << 1 之后的补码 0000 0000 0000 0000 0000 0000 1111 1110 = 原码 = 254
            Console.WriteLine("原值 {0}。溢出后，更换类型后的值：{1}", splus1, resultInt);
            // 如果只取低地址  补码：1111 1110  原码：1000 0010 = -2
            // 但是实际值已经溢出 sbyte 的范围，所以没有什么实际意义，还是采用上面的 int 值
            sbyte resultSbyte = (sbyte)resultInt;
            Console.WriteLine("原值 {0}。溢出后，不更换类型后的值：{1}", splus1, resultSbyte);
            unsafe
            {
                byte* p = (byte*)&resultInt;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Adress: {0}, Value: {1:X}", (int)p, *p);
                    p++;
                }
            }
            Console.WriteLine("------左移 负数------");
            // 负数
            sbyte smins1 = -1; //原码 1000 0001   int形式--> 1000 0000 0000 0000 0000 0000 0000 0001
            resultInt = smins1 << 1;//                  补码 1111 1111 1111 1111 1111 1111 1111 1111
            //   << 1 之后的补码 1111 1111 1111 1111 1111 1111 1111 1110 
            //            新原码 1000 0000 0000 0000 0000 0000 0000 0010 = -2
            Console.WriteLine("原值 {0}。没有溢出,更换类型后的值：{1}", smins1, resultInt);
            // 如果只取低地址  补码：1111 1110  原码：1000 0010 = -2
            // 但是实际值没有溢出 sbyte 的范围，可以采用 sbyte 类型
            resultSbyte = (sbyte)resultInt;
            Console.WriteLine("原值 {0}。没有溢出,不更换类型后的值：{1}", smins1, resultSbyte);
            // 指针验证
            unsafe
            {
                byte* p = (byte*)&resultInt;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Adress: {0}, Value: {1:X}", (int)p, *p);
                    p++;
                }
            }
            Console.WriteLine("------左移 溢出---总结：当左移舍弃的位上是1，就说明溢出原来的类型了---");
            int intplusMax = int.MaxValue / 2;//   1073741823  原码 0011 1111 1111 1111 1111 1111 1111 1111 = 补码
            resultInt = intplusMax << 1;                // <<1 补码 0111 1111 1111 1111 1111 1111 1111 1110 = 原码 = 2147483646
            Console.WriteLine("原值 {0}。没有溢出,不更换类型后的值：{1}", intplusMax, resultInt);
            // 指针验证
            unsafe
            {
                byte* p = (byte*)&resultInt;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Adress: {0}, Value: {1:X}", (int)p, *p);
                    p++;
                }
            }
            intplusMax += 1;                     // 补码 0100 0000 0000 0000 0000 0000 0000 0000
            resultInt = intplusMax << 1;//     <<1 ,补码 1000 0000 0000 0000 0000 0000 0000 0000 --- 没有原码 = -2147483648
            // 但是在算术上，已经溢出，没有意义了，一个整数左移变成负数
            Console.WriteLine("原值 {0}。溢出,不更换类型后的值：{1}", intplusMax, resultInt);
            // 指针验证
            unsafe
            {
                byte* p = (byte*)&resultInt;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Adress: {0}, Value: {1:X}", (int)p, *p);
                    p++;
                }
            }
            // 总结：当左移舍弃的位上是1，就说明溢出原来的类型了
        }

        // >>  右移运算符
        private static void ComfirmShiftRight()
        {
            Console.WriteLine("------右移  无符号-----------");
            // 无符号
            byte byte1 = 255; // 原码 1111 1111 -->int形式  0000 0000 0000 0000 0000 0000 1111 1111 = 原码
            int resultInt = byte1 >> 1;       // >>1后补码  0000 0000 0000 0000 0000 0000 0111 1111 = 新原码 =127
            Console.WriteLine("原值 {0}。未溢出更换类型后的值：{1}", byte1, resultInt);
            // 指针验证
            unsafe
            {
                byte* p = (byte*)&resultInt;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Adress: {0}, Value: {1:X}", (int)p, *p);
                    p++;
                }
            }
            byte resultByte = (byte)resultInt; // 低地址补码 0111 1111 = 新原码 = 127
            Console.WriteLine("原值 {0}。未溢出不更换类型后的值：{1}", byte1, resultByte);
            // 指针验证
            unsafe
            {
                byte* p = (byte*)&resultByte;
                for (int i = 0; i < sizeof(byte); i++)
                {
                    Console.WriteLine("Adress: {0}, Value: {1:X}", (int)p, *p);
                    p++;
                }
            }
            resultInt = byte1 >> 10;
            Console.WriteLine("原值 {0}。右移10位更换类型后的值：{1}", byte1, resultInt);
            // 指针验证
            unsafe
            {
                byte* p = (byte*)&resultInt;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Adress: {0}, Value: {1:X}", (int)p, *p);
                    p++;
                }
            }
            Console.WriteLine("------右移  有符号  正数-----------");
            // 有符号
            // 正数，跟无符号数一样
            sbyte splus = 127;// 原码 0111 1111 -->int形式  0000 0000 0000 0000 0000 0000 0111 1111 = 原码
            resultInt = splus >> 1;           // >>1后补码  0000 0000 0000 0000 0000 0000 0011 1111 = 新原码 = 63
            Console.WriteLine("原值 {0}。未溢出更换类型后的值：{1}", splus, resultInt);
            // 指针验证
            unsafe
            {
                byte* p = (byte*)&resultInt;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Adress: {0}, Value: {1:X}", (int)p, *p);
                    p++;
                }
            }
            Console.WriteLine("------右移  有符号  负数-----------");
            // 负数
            sbyte smins = -127;// 原码 1111 1111 -->int形式  1000 0000 0000 0000 0000 0000 0111 1111 
                               //                      补码  1111 1111 1111 1111 1111 1111 1000 0001
            resultInt = smins >> 1;           // >>1 新补码  1111 1111 1111 1111 1111 1111 1100 0000
                                              //     新原码  1000 0000 0000 0000 0000 0000 0100 0000 = -64
            Console.WriteLine("原值 {0}。未溢出更换类型后的值：{1}", smins, resultInt);
            // 指针验证
            unsafe
            {
                byte* p = (byte*)&resultInt;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Adress: {0}, Value: {1:X}", (int)p, *p);
                    p++;
                }
            }
            sbyte sbyteResult = (sbyte)resultInt; // 只取低地址的新补码  1100 0000 --> 原码 1100 0000 = -64
            Console.WriteLine("原值 {0}。未溢出不更换类型后的值：{1}", smins, sbyteResult);
            // 指针验证
            unsafe
            {
                byte* p = (byte*)&sbyteResult;
                for (int i = 0; i < sizeof(byte); i++)
                {
                    Console.WriteLine("Adress: {0}, Value: {1:X}", (int)p, *p);
                    p++;
                }
            }
            resultInt = smins >> 10;           // >>10 新补码  1111 1111 1111 1111 1111 1111 1111 1111
                                               //     新原码  1000 0000 0000 0000 0000 0000 0000 0001 = -1
            Console.WriteLine("原值 {0}。未溢出更换类型后的值：{1}", smins, resultInt);
            // 指针验证
            unsafe
            {
                byte* p = (byte*)&resultInt;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Adress: {0}, Value: {1:X}", (int)p, *p);
                    p++;
                }
            }
        }


        /// <summary>
        /// 乘法/除法优化
        ///    10进制会溢出 ，二进制同样会溢出，二进制效率高
        /// </summary>
        private static void OptimMul()
        {
            // 二进制左移 = *2 ，10进制的左移 = *10
            Console.WriteLine("乘法");
            int a = 100;
            int b = a * 2;
            int c = a << 1;
            Console.WriteLine("乘法的结果{0},左移的结果{1}", b, c);
            // 二进制右移 = /2 ，10进制的右移 = /10 ，并且都是整除
            b = a / 2;
            c = a >> 1;
            Console.WriteLine("除法的结果{0},右移的结果{1}", b, c);

        }

        /// <summary>
        /// 移位运算符应用 ---> 位提取
        /// </summary>
        private static void BitExtract()
        {
            int color = 0x102A162F; // 假如有这么一个数存储一个颜色 Red：2F  Green：16  Blue：2A Alpha:10
            byte red = (byte)color;
            byte green = (byte)(color >> 8);
            byte blue = (byte) (color >> 16);
            byte alpha = (byte) (color >> 24);
            Console.WriteLine(" Red：{0:X}  Green：{1:X}  Blue：{2:X} Alpha:{3:X}", red, green, blue, alpha);
        }
    }
}
