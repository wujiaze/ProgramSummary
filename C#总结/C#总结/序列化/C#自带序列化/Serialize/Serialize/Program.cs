/*
 *      序列化的作用：将 对象 按照一定的方式变成 byte ，写入文件
 *
 *      与之前的 stringwrite 和 streamwrite 的区别在于：
 *      序列化既可针对对象，也可用于基本类型
 *      而 stringwrite 和 streamwrite 只能用于基本类型
 *
 */
using System;
using System.Collections.Generic;
using System.IO;
using Serialize.测试各种集合;

namespace Serialize
{
    class Program
    {
        // TODO 序列化的文件是Json文件，
        static void Main(string[] args)
        {
            BaseClass ba = new BaseClass();
            SerTest st2 = new SerTest(1000, 5000f, 1000f, 1000f);
            string path = "D:\\Desktop\\1.bin";

            BinarySerializeHelper.InstanceDataToFile(st2, path);
            SerTest mc = BinarySerializeHelper.FileToInstanceData<SerTest>(path); mc.Print();

            MemoryStream stram = BinarySerializeHelper.InstanceDataToMemory(st2);
            SerTest m0 = BinarySerializeHelper.MemoryToInstanceData<SerTest>(stram, 0); m0.Print();

            SoapSerializeHelper.InstanceDataToFile(st2, path);
            SerTest mc1 = BinarySerializeHelper.FileToInstanceData<SerTest>(path); mc1.Print();

            MemoryStream stram1 = BinarySerializeHelper.InstanceDataToMemory(st2);
            SerTest m01 = BinarySerializeHelper.MemoryToInstanceData<SerTest>(stram1, 0); m01.Print();



            /* 测试结果：两种都正常使用*/ 
            /* 测试 集合 */
            ClassCollection cc1 = new ClassCollection();
            // 第一种 字段全为null
            MemoryStream temp = BinarySerializeHelper.InstanceDataToMemory(cc1);
            Console.WriteLine(temp.Length);
            ClassCollection cc2 = BinarySerializeHelper.MemoryToInstanceData<ClassCollection>(temp, 0);
            cc2.Print();
            Console.WriteLine("---------------------");
            // 第二种 正常
            List<SerTest> list = new List<SerTest>();
            Dictionary<string, SerTest> dict = new Dictionary<string, SerTest>();
            Stack<SerTest> stack = new Stack<SerTest>();
            Queue<SerTest> queue = new Queue<SerTest>();
            list.Add(st2); dict.Add("1", st2); stack.Push(st2); queue.Enqueue(st2);
            ClassCollection cc3 = new ClassCollection(list, dict, stack, queue);
            cc3.Myfloat = 0.9f;
            MemoryStream temp2 = BinarySerializeHelper.InstanceDataToMemory(cc3);
            Console.WriteLine("**");
            Console.WriteLine(temp2.Length);
            ClassCollection cc4 = BinarySerializeHelper.MemoryToInstanceData<ClassCollection>(temp2, 0);
            cc4.PrintDetail();
            Console.WriteLine(cc4.x);
            Console.WriteLine(cc4.Myfloat);
            Console.ReadLine();
            
        }
    }
}


