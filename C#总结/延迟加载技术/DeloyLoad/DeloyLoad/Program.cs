using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeloyLoad
{
    class Program
    {
        // 延迟加载 ： 针对的是 静态成员
        static void Main(string[] args)
        {
            // 当类的内部没有静态构造函数时，静态成员的赋值，是Net内部的优化方法在静态成员被引用之前完成赋值
            // 但是，这个顺序，我们自己无法把握
            Console.WriteLine("Main");
            string str = TestClass.Str;
            Console.WriteLine(str);

            // 在类的内部添加静态构造函数，那么静态成员的赋值，只有在我们调用这个静态成员时，才会赋值，这就是 延迟加载技术
            //Console.WriteLine("Main");
            //string str2 = DeloyClass.Str;
            //Console.WriteLine(str2);

            // 静态成员调用的顺序：静态成员在类的内部先完成赋值，然后执行静态函数，最后返回静态成员的引用
            //Console.WriteLine("Main");
            //Console.WriteLine(DeloyClass.testStr);

            Console.ReadLine();
        }
    }
}
