/*
 *      C# 指针基础      参考资料   https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/unsafe-code-pointers/pointer-types
 *                      使用指针需要将项目的 属性 -- 生成 -- 勾选 " 允许不安全代码 " ,并需要在关键字unsafe块中  unsafe{  } 或 方法中
 *      一、定义：
 *              指针是一种类型，与 值类型、引用类型 平齐，可以叫做 point-type
                指针类型是4个字节的长度，无符号整数

 *      二、可作为指针的类型(也是指针可以指向的类型)
 *              整形：     sbyte、byte、short、ushort、int、uint、long、ulong
 *              浮点型：   float、double
 *              字符：     char
 *              布尔：     bool
 *              小数类型： decimal  (补充说明：比double更精确，多用于财务计算)
 *              枚举类型： 任何枚举(包括自定义)
 *              指针类型： 比如可以出现  int**  (int* 表示一个指针,int** 表示指针的指针) 
 *              结构类型： 必须只含有非托管类型
 *            建议：什么类型的数据就采用对应类型的指针比较好，便于地址计算和数据获取
 *
 *      三、特性
 *              1、指针类型 不继承 Object ，所以和Object之间不存在转换
 *              2、指针不支持 装箱和拆箱
 *              3、指针不能指向 引用类型 或 包含引用类型的结构体 (因为引用类型会被垃圾回收器回收)
 *
 *      四、指针的声明写法
 *              int*    p               指向 整数类型 的指针
 *              int**   p               指向 整数指针 的指针
 *              int*[]  p               一组 指向整数 的指针数组
 *              int*    p1,p2,p3        多个 指向整数类型 的指针
 *              void*   p               指向 未知类型 的指针  类似 var
 *
 *      五、指针的转换
 *               1、隐式转换:
 *                         1.1、任何指针类型 转换为  void*
 *                         1.2、null        转换为  任何指针类型，也就是说指针类型可以为null
 *              2、显示转换:
 *                         2.1、任何指针类型 转换为 其他任何指针类型
 *                         2.2、整型(sbyte、byte、short、ushort、int、uint、long、ulong)   <----->   任何指针类型
 *
 *      六、指针的运算
 *            fixed         临时固定堆上的内存地址，防止被GC回收或改变，栈上的内存就不需要使用
 *
 *              &           获取变量的地址
 *
 *              *           跟定义时的 * 不同，这里的 * 表示一种行为：执行指针的间接寻址功能。不能用于void*
 *                              举例：一个指针的值(即某个变量的地址)，然后通过这个值，找到那个值(内存地址)中存储的真正数据，并且返回
 *
 *    ++  --  +  -  +=  -=  指针运算,根据不同的指针类型,计算地址。不能用于void*
 *                              举例：一个指针p的值为1000, p的类型为int*，那么 p++ 运算之后，p的值为1004 ,增加了一个int的字节数，4个字节
 *                              原理：内存地址在一个int数字的头部，并且一个内存地址存储1个字节的数据，指针运算就是寻找下一个地址
 *
 *              ->          通过指针访问结构体的成员；  p->x 等效于 (*p).x
 *
 *     ==  !=  >  <  >=  <= 比较指针的大小。
 *
 *          stackalloc      在栈上分配内存
 *
 *              []          指针的索引  p[x] 等效于 *(p+x)
 *          
 *      七、指针的意义
 *              type* p = &variable      p的值表示 variable 的内存地址
 *              int address = (int)p     来获取指针的值，也就是对应的变量内存地址
 *      
 *      八、指针的测试
 *          fixed 关键字：临时固定变量的内存地址，这样每次运行都得到同一个地址，不然多次运行时，可能得到的内存地址不同，干扰判断
 *          unsafe 中，尽量不要分配堆上的内存，因为这样会使GC对这个堆失去控制，若自己有没有删除，那么就会始终占用那个内存空间
 *      九、注意
 *          List、Dictionary、Queue、Stack 都不是使用指针
 *      十、补充
 *          为什么一般用16进制，在内存中表示数据
 *          因为 一个内存 存储 一个字节 ，一个字节的范围 0~255，刚好是 16进制 0~FF,这样用16进制来表示，整洁工整
 */

