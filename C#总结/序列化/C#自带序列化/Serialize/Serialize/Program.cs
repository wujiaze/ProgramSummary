/*
 *      序列化的作用：将 对象 按照一定的方式变成 byte ，写入文件
 *
 *      与之前的 stringwrite 和 streamwrite 的区别在于：
 *      序列化既可针对对象，也可用于基本类型
 *      而 stringwrite 和 streamwrite 只能用于基本类型
 *
 */
using System;
using System.IO;

namespace Serialize
{
    class Program
    {
        // TODO 序列化的文件是Json文件，
        static void Main(string[] args)
        {
            BaseClass ba = new BaseClass();
            SerTest st2 = new SerTest(1000, 5000f, 1000f, 1000f, ba);
            ba.ser = st2;
            string path = "D:\\Desktop\\1.bin";

            BinarySerializeHelper.InstanceDataToFile(st2, path);
            SerTest mc = BinarySerializeHelper.FileToInstanceData<SerTest>(path); mc.Print();

            MemoryStream stram = BinarySerializeHelper.InstanceDataToMemory(st2);
            SerTest m0 = BinarySerializeHelper.MemoryToInstanceData<SerTest>(stram, 0);m0.Print();

            SoapSerializeHelper.InstanceDataToFile(st2, path);
            SerTest mc1 = BinarySerializeHelper.FileToInstanceData<SerTest>(path); mc1.Print();

            MemoryStream stram1 = BinarySerializeHelper.InstanceDataToMemory(st2);
            SerTest m01 = BinarySerializeHelper.MemoryToInstanceData<SerTest>(stram1, 0); m01.Print();

            Console.ReadLine();
        }
    }
}
