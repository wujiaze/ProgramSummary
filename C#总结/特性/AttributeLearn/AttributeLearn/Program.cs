#define mydefine // 定义一个宏

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace 特性
{
    // 通过制定属性的名字，给属性赋值，命名参数
    [Test("这是个简单的特性类", ID = 200)]
    //[Test("这是个简单的特性类",ID = 100)]
    class Program
    {
        #region  Obsolete
        [Obsolete]
        static void OldMethod()
        {
            Console.WriteLine("OldMethod");
        }

        [Obsolete("这个方法已过时,使用NewMethod代替")]
        static void OldMethod2()
        {
            Console.WriteLine("OldMethod2");
        }
        [Obsolete("这个方法已过时,使用NewMethod代替",true)]
        static void OldMethod3()
        {
            Console.WriteLine("OldMethod3");
        }

        static void NewMethod()
        {
            Console.WriteLine("NewMethod");
        }
        #endregion

        #region Conditional

        [Conditional("mydefine")]
        static void Test1()
        {
            Console.WriteLine("Test1");
        }

        static void Test2()
        {
            Console.WriteLine("Test2");
        }

        #endregion

        #region DebuggerStepThrough
        [DebuggerStepThrough]// 确定方法没问题,跳过---调试时使用
        static int Method01(int a)
        {
            return a = a + 1;
        }

        static void Method02(int a)
        {
            Console.WriteLine(a);
        }

        #endregion

        #region Flags
        // 常用于枚举，这个枚举就成为了位标记枚举
        //  所以可以使用按位或“|”来进行组合。


        #endregion


        #region MyAttribute
        //[Test]
        static void play()
        {
            
        }


        #endregion
        static void Main(string[] args)
        {
            // 先介绍几个系统自带的特性
            //Obsolete：提示方法过时
            //OldMethod();
            //OldMethod2();
            //OldMethod3();
            //NewMethod();

            //Conditional:允许我们取消特定方法的所有调用
            //Test1();
            //Test2();
            //Test1();
            //Test2();
            //Test1();
            //Test2();
            //Test1();
            //Test2();
            //int a = 9;
            //Method02(Method01(a));
            
            // 特性：是一种允许我们向程序的程序集增加元数据的语言结构
            // 还有很多其他的特性

            // 自定义一个特性,用来了解特性
            Type type = typeof(Program);
            // false :不搜索父类
            object[] array =type.GetCustomAttributes(false);
            TestAttribute test = array[0] as TestAttribute;
            Console.WriteLine(test.Description);
            Console.WriteLine(test.ID);
            Console.ReadKey();
        }
    }
}