using System;
using PointLearn.固定缓冲区大小;
using PointLearn.小端大端内存理解;

namespace PointLearn
{
    class Program
    {
        static void Main(string[] args)
        {
            byte xx = 10;
            byte yy = xx;
            unsafe
            {
                byte* p1 = &xx;
                byte* p2 = &yy;
                Console.WriteLine((int)p1 + "  " + (int)p2);
            }
            xx = 20;
            unsafe
            {
                byte* p1 = &xx;
                Console.WriteLine((int)p1);
            }
            
            Console.WriteLine("--------指针转换-------");
            PointConvert();
            Console.WriteLine("--------指针寻址的值--------");
            PointValue();
            Console.WriteLine("---------指针的运算1----------");
            PointCalculate();
            Console.WriteLine("------指针的运算2------");
            PointCalculate2();
            Console.WriteLine("-----指针的运算3------");
            PointCalculate3();
            Console.WriteLine("------指针运算4------");
            PointCalculate4();
            Console.WriteLine("-----指针运算5-----");
            PointCalculate5();
            Console.WriteLine("-------指针运算6--------");
            PointCalculate6();
            Console.WriteLine("----大小端问题----");
            PointandMemory.MemoryOrderInt();
            Console.WriteLine("------大小端问题-------");
            PointandMemory.MemoryOrderIntArr();
            Console.WriteLine("-------固定缓冲区大小-------");
            FixedBuffer.Test();
            Console.Read();
        }

        // 指针的转换
        public static void PointConvert()
        {
            int temp;   // 虽然没有赋值，但是已有内存地址
            unsafe      // 指针需要再 unsafe 块中
            {
                void* o = &temp;        // 隐式转换 int*  -->  void*
                int* p = null;          // 隐式转换 null  -->  int*
                byte* b1 = (byte*)o;    // 显示转换 void* -->  byte*
                int value = (int)b1;    // 显示转换 byte* -->  int

                temp = 10000;  // 0x00002710
                int* pInt = &temp;
                byte* pByte = (byte*)&temp;
                Console.WriteLine(*pInt);
                Console.WriteLine("{0:X}", *pByte);  // pInt 和 pByte 对应的地址是同一个，但是类型不同，所以 pInt 对应4个地址合成的数据，pByte 对应1个地址的数据，再根据小端系统，那么就是 10(16进制)


            }
        }

        // 通过指针获取 变量的值 和 变量的地址
        public static void PointValue()
        {
            unsafe
            {
                // 通过指针 获取变量的值
                char theChar = 'Z';
                char* pChar = &theChar;
                void* pVoid = pChar;                                // void* 不可使用 间接寻址运算符 *pVoid
                int* pInt = (int*)pVoid;
                Console.WriteLine("Value of pChar = {0}", *pChar);  // 通过 char型指针获取的数据类型是 char 为 Z
                Console.WriteLine("Value of pInt = {0}", *pInt);    // 通过 int 型指针获取的数据类型是 int  为 90 , (int)Z =90

                /* 利用指针的间接寻址功能，对保存的数据进行处理*/
                *pInt += 1;
                Console.WriteLine("new Value    " + *pInt);         // 91
                Console.WriteLine("new value    " + (int)theChar);  // 91

                // 地址: 不同的指针表示，但是都指向同一个地址
                Console.WriteLine("内存地址:  " + (int)pChar);       // 获取指针自身的值，也是目标地址
                Console.WriteLine("内存地址:  " + (int)pVoid);
                Console.WriteLine("内存地址:  " + (int)pInt);
            }
        }

        // 指针运算  fixed  &  *

        public static void PointCalculate()
        {
            int[] arr = { 11, 25, 37, 41, 52 };
            unsafe
            {
                // fixed    临时固定 arr[0]变量 以便测试时固定地址
                // &        获取变量arr[0]的地址
                fixed (int* p = &arr[0])
                {
                    int* p2 = p;                // 指针可以赋值，将 p 的值(一个内存地址)赋给 另一个非固定的整型指针
                    int value = *p2;            // *    一种行为：执行指针的间接寻址功能，得到地址保存的数据
                    Console.WriteLine(value);
                }

            }
        }

