/*
 *      Title：.Net框架自带的 Xml序列化器   XmlSerializer 进行序列化操作
 *
 *      优点:  1、可以序列化字段(public)和属性(属性的访问器必须是public，且两种访问器都要有)     
 *             2、可以在得到的xml文档中添加注释
 *             3、支持 List<>  ArrayList
 *      缺点：1、不能序列化  委托对象   接口对象(注意:实现接口的类是可以序列化的)
 *                               实现 IDictionary,IDictionary<> 接口的对象，比如：HashTable/Dictionary，
 *                               实现 ICollection,ICollection<> 接口的对象，比如：Queue/Queue<> ,Stack/Stack<>
 *            2、不支持 ArrayList[]  List<>[]
 *            3、不支持多维数组,但支持交错数组
 *
 *      使用方法：1、不想要序列化的字段成员前加上 [XmlIgnore] 特性,可以被继承
 *               2、[XmlAttribute] 特性，使字段或属性，变成Xml文档中的属性
 *
 *      使用注意点: 1、必须有显示/隐式的默认构造函数
 *                 2、只能序列化是public修饰的对象
 *                 3、[Obsolete] 对象不再序列化
 *                 4、不支持 static const readonly
 *                 5、序列化对象不能相互引用，只支持单向引用
 *
 *                  todo 不加 特性 [serilizar] 进行网络传输 用于网络传输，1.什么都不加测试，加上[Serializable]测试，加上[XmlAttribute("AreaName")] 在测试 
 */


