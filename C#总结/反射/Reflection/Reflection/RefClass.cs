using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public class RefClass
    {
        private int fieldPrivate;
        protected int fieldProtected;
        internal int fieldInternal;
        public int fieldPublic;
        public static string staticFieldPublic;

        private int AttributePrivate { get; set; }
        protected int AttributepProtected { get; set; }
        internal int AttributeInternal { get; set; }
        public int AttributePublic { get; set; }

        public static string StaticAttributePublic { get; set; }

        private void MethodPrivate()
        {
        }
        protected void MethodProtected()
        {
        }
        internal void MethodInternal()
        {
        }
        public int MethodPublic()
        {
            return 1;
        }

        private static void StaticMethodPrivate()
        {
        }
        protected static void StaticMethodProtected()
        {
        }
        internal static void StaticMethodInternal()
        {
        }
        public static void StaticMethodPublic()
        {
        }

        private int x;
        private int y;
        private int z;
        public RefClass()
        {
            x = 0;
            y = 0;
        }

        public RefClass(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public RefClass(string a, int b)
        {
            Console.WriteLine(a + b);
        }

        public RefClass(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Debug()
        {
            Console.WriteLine(x + " " + y + " " + z);
        }
        public int Add()
        {
            Console.WriteLine("调用的方法参数  int  () ");
            return 1;
        }

        public static void Add(int x, int y)
        {
            Console.WriteLine("调用的方法参数  static (int x, int y) ");
        }

        public void Add(string a, int z)
        {
            Console.WriteLine("调用的方法参数   (string a,int z) ");
        }

        public void Add(int x, out int y)
        {
            Console.WriteLine("传入的参数：" + x);
            y = 19;
            Console.WriteLine("调用的方法参数  (int x, out int y) ");
        }
        public void Add(string a, ref int z)
        {
            z++;
            Console.WriteLine("调用的方法参数  (string a, ref int z) ");
        }
    }
}
