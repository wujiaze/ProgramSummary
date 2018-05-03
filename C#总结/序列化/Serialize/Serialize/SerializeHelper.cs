using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

// 由于 SoapFormatter 不能序列化泛型，所以就不采用这个格式化器了
// 这个命名空间需要在引用中手动添加
using System.Runtime.Serialization.Formatters.Soap; 

namespace Serialize
{
   public class SerializeHelper
    {
        /// <summary>
        /// 将对象序列化到内存流
        /// </summary>
        /// <param name="instance">需要序列化的对象</param>
        /// <returns></returns>
        public static MemoryStream InstanceDataToMemory(object instance)
        {
            
            // 参数检查
            if (instance == null) return null;
            // 需要序列化的对象存在于内存中，所以这里采用 MemoryStream
            MemoryStream memoStream =new MemoryStream();
           
            // 选择格式化器 
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            // 将对象序列化之后传入流中
            binaryFormatter.Serialize(memoStream,instance);
            return memoStream;

        }
        /// <summary>
        /// 将内存流反序列化为对象
        /// </summary>
        /// <param name="memoryStream">内存流</param>
        /// <returns></returns>
        public static object MemoryToInstanceData(Stream memoryStream)
        {
            //参数判断
            if (memoryStream == null) return null;
            // 选择格式化器 
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            object obj = binaryFormatter.Deserialize(memoryStream);
            memoryStream.Close();
            return obj;
        }

        /// <summary>
        /// 序列化对象到文件
        /// </summary>
        /// <param name="instance">对象</param>
        /// <param name="filepath">文件路径</param>
        public static void InstanceDataToFile(object instance,string filepath)
        {
            // 参数判断
            if (instance==null) return; 
            FileStream fs =new FileStream(filepath,FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                bf.Serialize(fs, instance);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                fs.Close();
            }
        }

        /// <summary>
        /// 将文件反序列化到对象
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static object FileToInstanceData(string filepath)
        {
            FileStream fs = new FileStream(filepath, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            object obj = null;
            try
            {
                obj = bf.Deserialize(fs);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                fs.Close();
            }
            return obj;
        }

       
    }
}
