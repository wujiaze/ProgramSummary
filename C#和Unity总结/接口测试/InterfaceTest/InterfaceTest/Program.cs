using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceTest
{
    class Program
    {
        ITest test;
        public  void Set(ITest it) {
            test = it;
        }
        static void Main(string[] args)
        {
            Program p1 = new Program();
            p1.Set(new Class1());
            p1.Set(new Class2());
            p1.test.print("5555555555");   // 这步说明，接口对象只执行最后赋值的类的方法

            // 下面两步印证了上面的结论
            Program p2 = new Program();
            p2.Set(new Class2());
            p2.test.print("5555555555");
            Program p3 = new Program();
            p3.Set(new Class1());
            p3.test.print("5555555555");
            Console.Read();
        }
        
    }
}
