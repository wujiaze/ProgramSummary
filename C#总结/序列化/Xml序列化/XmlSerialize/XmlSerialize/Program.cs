using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlSerialize
{
    class Program
    {
        static void Main(string[] args)
        {
            #region MyRegion
            //SerClass sc = new SerClass();
            //sc.pubsby = 1;
            //sc.pubbyt = 2;
            //sc.pubsho = 3;
            //sc.pubush = 4;
            //sc.pubfloat = 10.4f;
            //sc.pubdou = 5;
            //sc.pubint = 6;
            //sc.pubuin = 7;
            //sc.pubcha = 'k';
            //sc.publon = 9;
            //sc.pubulo = 10;
            //sc.pubdec = 11;
            //sc.pubboo = true;
            //sc.pubms = new MyStruct();
            //sc.pubme = MyEnum.myenum;
            //sc.pubobj = new object();
            //sc.pubstr = "12";
            //sc.pubcla = new class1();
            //sc.pubarrint = new int[5];
            //sc.pubarr = new System.Collections.ArrayList();
            //sc.publis = new List<int>();
            //sc.Pubint = 14;


            // 序列化
            //XmlSerializerHelper.XmlSerialize<SerClass>(sc,"xml01");
            //XmlSerializerHelper.XmlFileDesToObj("xml01.txt", Encoding.UTF8);
            //string xmlstr = XmlSerializerHelper.XmlSerObjToStr<SerClass>(sc,Encoding.UTF8);
            //SerClass ss = XmlSerializerHelper.XmlDseStrToObj<SerClass>(xmlstr);
            //Console.WriteLine(ss.pubcha); 
            //Console.WriteLine(xmlstr);
            #endregion

            MyEnum me = MyEnum.your;
            MyStruct ms = new MyStruct();
            Object obj = new object();
            Class1 c1 = new Class1();
            IInterface iif = new ClassFace();
            int[] arrint =new int[]{10,11,12};
            Class1 c2 = new Class1();
            Class1[] arrc1 = new Class1[]{ c1 ,c2};
            ArrayList arrlist = new ArrayList();arrlist.Add("{}");
            List<int> intlist = new List<int>();intlist.Add(999);
            Hashtable has = new Hashtable();has.Add('2',5);
            Dictionary<float,byte> dic = new Dictionary<float, byte>();dic.Add(1.5f,15);
            Queue arrqueue = new Queue(); arrqueue.Enqueue(c2);
            Queue<double> queue = new Queue<double>(); queue.Enqueue(15.63);
            Stack arratack = new Stack(); arratack.Push(me);
            Stack<decimal> stack = new Stack<decimal>(); stack.Push(47);
            List<ArrayList> aar1 = new List<ArrayList>(); aar1.Add(arrlist);
            ArrayList[] a = new ArrayList[]{ arrlist };
             MyClass mc1 = new MyClass(me, -10,15,'=',-99,89,15,-45,25,-78,true,ms, obj,"oopp",c1, iif, Print, arrint,
                arrc1, arrlist, intlist, has, dic, arrqueue, queue, arratack, stack, aar1, a);

            XmlSerializerHelper.InstanceToFileByXml(mc1,"D:\\Desktop\\xml2.txt");
            Console.Read();
        }

        private static void Print()
        {
            Console.WriteLine("action~~");
        }

        private static void Look(int x)
        {
            Console.WriteLine(x);
            x = 10;
            Console.WriteLine(x);
        }
    }
}
