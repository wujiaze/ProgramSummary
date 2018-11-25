
using System;

namespace Operator_01Simple
{
    class Program
    {
        static void Main(string[] args)
        {
            // 一些简单零碎的运算符的记录
            // 整除
            int value = -5 / 3; // 现实中为 -1.66667 ，这里是整除 -1
            Console.WriteLine(value);
            // 数值比较方式 ：比较值的大小
            int intnum = 1;
            byte byteNum = 1;
            Console.WriteLine(intnum == byteNum); // 类型虽然不同，但是值相同，所以相同

            // 自己重载的运算符
            Program p1 = new Program();
            int result = p1 + 5;
            Console.WriteLine(result);
            // 自己重载的运算符
            Console.WriteLine(new MyClass() - new Program());
            Console.Read();
        }

        // 重载 + 运算符 
        // 返回值：int 类型
        // 参数：也是实际使用时的操作数:二元运算符，其中一个参数必须是自身的类或结构
        //                           一元运算符，参数必须是自身的类或结构
        public static int operator +(Program p, int x)
        {
            return x + 1;
        }
    }

    class MyClass
    {
        private int value = new Program() + 8;
        public static int operator -(MyClass p, Program x)
        {
            return 5;
        }
    }
}
