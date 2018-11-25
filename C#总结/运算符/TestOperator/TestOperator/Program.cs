/*
 *      运算符
 *      
 *      移位运算符,C#只提供了单向移位,是一种逻辑运算
 *          适用范围：byte short char int long
 *      1、左移运算符  <<  
 *          1、byte short char int 计算时转换成 int类型计算，自身不变
 *        
 */

using System;

namespace TestOperator
{
    class Program
    {
        static void Main(string[] args)
        {
            // 正数左移
            char bo = '1';
            long ii = 109;

            byte bplusNor = 64; // 原码 0100 0000
            int bint = bplusNor << 1;// 先转换成 0000 0000 0000 0000 0000 0000 0100 0000 ---> 0000 0000 0000 0000 0000 0000 1000 0000
            unsafe
            {
                int* p = &bint;
                byte* pb = (byte*) p;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Address {0} ,Value {1:X}",(int)pb,*pb);
                    pb++;
                }
            }
            byte bplusMax = 127;// 原码 0111 1111  
            int bintMax = bplusMax << 32;// 先转换成 0000 0000 0000 0000 0000 0000 0111 1111 ---> 0000 0000 0000 0000 0000 0000 1111 1110
            unsafe
            {
                int* p = &bintMax;
                byte* pb = (byte*)p;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Address {0} ,Value {1:X}", (int)pb, *pb);
                    pb++;
                }
            }
            Console.WriteLine("--------");
            sbyte bMinsMax = -127; // 原码 1111 1111  补码 1000 0001
            int bintMins = bMinsMax << 1;// 先转换成int原码 1000 0000 0000 0000 0000 0000 0111 1111 
                                         //           补码 1111 1111 1111 1111 1111 1111 1000 0001
                                         //    左移后的补码 1111 1111 1111 1111 1111 1111 0000 0010
                                         //           源码 1000 0000 0000 0000 0000 0000 1111 1110 = -254
            Console.WriteLine(bintMins);
            Console.WriteLine((byte)bintMins);
            unsafe
            {
                int* p = &bintMins;
                byte* pb = (byte*)p;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Address {0} ,Value {1:X}", (int)pb, *pb);
                    pb++;
                }
            }

            Console.WriteLine("------------");
            int intMinsMax = -int.MaxValue; //        原码 1111 1111 1111 1111 1111 1111 1111 1111
                                            //        补码 1000 0000 0000 0000 0000 0000 0000 0001
                                            // 左移后的补码 0000 0000 0000 0000 0000 0000 0000 0010
                                            //        源码 0000 0000 0000 0000 0000 0000 0000 0010 = 2
            int temp = intMinsMax << 1; // 照理说溢出了，确实溢出了，没有循环，所以，只有在没有溢出的情况下，逻辑左移看上去和算术左移一致
            Console.WriteLine(temp);
            unsafe
            {
                int* p = &temp;
                byte* pb = (byte*)p;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Address {0} ,Value {1:X}", (int)pb, *pb);
                    pb++;
                }
            }

            int intMinmax2 = -int.MaxValue;
            long temp2 = (long) intMinmax2 << 1;
            Console.WriteLine(temp2);

            // 右移：还要考虑一个系统？

            Console.WriteLine("111");
            int x = 9;
            int y = 8 + x++;
            Console.WriteLine(y);

            short s1 = 0x12F4;
            byte b1 = (byte) s1;
            Console.WriteLine("{0:X}",b1); // 获取了低地址的8位，舍弃了高地址
            Console.WriteLine("sss  " + (64 > 9));
            Console.Read();
            
        }
    }
}
