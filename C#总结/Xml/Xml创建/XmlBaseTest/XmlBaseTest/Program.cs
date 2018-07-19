using System;
using System.Xml;

namespace XmlBaseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 采用文档对象模型DOM 方式*/



            /* 采用流方式 */
            /*
             *  采用工厂方法，创建 XmlReader 的三种子类的实例
             *  第一种  XmlDictionaryReader
             *  第二种  XmlNodeReader
             *  第三种  XmlTextReader
             *  Create(String inputUri, XmlReaderSettings settings, XmlParserContext inputContext)
             *  Create(       inputUri,                   settings,                  null        )
             *  Create(       inputUri,                   null    ,                  null        )
             *
             *  第四种  XmlValidatingReader
             */
            string xmlFilePath = "D:\\Desktop\\xml1.xml";       //最好是xml扩展名
            // 采用第一种
            using (XmlReader reader = XmlReader.Create(xmlFilePath))// 打算弄懂一种即可
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Text)        // 获取全部的文本节点
                        Console.WriteLine(reader.Value);
                }
            }
            Console.Read();
        }

        private void ReadXmlToInstanceData()
        {
           
        }
    }
}
