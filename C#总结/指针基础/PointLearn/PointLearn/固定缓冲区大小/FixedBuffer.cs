/*
 *     固定缓冲区：一般用于和不同语言、平台的数据源进行互操作时
 *
 *         位置：只存在于结构体中
 *         关键字: fixed ,这里和指针的意义不同，指的是固定内存大小，不是地址
 *                        比如 fixed int[30] ,那么就得到了 4*30 的内存地址
 *
 *         声明：
 *          修饰符 fixed type 变量[数量]
 *
 *          type：
 *              整型：byte、sbyte、short、ushort、int、uint、long、ulong
 *            布尔型：bool
 *            字符型：char
 *            浮点型：float、double
 */

using System;

namespace PointLearn.固定缓冲区大小
{
    public unsafe struct Mystruct
    {
        public fixed int Arr[10];
    }
    class FixedBuffer
    {
        static Mystruct ms = new Mystruct();

        public static unsafe void Test()
        {
            fixed (int* p = ms.Arr)
            {
                p[1] = 12;
            }

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(ms.Arr[i]); // c# 7.3以上支持
            }
        }
    }
}
