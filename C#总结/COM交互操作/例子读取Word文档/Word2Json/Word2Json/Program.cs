using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using LitJson;
using Word;

namespace Word2Json
{
    class Program
    {
        static void Main(string[] args)
        {
            string jsonPath = @"D:\Desktop\常规项目\魔墙工程\嘉兴平湖2018.9.30\Detail.json";
            string wordPath = @"D:\Desktop\常规项目\魔墙工程\嘉兴平湖2018.9.30\资料\互动魔镜墙脚本8.13.docx";//文档的路径
            Document doc = ReadWord(wordPath);
            List<Detail> details = Word2JsonClass(doc, 1, 101);
            Write2Json(details, jsonPath);
            CloseWord(doc); // 关闭文档
            Console.WriteLine("完成!之后进入json网站将格式转换成可读的格式");
            Console.Read();
            
        }

        /// <summary>
        /// 读取Word文档
        /// </summary>
        /// <returns></returns>
        public static Document ReadWord(string path)
        {
            Application app = new Application();
            Document doc = null;
            object missingValue = Type.Missing;
            object objpath = path;
            try
            {
                doc = app.Documents.Open(ref objpath, ref missingValue, ref missingValue, ref missingValue,
                    ref missingValue, ref missingValue, ref missingValue,
                    ref missingValue, ref missingValue, ref missingValue,
                    ref missingValue, ref missingValue, ref missingValue,
                    ref missingValue, ref missingValue, ref missingValue);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return doc;
        }
        /// <summary>
        /// 关闭文档
        /// </summary>
        /// <param name="doc"></param>
        public static void CloseWord(Document doc)
        {
            try
            {
                object missingValue = Type.Missing;
                doc.Close(ref missingValue, ref missingValue, ref missingValue);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 将word文档写入符合json类的list中
        ///     每次不同的Detail类都需要重写这个方法
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="startIndex">word文档的开头序号(若文档的开头大于1，都会转换成从0开始)</param>
        /// <param name="endIndex">word文档的结尾序号</param>
        /// <returns></returns>
        public static List<Detail> Word2JsonClass(Document doc, int startIndex, int endIndex)
        {
            List<Detail> details = new List<Detail>();
            int realyStartIndex = 0;                  // 真正的开始下标
            int count = endIndex - startIndex + 1;    // 根据具体文档的数量
            int currentIndex = realyStartIndex;
            // 分隔符：不同的word文档，分隔符都是不确定的，可能需要修改
            char[] ch1 = { '.' };
            char[] ch2 = { '。' };
            foreach (Paragraph paragraph in doc.Paragraphs)
            {
                string str = paragraph.Range.Text;
                for (int i = currentIndex; i <= count; i++)
                {
                    if (str.StartsWith(i.ToString()))
                    {
                        currentIndex++;
                        Detail detail = new Detail();
                        details.Add(detail);

                        // 以下的根据不同文档需要修改
                        string[] temp = str.Split(ch1, 2);
                        detail.Id = int.Parse(temp[0]) - startIndex; //将Id从零开始
                        string[] temp2 = temp[1].Split(ch2, 2);
                        detail.Name = temp2[0];
                        detail.Introduction = temp2[1].Trim();
                        break;
                    }
                }
            }
            return details;
        }

        /// <summary>
        /// 将Json写入文档
        /// </summary>
        /// <param name="objJson">写入的内容</param>
        /// <param name="jsonpath">json路径</param>
        public static void Write2Json(object objJson, string jsonpath)
        {
            string jsonstr = JsonMapper.ToJson(objJson);
            // 将中文从Unicode 转换成 UTF-8
            jsonstr = UnicodeToString(jsonstr);         // 这一步也可以不进行（json网站可以完成）
            File.WriteAllText(jsonpath, jsonstr);
        }

        /// <summary>
        /// unicode编码转换为中文
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string UnicodeToString(string text)
        {
            MatchCollection mc = Regex.Matches(text, "\\\\u([\\w]{4})");
            if (mc != null && mc.Count > 0)
            {
                List<Match> matchs = new List<Match>(); // 保存已经转换好的就不用第二次转换了，节省性能
                foreach (Match m2 in mc)
                {
                    if (matchs.Contains(m2))
                        break;
                    matchs.Add(m2);
                    string v = m2.Value;
                    string word = v.Substring(2);
                    byte[] codes = new byte[2];
                    int code = Convert.ToInt32(word.Substring(0, 2), 16);
                    int code2 = Convert.ToInt32(word.Substring(2, 2), 16);
                    codes[0] = (byte)code2;
                    codes[1] = (byte)code;
                    text = text.Replace(v, Encoding.Unicode.GetString(codes)); // 从Unicode编码中提取字符串,.NET内置的会把中文字符串转换成UTF-8
                }
            }
            return text;
        }
    }

}
