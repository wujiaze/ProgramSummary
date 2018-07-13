using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlSerialize
{
    class Program
    {
        static void Main(string[] args)
        {
            MyEnum me = MyEnum.your;
            MyStruct ms = new MyStruct(1,2,3);
            Object obj = new object();
            Class1 c1 = new Class1(1,2,3);
            IInterface iif = new ClassFace();
            int[] arrint = new int[] { 10, 11, 12 };
            Class1 c2 = new Class1(2,3,4);
            Class1[] arrc1 = new Class1[] { c1, c2 };
            ArrayList arrlist = new ArrayList(); arrlist.Add("{}");
            List<int> intlist = new List<int>(); intlist.Add(999);
            Hashtable has = new Hashtable(); has.Add('2', 5);
            Dictionary<float, byte> dic = new Dictionary<float, byte>(); dic.Add(1.5f, 15);
            Queue arrqueue = new Queue(); arrqueue.Enqueue(c2);
            Queue<double> queue = new Queue<double>(); queue.Enqueue(15.63);
            Stack arratack = new Stack(); arratack.Push(me);
            Stack<decimal> stack = new Stack<decimal>(); stack.Push(47);
            List<ArrayList> aar1 = new List<ArrayList>(); aar1.Add(arrlist);
            ArrayList[] a = new ArrayList[] { arrlist };
            ClassFace cf = new ClassFace();
            

            MyClass mc1 = new MyClass(me, -10, 15, '=', -99, 89, 15, -45, 25, -78, true, ms, obj, "你好！AVB+", c1, iif, Print, arrint,
               arrc1, arrlist, intlist, has, dic, arrqueue, queue, arratack, stack, aar1, a,cf,96);
            //cf.my = mc1;

            XmlSerializerHelper.InstanceToFileByXml(mc1, "D:\\Desktop\\xml2.xml");
            MyClass m0 = XmlSerializerHelper.FileToInstanceByXml<MyClass>("D:\\Desktop\\xml2.xml"); m0.Print();


            MemoryStream stream = XmlSerializerHelper.InstanceToMemoryByXml(mc1);
            MyClass cc = XmlSerializerHelper.MemoryToInstanceByXml<MyClass>(stream, 0); cc.Print();


            Console.Read();
        }

        private static void Print()
        {
            Console.WriteLine("1111");
        }
    }
}
