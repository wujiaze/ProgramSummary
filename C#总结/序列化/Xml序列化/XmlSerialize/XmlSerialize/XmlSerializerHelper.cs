/*
 *      Title：.Net框架自带的 Xml序列化器   XmlSerializer 进行序列化操作
 *
 *      优点：1、可以序列化字段和属性（TODO 字段和属性是同一个）属性访问器私有化？，标签，xml的开头
 *
 *      使用注意点: 1、必须有显示/隐式的默认构造函数
 *                 2、只能是public修饰
 *                 3、不能序列化      接口对象
 *                                   实现（IDictionary,IDictionary<>）的HashTable/Dictionary，
 *                                   实现（ICollection,ICollection<>）的Queue/Queue<> ,Stack/Stack<>
 *                 4、不支持 ArrayList[]  List<>[]
 *                 5、不支持多维数组,但支持交错数组
 *                 4、[Obsolete] 对象不再序列化
 *                
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace XmlSerialize
{
    public class XmlSerializerHelper
    {
        
        // 首先，说明 StringWriter StreamWriter XmlWriter 都可以写入，
        // 由于这里是写入xml文档，XmlWriter可以设置xml文档的格式，并且.net特意写了这个XmlWriter，所以写入采用XmlWriter

        // 第一部分
        // 1.1 Obj对象 序列化为 Xml字符串
        public static string XmlSerObjToStr<T>(T obj,Encoding encoding)
        {
            string str = "";
            // xml 格式设置
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            settings.Indent = true;
            settings.IndentChars = "\t";
            settings.NewLineChars = "\r\n";
            // xml 序列化器
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            // obj 是内存中的数据，就用内存流来保存序列化后的数据
            using (MemoryStream ms = new MemoryStream())
            {
                using (XmlWriter xw = XmlWriter.Create(ms, settings))
                {
                    serializer.Serialize(xw, obj, ns);
                    xw.Dispose(); // 这里 XmlWriter 似乎需要提前关闭，具体原因不是很懂
                    // 以下是为了返回给用户使用
                    using (StreamReader sr = new StreamReader(ms))
                    {
                        ms.Position = 0;
                        str = sr.ReadToEnd();
                    }
                }
            }
            return str; 
        }

        // 1.2 xml字符串 反序列化为 Obj对象
        public static T XmlDseStrToObj<T>(string xmlStr) {
            if (xmlStr == null )
            {
                throw new Exception("请输入有效Xml字符串");
            }
            T t = default(T);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader sr = new StringReader(xmlStr))
            {
                t = (T)serializer.Deserialize(sr);
            }
            return t;
        }

        // 第二部分
        // 2.1  obj对象序列化转换成xml文件
        public static void XmlSerObjToFile<T>(T obj,string outputPath)
        {
            // 方法3 ：XmlWriter写入

            // 设置Xml文档格式
            XmlWriterSettings settings = new XmlWriterSettings();
            // 设置xml文档的编码格式
            settings.Encoding = Encoding.UTF8;   //  settings.OmitXmlDeclaration = false; 去除开头
            // 设置xml文档的命名空间
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            // 设置缩进
            settings.Indent = true;
            settings.IndentChars = "\t";
            // 设置换行
            settings.NewLineChars = "\r\n";
            using (MemoryStream stream = new MemoryStream())
            {
                using (XmlWriter xw = XmlWriter.Create(stream, settings))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(xw, obj, ns);
                    xw.Close();
                    stream.Close();
                    Console.WriteLine(stream.ToString()); 
                }
            } 
        }

        // 2.2  xml文件反序列化为Obj对象
        // 前提：需要有对应的类来接受这个对象
        public static void XmlFileDesToObj(string filePath,Encoding encoding) {
            if (filePath == null || !File.Exists(filePath))
            {
                throw new Exception("请输入有效路径");
            }
            if (encoding == null )
            {
                throw new Exception("请输入有效的字符编码方式");
            }

            string filetext = File.ReadAllText(filePath, encoding); 

            Console.WriteLine(filetext);
        }
        

        // 序列化到文件
        public static void InstanceToFileByXml(object instance,string filePath,bool isAppend=false,Encoding encoding = null)
        {
            // 参数判断
            if (instance == null) return;
            if (filePath == null) throw new Exception("文件名不能为空");
            filePath = Path.ChangeExtension(filePath, ".xml");
            FileStream fileStream = GetFileStream(filePath, isAppend);
            if (encoding == null)
                encoding = Encoding.UTF8;
            using (StreamWriter write = new StreamWriter(fileStream, encoding))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(instance.GetType());
                xmlSerializer.Serialize(write, instance);
            }
        }

        private static FileStream GetFileStream(string filePath, bool isAppend = false)
        {
            FileStream fileStream = null;
            if (File.Exists(filePath))
            {
                if (isAppend == false)
                    fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
                if (isAppend == true)
                    fileStream = new FileStream(filePath, FileMode.Append, FileAccess.ReadWrite);
            }
            else
            {
                fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            }
            return fileStream;
        }



        public static void InstanceToFileByXml(object instance, FileStream stream)
        {
            if (instance == null) return;
            using (stream)
            {

            }
        }
        public static void InstanceToFileByXml(object instance, FileStream stream, bool isleaveOpen)
        {
            if (instance == null) return;
            using (stream)
            {

            }
        }
        // 序列化到内存












    }
}
