/*
 *         主题 ：采用 SoapFormatter 格式化器，进行序列化操作
 *                     流的编码格式由 SoapFormatter 确定
 *
 *         缺点： 不支持泛型成员
 *
 *         优点：1、一般可读性；                                                                                互操作性强(TODO 没有接触过)
 *              2、可以序列化任何类字段成员    值类型：枚举，各种数值类型，布尔类型，自定义结构类型；
 *                                        引用类型：Object，string，类对象，接口对象，委托对象，数组，集合        （TODO 指针，没接触过，接触到了再补充）
 *              3、可以序列化任意修饰符：  public，internal，private，protected，只是反序列化时，注意如何调用即可
 *
 *              4、可以序列化相互引用的对象，格式化器可以检测出相互引用的关系
 *
 *        使用方法： 1、类 / 结构体 必须标记 [Serializable] 特性，类 / 结构体 对象才能使用
 *                  2、不想要序列化的字段成员前加上[NonSerialized]
 *
 *        注意点：  1、反序列化时  类 / 结构体 不需要任何构造函数，有构造函数也没问题
 *                   2、不可以序列化 static const 
 *                  3、属性不能被序列化，但是属性内部由字段构成，所以看上去属性是可以序列化的，故也是可以这样直接用属性的
 *                  4、反序列化出来的是一种深度复制，不是复制了引用，而是复制了内存中的数据
 *                  5、同一个流可以容纳多个对象的序列化，但是反序列化时，只要按照顺序，就可以得到对象的引用
 *  *               6、[Serializable] 特性不能被子类继承，所以 反/序列化 子类时，父类和子类都必须要有 [Serializable] 特性
 *                  7、[NonSerialized] 特性 是可以被子类继承的
 */

using System;
using System.IO;
// 这个命名空间需要在引用中手动添加
using System.Runtime.Serialization.Formatters.Soap;
public class SoapSerializeHelper
{
    /// <summary>
    /// Soap格式化器
    /// </summary>
    private static SoapFormatter soapFormatter = new SoapFormatter();

    // 非泛型
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
        MemoryStream memoStream = new MemoryStream();
        // 将对象序列化之后传入流中
        soapFormatter.Serialize(memoStream, instance);
        return memoStream;
    }
    /// <summary>
    /// 将内存流反序列化为内存对象(可以根据序列化之前的类型进行转换)
    /// </summary>
    /// <param name="stream">内存流</param>
    /// <param name="isLeaveOpen">离开方法时，流是否保持开的状态</param>
    /// <returns>内存对象</returns>
    public static object MemoryToInstanceData(Stream stream, bool isLeaveOpen)
    {
        //参数判断
        if (stream == null) return null;
        object obj = soapFormatter.Deserialize(stream);
        // 关闭流
        if (!isLeaveOpen)
            stream.Close();
        return obj;
    }
    public static object MemoryToInstanceData(Stream stream)
    {
        return MemoryToInstanceData(stream, false);
    }
    /// <summary>
    ///  序列化对象到文件
    /// </summary>
    /// <param name="instance">对象</param>
    /// <param name="filepath">文件路径</param>
    /// <param name="isAppend">追加还是创建</param>
    public static void InstanceDataToFile(object instance, string filepath, bool isAppend)
    {
        // 参数判断
        if (filepath == null) throw new Exception("文件名不能为空");
        if (instance == null) return;
        FileStream fileStream = null;
        if (!File.Exists(filepath))
        {
            fileStream = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite);
        }
        else
        {
            if (isAppend == false)
                fileStream = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite);
            if (isAppend == true)
                fileStream = new FileStream(filepath, FileMode.Append, FileAccess.ReadWrite);
        }
        try
        {
            soapFormatter.Serialize(fileStream, instance);
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            fileStream.Close();
        }
    }
    public static void InstanceDataToFile(object instance, string filepath)
    {
        InstanceDataToFile(instance, filepath, false);
    }
    /// <summary>
    /// 将文件反序列化到对象(可以根据序列化之前的类型进行转换)
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns></returns>
    public static object FileToInstanceData(string filepath)
    {
        if (string.IsNullOrEmpty(filepath))
            throw new Exception("文件名不能为空！");
        if (!File.Exists(filepath))
            throw new Exception("文件不存在！");
        FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.ReadWrite);
        object obj = null;
        try
        {
            obj = soapFormatter.Deserialize(fileStream);
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            fileStream.Close();
        }
        return obj;
    }



    // 泛型
    public static MemoryStream InstanceDataToMemory<T>(T instance)
    {
        // 参数检查
        if (instance == null) return null;
        // 需要序列化的对象存在于内存中，所以这里采用 MemoryStream
        MemoryStream memoStream = new MemoryStream();
        // 将对象序列化之后传入流中
        soapFormatter.Serialize(memoStream, instance);
        return memoStream;
    }
    public static T MemoryToInstanceData<T>(Stream stream, bool isLeaveOpen)
    {
        //参数判断
        if (stream == null) return default(T);
        object obj = soapFormatter.Deserialize(stream);
        // 关闭流
        if (!isLeaveOpen)
            stream.Close();
        return (T)obj;
    }
    public static T MemoryToInstanceData<T>(Stream stream)
    {
        return MemoryToInstanceData<T>(stream, false);
    }
    public static void InstanceDataToFile<T>(T instance, string filepath, bool isAppend)
    {
        // 参数判断
        if (filepath == null) throw new Exception("文件名不能为空");
        if (instance == null) return;
        FileStream fileStream = null;
        if (!File.Exists(filepath))
        {
            fileStream = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite);
        }
        else
        {
            if (isAppend == false)
                fileStream = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite);
            if (isAppend == true)
                fileStream = new FileStream(filepath, FileMode.Append, FileAccess.ReadWrite);
        }
        try
        {
            soapFormatter.Serialize(fileStream, instance);
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            fileStream.Close();
        }
    }
    public static void InstanceDataToFile<T>(T instance, string filepath)
    {
        InstanceDataToFile<T>(instance, filepath, false);
    }
    public static T FileToInstanceData<T>(string filepath)
    {
        if (string.IsNullOrEmpty(filepath))
            throw new Exception("文件名不能为空！");
        if (!File.Exists(filepath))
            throw new Exception("文件不存在！");
        FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.ReadWrite);
        object obj = null;
        try
        {
            obj = soapFormatter.Deserialize(fileStream);
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            fileStream.Close();
        }
        return (T)obj;
    }
}

