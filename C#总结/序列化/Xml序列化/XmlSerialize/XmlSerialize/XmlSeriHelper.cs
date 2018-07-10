using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace XmlSerialize
{
    public class XmlSeriHelper
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
















    }
    // 方法1：字符串写入
    //StringWriter sw = new StringWriter();
    //serializer.Serialize(sw, obj);
    //sw.Close();
    //sw.Dispose();
    // 方法2：流写入
    //StreamWriter SW = new StreamWriter(,);
    //serializer.Serialize(sw,obj,new XmlSerializerNamespaces());
}
