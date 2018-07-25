using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeloyLoad
{
    public class BaseClass
    {
        public static string mtstr = "888";
        public static string Str = Show("222");

        private static string Show(string v)
        {
            Console.WriteLine("父类静态成员");
            return v;
        }

        static BaseClass()
        {
            mtstr = "999";
            Console.WriteLine(DeloyClass.Str);
            Console.WriteLine("父类静态构造函数");
        }

        public BaseClass()
        {
            Console.WriteLine("父类构造函数");
        }
    }
}
