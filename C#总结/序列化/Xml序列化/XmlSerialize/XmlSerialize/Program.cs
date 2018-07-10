using System;
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
            SerClass sc = new SerClass();
            sc.pubsby = 1;
            sc.pubbyt = 2;
            sc.pubsho = 3;
            sc.pubush = 4;
            sc.pubfloat = 10.4f;
            sc.pubdou = 5;
            sc.pubint = 6;
            sc.pubuin = 7;
            sc.pubcha = 'k';
            sc.publon = 9;
            sc.pubulo = 10;
            sc.pubdec = 11;
            sc.pubboo = true;
            sc.pubms = new MyStruct();
            sc.pubme = MyEnum.myenum;
            sc.pubobj = new object();
            sc.pubstr = "12";
            sc.pubcla = new class1();
            sc.pubarrint = new int[5];
            sc.pubarr = new System.Collections.ArrayList();
            sc.publis = new List<int>();
            sc.Pubint = 14;


            // 序列化
            //XmlSeriHelper.XmlSerialize<SerClass>(sc,"xml01");
            //XmlSeriHelper.XmlFileDesToObj("xml01.txt", Encoding.UTF8);
            string xmlstr = XmlSeriHelper.XmlSerObjToStr<SerClass>(sc,Encoding.UTF8);
            SerClass ss = XmlSeriHelper.XmlDseStrToObj<SerClass>(xmlstr);
            Console.WriteLine(ss.pubcha); 
            //Console.WriteLine(xmlstr);
            Console.Read();
        }
    }
}
