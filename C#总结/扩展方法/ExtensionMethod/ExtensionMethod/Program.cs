using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethod
{
    class Program
    {
        private int a;

        public int Set(int x)
        {
            a += x;
            return a;
        }


        static void Main(string[] args)
        {
            Program pro = new Program();
            pro.Set(8); //类的实例正常调用实例方法
            string str = pro.Get("你好", 7); // 类的实例调用扩展方法
            Console.WriteLine(str);
            Console.Read();
        }
    }
    /*  首先假设因各种原因， 无法修改Program的源码，但是想增加Program实例的方法，此时就可以采用扩展方法*/
    // 1、必须是静态类
    // 2、必须是静态方法
    // 3、第一个参数不需是 this + 实例类型（需要扩展的类型）

    static class MyClass
    {
        // 扩展方法
        public static string Get(this Program program,string str,int x)
        {
            return program.Set(x) + "扩展方法" + str;
        }

        // 普通的增加功能方法：需要当前的静态类来调用
        public static string Get2(Program program, string str, int x)
        {
            return program.Set(x) + "扩展方法" + str;
        }
    }
}