using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace XmlSerialize
{
    public class XmlSerializerHelper
    {
        // StreamWrite / StreamReader

        #region 序列化到内存
        /// <summary>
        /// 序列化对象到内存流
        ///     未关闭流
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">序列化对象</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>返回的流的Position是流的末尾</returns>
        public static MemoryStream InstanceToMemoryByXml<T>(T instance, Encoding encoding = null)
        {
            // 参数检查
            if (instance == null) return null;
            // 获取编码格式
            if (encoding == null) encoding = Encoding.UTF8;
            MemoryStream memoryStream = new MemoryStream();
            using (StreamWriter streamWriter = new StreamWriter(memoryStream, encoding, 1024, true)) //不关闭底层流
            {
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));    // 这个构造函数不会内存泄漏
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    xmlSerializer.Serialize(streamWriter, instance, ns);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return memoryStream;
        }

        /// <summary>
        /// 将流反序列化到内存对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">流</param>
        /// <param name="streamIndex">设置流开始读取的位置</param>
        /// <param name="encoding">读取编码格式</param>
        /// <param name="leaveOpen">离开方法时，流是否保持开的状态</param>
        /// <returns>内存对象</returns>
        public static T MemoryToInstanceByXml<T>(Stream stream, long streamIndex, Encoding encoding = null, bool leaveOpen = false)
        {
            //参数判断
            if (stream == null) return default(T);
            // 获取编码格式
            if (encoding == null) encoding = Encoding.UTF8;
            using (StreamReader reader = new StreamReader(stream, encoding, true, 1024, leaveOpen))
            {
                try
                {
                    stream.Position = streamIndex;
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    T resultObj = (T)xmlSerializer.Deserialize(reader);
                    return resultObj;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }


        #endregion


        #region 序列化到文件
        /// <summary>
        /// 内存对象序列化到文件
        /// </summary>
        /// <typeparam name="T"> 对象类型</typeparam>
        /// <param name="instance">序列化对象</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="isAppend">是否追加模式</param>
        /// <param name="encoding">编码方式</param>
        public static void InstanceToFileByXml<T>(T instance, string filePath, bool isAppend = false, Encoding encoding = null)
        {
            // 参数判断
            if (instance == null) return;
            if (filePath == null) throw new Exception("文件名不能为空");
            // 修改扩展名
            filePath = Path.ChangeExtension(filePath, ".xml");
            // 获取编码格式
            if (encoding == null) encoding = Encoding.UTF8;
            // 获取文件流
            FileStream fileStream = GetFileStream(filePath, isAppend);
            using (StreamWriter streamWriter = new StreamWriter(fileStream, encoding)) // 这个构造函数，默认关闭底层流
            {
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));    // 这个构造函数不会内存泄漏
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    xmlSerializer.Serialize(streamWriter, instance, ns);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// 文件反序列化到内存对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="filePath">文件路径</param>
        /// <param name="encoding">编码格式</param>
        /// <returns></returns>
        public static T FileToInstanceByXml<T>(string filePath, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new Exception("文件名不能为空！");
            if (!File.Exists(filePath))
                throw new Exception("文件不存在！");
            // 获取编码格式
            if (encoding == null)
                encoding = Encoding.UTF8;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            T resultObj = default(T);
            using (StreamReader reader = new StreamReader(fileStream, encoding))
            {
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    resultObj = (T)xmlSerializer.Deserialize(reader);
                    return resultObj;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /* 辅助方法 */
        private static FileStream GetFileStream(string filePath, bool isAppend)
        {
            FileStream fileStream = null;
            if (File.Exists(filePath))
            {
                if (isAppend == false)
                    fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                if (isAppend == true)
                    fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            }
            return fileStream;
        }


        #endregion

        // TODO 使用 XmlWrite/XmlReader xml文件,添加自定义格式？

        #region 等 XmlWrite 学好再看
        //// XmlWrite/XmlReader
        //// 1.1 Obj对象 序列化为 Xml字符串
        //public static string XmlSerObjToStr<T>(T instance, Encoding encoding)
        //{
        //    string str = "";
        //    // xml 格式设置
        //    XmlWriterSettings settings = new XmlWriterSettings();
        //    settings.Encoding = Encoding.UTF8;
        //    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        //    ns.Add("", "");
        //    settings.Indent = true;
        //    settings.IndentChars = "\t";
        //    settings.NewLineChars = "\r\n";
        //    // xml 序列化器
        //    XmlSerializer serializer = new XmlSerializer(typeof(T));
        //    // instance 是内存中的数据，就用内存流来保存序列化后的数据
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        using (XmlWriter xw = XmlWriter.Create(ms, settings))
        //        {
        //            serializer.Serialize(xw, instance, ns);
        //            xw.Dispose(); // 这里 XmlWriter 似乎需要提前关闭，具体原因不是很懂
        //            // 以下是为了返回给用户使用
        //            using (StreamReader sr = new StreamReader(ms))
        //            {
        //                ms.Position = 0;
        //                str = sr.ReadToEnd();
        //            }
        //        }
        //    }
        //    return str;
        //}

        //// 1.2 xml字符串 反序列化为 Obj对象
        //public static T XmlDseStrToObj<T>(string xmlStr)
        //{
        //    if (xmlStr == null)
        //    {
        //        throw new Exception("请输入有效Xml字符串");
        //    }
        //    T t = default(T);
        //    XmlSerializer serializer = new XmlSerializer(typeof(T));
        //    using (StringReader sr = new StringReader(xmlStr))
        //    {
        //        t = (T)serializer.Deserialize(sr);
        //    }
        //    return t;
        //}


        //// 2.1  obj对象序列化转换成xml文件
        //public static void XmlSerObjToFile<T>(T instance, string outputPath)
        //{
        //    // 方法3 ：XmlWriter写入

        //    // 设置Xml文档格式
        //    XmlWriterSettings settings = new XmlWriterSettings();
        //    // 设置xml文档的编码格式
        //    settings.Encoding = Encoding.UTF8;   //  settings.OmitXmlDeclaration = false; 去除开头
        //    // 设置xml文档的命名空间
        //    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        //    ns.Add("", "");
        //    // 设置缩进
        //    settings.Indent = true;
        //    settings.IndentChars = "\t";
        //    // 设置换行
        //    settings.NewLineChars = "\r\n";
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        using (XmlWriter xw = XmlWriter.Create(stream, settings))
        //        {
        //            XmlSerializer serializer = new XmlSerializer(typeof(T));
        //            serializer.Serialize(xw, instance, ns);
        //            xw.Close();
        //            stream.Close();
        //            Console.WriteLine(stream.ToString());
        //        }
        //    }
        //}

        //// 2.2  xml文件反序列化为Obj对象
        //// 前提：需要有对应的类来接受这个对象
        //public static void XmlFileDesToObj(string filePath, Encoding encoding)
        //{
        //    if (filePath == null || !File.Exists(filePath))
        //    {
        //        throw new Exception("请输入有效路径");
        //    }
        //    if (encoding == null)
        //    {
        //        throw new Exception("请输入有效的字符编码方式");
        //    }

        //    string filetext = File.ReadAllText(filePath, encoding);

        //    Console.WriteLine(filetext);
        //}



        #endregion





    }
}
