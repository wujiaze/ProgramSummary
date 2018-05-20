using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeloyLoad
{
public class DeloyClass
    {
        public static string testStr = "333";
        public static string Str = Show("222");
        public static string Show(string str)
        {
            Console.WriteLine("内部" + str);
            return str;
        }

        
        static DeloyClass()
        {
            Console.WriteLine("静态构造");
        }
    }
}
