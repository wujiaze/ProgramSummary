using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeloyLoad
{
    public class TestClass
    {
        public static string Str = Show("111");
        public static string Atr { get; set; } = "1";

        public static string Show(string str)
        {
            Console.WriteLine("无静态构造函数的静态方法");
            Console.WriteLine("内部" + str);
            return str;
        }
    }
}
