/*
   * 非静态构造函数的调用顺序： 本类字段和属性 --> 父类的字段和属性(所有修饰符修饰的都要执行) --> 父类的构造函数 --> 子类的构造函数
   */
/* 测试总结
         *
         *  普通类
         *  
         *  类：没有静态构造函数，有静态成员
         *  当类被加载时，
         *  类中的静态字段和属性，通过Net内部的优化方法在其被引用之前完成赋值，这个时间我们无法控制
         *
         *  调用类的静态成员  调用顺序：本类静态字段和属性 --> 本类静态字段和属性
         *  实例化类的成员    调用顺序：本类静态字段和属性 --> 本类静态字段和属性 --> 本类实例化成员 --> 父类实例化成员 --> 父类构造函数 --> 子类构造函数
         *
         *  类：有静态构造函数
         *  当类被加载时，类中的静态字段和属性，不会被赋值
         *  当第一次调用其时，会对静态成员赋值
         *  
         *  调用类的静态成员  调用顺序： 本类的静态字段和属性 --> 本类的静态构造函数 --> 父类的静态字段和属性 --> 父类的静态构造函数
         *  实例化类的成员    调用顺序： 本类的静态字段和属性 --> 本类的静态构造函数 --> 父类的静态字段和属性 --> 父类的静态构造函数 -->
         *                             ---> 本类实例化成员 --> 父类实例化成员(所有修饰符修饰的都要执行) --> 父类构造函数 --> 子类构造函数
         *
         *  特别注意：  const 在所有成员之前被赋值。
         *             readonly 的赋值顺序，相当于实例化成员
         *
         *
         *  静态类(无法继承)
         *  没有静态构造函数：
         *      类中的静态字段和属性，通过Net内部的优化方法在其被引用之前完成赋值，这个时间我们无法控制
         *  有静态构造函数：
         *      调用类的静态成员  调用顺序： 本类的静态字段和属性 --> 本类的静态构造函数
         */
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
            Console.WriteLine(StaticClass.x);
            
            DeloyClass dc = new DeloyClass();
            // 当类的内部没有静态构造函数时，静态成员的赋值，是Net内部的优化方法在静态成员被引用之前完成赋值
            // 但是，这个顺序，我们自己无法把握
            Console.WriteLine(TestClass.Atr);

            Console.WriteLine("Main");
            string str = TestClass.Str;
            Console.WriteLine(str);

            // 在类的内部添加静态构造函数，那么静态成员的赋值，只有在我们调用这个静态成员时，才会赋值，这就是 延迟加载技术

            //Console.WriteLine("Main");

            string str2 = DeloyClass.Str;
            Console.WriteLine(DeloyClass.Atr);
            Console.WriteLine(str2);

            // 静态成员调用的顺序：静态成员在类的内部先完成赋值，然后执行静态函数，最后返回静态成员的引用

            Console.WriteLine("Main");
            Console.WriteLine(DeloyClass.testStr);

            Console.ReadLine();
        }
    }
}
