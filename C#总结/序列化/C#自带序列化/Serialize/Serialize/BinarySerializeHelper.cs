/*
 *         主题 ：采用 SoapFormatter 格式化器，进行序列化操作
 *
 *         缺点： 不支持泛型成员
 *
 *         优点： 可读性强，互操作性强
 *  
 */
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class BinarySerializeHelper
{
    /// <summary>
    ///  二进制格式化器
    /// </summary>
    private static BinaryFormatter binaryFormatter = new BinaryFormatter();

    /// <summary>
    /// 将内存对象序列化到内存流
    /// </summary>
    /// <param name="instance">需要序列化的对象</param>
    /// <returns>内存流</returns>
    public static MemoryStream InstanceDataToMemory(object instance)
    {
        // 参数检查
        if (instance == null) return null;
        // 需要序列化的对象存在于内存中，所以这里采用 MemoryStream
        MemoryStream memoStream =new MemoryStream();
        // 将对象序列化之后传入流中
        binaryFormatter.Serialize(memoStream,instance);
        return memoStream;

    }

    
   
    /// <summary>
    /// 将内存流反序列化为内存对象(可以根据序列化之前的类型进行转换)
    /// </summary>
    /// <param name="memoryStream">内存流</param>
    /// <param name="isLeaveOpen">离开方法时，流是否保持开的状态</param>
    /// <returns>返回内存对象</returns>
    public static object MemoryToInstanceData(Stream memoryStream, bool isLeaveOpen)
    {
        //参数判断
        if (memoryStream == null) return null;
        // 选择格式化器 
        object obj = binaryFormatter.Deserialize(memoryStream);
        if (!isLeaveOpen)
            memoryStream.Close();
        return obj;
    }
    public static object MemoryToInstanceData(Stream memoryStream)
    {
        return MemoryToInstanceData(memoryStream, false);
    }


    /// <summary>
    /// 序列化对象到文件
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="filepath">文件路径</param>
    /// <param name="isAppend">追加还是创建</param>
    public static void InstanceDataToFile(object obj, string filepath, bool isAppend)
    {
        // 参数判断
        if (filepath == null) throw new Exception("文件名不能为空");
        if (obj == null) return;
        FileStream fileStream = null;
        if (File.Exists(filepath))
        {
            if (isAppend == false)
                fileStream = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite);
            if (isAppend == true)
                fileStream = new FileStream(filepath, FileMode.Append, FileAccess.ReadWrite);
        }
        else
        {
            fileStream = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite);
        }

        try
        {
            binaryFormatter.Serialize(fileStream, obj);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            fileStream.Close();
        }
    }

    public static void InstanceDataToFile(object obj,string filepath)
    {
        InstanceDataToFile(obj, filepath, false);
    }
    

    /// <summary>
    /// 将文件反序列化到对象(可以根据序列化之前的类型进行转换)
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns>对象</returns>
    public static object FileToInstanceData(string filepath)
    {
        if (string.IsNullOrEmpty(filepath))
            throw new Exception("文件名不能为空！");
        if (!File.Exists(filepath))
            throw new Exception("文件不存在！");
        FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.ReadWrite);
        object obj = null;
        try
        {
            obj = binaryFormatter.Deserialize(fs);
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

