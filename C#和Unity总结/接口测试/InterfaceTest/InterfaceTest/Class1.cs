using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceTest
{
    class Class1 : ITest
    {
        public void print(string str)
        {
            Console.WriteLine("Class1实现"+ str);
        }
    }
}
