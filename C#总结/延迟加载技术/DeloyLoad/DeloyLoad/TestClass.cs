using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeloyLoad
{
     public class TestClass
     {
         public static string Str = Show("111");
         public static string Show(string str)
         {
             Console.WriteLine("内部"+ str);
             return str;
         }
     }
}
