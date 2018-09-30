using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeloyLoad
{
    public class DeloyClass : BaseClass
    {
        public static string testStr = "333";
        public const int x = 0;
        public  readonly object _lockobj = null;
        public string ll = "22";
        public static string Str = Show("222");
        public static string Atr { get; set; } = "1";

        public static string Show(string str)
        {
            Console.WriteLine("子类静态成员");
            Console.WriteLine("内部" + str);
            return str;
        }


        static DeloyClass()
        {

            Console.WriteLine("子类静态构造函数");
            Atr = "2";
            Str = "555";
            //_lockobj = new object();
            Console.WriteLine("静态构造");
        }

        public DeloyClass()
        {
            Console.WriteLine("子类构造函数");
        }
    }
}
