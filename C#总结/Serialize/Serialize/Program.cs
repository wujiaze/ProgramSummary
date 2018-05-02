using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialize
{
    // 流：为序列化后的字节块提供容纳的容器
    class Program
    {
        #region 内存之间的序列化
        //static void Main(string[] args)
        //{
        //    Hero heroinstance = new Hero();
        //    heroinstance.id = 10000;
        //    heroinstance.attack = 10000f;
        //    heroinstance.defence = 9000f;
        //    heroinstance.name = "DefaultHeroName";
        //    heroinstance.myClist = new List<MyClass>();
        //    heroinstance.myClist.Add(new MyClass("w"));
        //    heroinstance.myDic = new Dictionary<int, MyClass>();
        //    heroinstance.myDic.Add(1, new MyClass("s"));
        //    // 进行序列化
        //    MemoryStream stream = Class1.InstanceDataToMemory(heroinstance);
        //    // 重置 实例对象
        //    heroinstance = null;

        //    // 反序列化
        //    stream.Position = 0; // 流的特性：序号在流的结尾，这里需要从头开始，所以将序号返回0，流中的内容不变
        //    heroinstance = (Hero)Class1.MemoryToInstanceData(stream);
        //    // 说明：1、默认构造不需要写
        //    Console.WriteLine(heroinstance.id); // 说明：属性可以反序列化
        //    Console.WriteLine(heroinstance.attack);
        //    Console.WriteLine(heroinstance.defence);
        //    Console.WriteLine(heroinstance.name); // 说明：字段可以反序列化
        //    // private 修饰的可以序列化
        //    heroinstance.myClist[0].GetName(); // 说明：列表可以序列化
        //    heroinstance.myDic[1].GetName(); // 说明：字典可以序列化
        //    Console.Read();
        //}
        #endregion


        #region 内存到文件，文件到内存
        static void Main(string[] args)
        {
            Hero heroinstance = new Hero();
            heroinstance.id = 10000;
            heroinstance.attack = 10000f;
            heroinstance.defence = 9000f;
            heroinstance.name = "DefaultHeroName";
            heroinstance.myClist = new List<MyClass>();
            heroinstance.myClist.Add(new MyClass("w"));
            heroinstance.myDic = new Dictionary<int, MyClass>();
            heroinstance.myDic.Add(1, new MyClass("s"));
            Class1.InstanceDataToFile(heroinstance, "D:/Desktop/tt.txt");

            Hero hero2 = new Hero();
            hero2 = (Hero)Class1.FileToInstanceData("D:/Desktop/tt.txt");
            Console.WriteLine(heroinstance.id);
            Console.WriteLine(hero2.id);
            hero2.id = 99;
            // 从这里可以看出反序列化出来的是一种深度复制，不是复制了引用，而是复制了内存中的数据
            Console.WriteLine(heroinstance.id);
            Console.WriteLine(hero2.id);
            Console.Read();
        }

        #endregion

    }
}