        // 指针运算 ++  -- 
        public static void PointCalculate2()
        {
            int[] array = { 10, 20, 30, 40, 50 };
            unsafe
            {
                fixed (int* p = array)  // 引用类型的引用就是地址，所以可以直接赋给指针
                {
                    // 从这里可以知道 数组的引用 就是数组第一个元素的地址 ，那么对应的值就自然是第一个元素的数据
                    Console.WriteLine("Array Value:{0} @ Array Address:{1}", *p, (int)p);
                    // 每一次使用 ++ ，则地址增加 sizeof(type) ，这里是 sizeof(int) = 4
                    for (int* p2 = p; p2 < p + array.Length; p2++)
                    {
                        Console.WriteLine("Value:{0} @ Address:{1}", *p2, (int)p2);
                    }
                }
            }
        }

        // 指针运算 stackalloc []
        public static void PointCalculate3()
        {
            unsafe
            {
                // new 运算符在堆上分配内存
                // stackalloc 运算符的作用：在栈上分配内存，所以不会被垃圾回收不需要使用 fixed
                // block 的生命周期，在 unsafe 块中
                int* block = stackalloc int[10];
                for (int i = 0; i < 10; i++)
                {
                    int* address = &block[i];
                    Console.WriteLine((int)address);    // 获取地址
                    Console.WriteLine(*address);        // 获取地址保存的数值 与索引一致
                    Console.WriteLine(block[i]);        // [] 索引表示对应地址保存的数值
                }
            }
        }

        // 指针运算 + - += -=
        public static void PointCalculate4()
        {
            unsafe
            {
                int* array = stackalloc int[10];

                /* 指针相减：相同类型成立 */
                int* p1 = &array[4];
                int* p2 = &array[10];
                long differ = p2 - p1;      // 表示相差6个地址，计算公式((long)p1 - (long)p2) / sizeof(int)：两个地址相减/sizeof(type),就得出相差的地址数
                Console.WriteLine("different    " + differ);
                /* 指针与整数的计算*/
                int* p3 = &array[11];
                p3 = p3 - 1;                // 指针 - 整数 还是指针 计算公式 long(p3) - 1*sizeof(int) 所得到值对应的指针
                Console.WriteLine("是否等于0    " + ((int)p3 - (int)p2));
            }

        }

        // 指针运算 == != > < >= <=
        public static void PointCalculate5()
        {
            unsafe
            {
                int x = 1;
                int y = 2;
                int* px = &x;
                int* py = &y;
                Console.WriteLine((int)px);
                Console.WriteLine((int)py);
                Console.WriteLine(px > py); // 比较的是两个地址的大小
            }
        }

        // 指针运算 ->
        public static void PointCalculate6()
        {
            Mystruct s1 = new Mystruct() { x = 1 };
            unsafe
            {
                Mystruct* p = &s1;
                p->x = 10;
                Console.WriteLine("通过指针获取值 " + p->x);
            }

            Console.WriteLine("测试值 " + s1.x);
        }

        /* 一个指针的应用 */
        public static unsafe void CopyArr(int[] source, int sourceOffest, int[] target, int targetOffest, int count)
        {
            if (source == null || target == null)
                throw new System.ArgumentException();
            if (sourceOffest < 0 || targetOffest < 0 || count < 0)
                throw new System.ArgumentException();
            if (source.Length - sourceOffest < count || target.Length - targetOffest < count)
                throw new System.ArgumentException();

            fixed (int* pSource = source, pTarget = target)
            {
                for (int i = 0; i < count; i++)
                {
                    pTarget[i + targetOffest] = pSource[i + sourceOffest];
                }
            }
        }
    }

    // 用于指针的结构体，内部不能有引用类型
    public struct Mystruct
    {
        public int x;
    }
}
