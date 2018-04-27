using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceTest
{
    class Class2 : ITest
    {
        public void print(string str)
        {
            Console.WriteLine("Class2实现"+ str);
        }
    }
}
