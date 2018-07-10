/*
 *      序列化的作用：将 对象 按照一定的方式变成 byte ，写入文件
 *
 *      与之前的 stringwrite 和 streamwrite 的区别在于：
 *      序列化既可针对对象，也可用于基本类型
 *      而 stringwrite 和 streamwrite 只能用于基本类型
 *
 */
using System;

namespace Serialize
{
    class Program
    {
        // TODO 序列化的文件是Json文件，xml文件,添加格式
        static void Main(string[] args)
        {
            SerTest st2 = new SerTest(1000, 5000f, 1000f, 1000f);
            BinarySerializeHelper.InstanceDataToFile(st2, "D:\\Desktop\\1.txt");
            SerTest mc = (SerTest)BinarySerializeHelper.FileToInstanceData("D:\\Desktop\\1.txt");
            mc.Print();
            Console.ReadLine();
        }
    }
}
