using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;


// 由于 SoapFormatter 不能序列化泛型，所以就不采用这个了
namespace Serialize
{
   public class Class1
    {
        public static MemoryStream InstanceDataToMemory(object instance)
        {
            // 需要序列化的对象存在于内存中，所以这里采用 MemoryStream
            MemoryStream memoStream =new MemoryStream();
            // 选择格式化器 
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            // 将对象序列化之后传入流中
            binaryFormatter.Serialize(memoStream,instance);
            return memoStream;
        }

        public static object MemoryToInstanceData(Stream memoryStream)
        {
            // 选择格式化器 
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            object obj = binaryFormatter.Deserialize(memoryStream);
            memoryStream.Close();
            return obj;
        }

        // SoapFormatter 需要先在引用中添加dll文件
        public static void InstanceDataToFile(object instance,string filepath)
        {
            
            if (instance==null)
            {
                return;
            }
            FileStream fs =new FileStream(filepath,FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                bf.Serialize(fs, instance);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        public static object FileToInstanceData(string filepath)
        {
            FileStream fs = new FileStream(filepath, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                object obj = bf.Deserialize(fs);
                fs.Close();
                return obj;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }


    }
}
