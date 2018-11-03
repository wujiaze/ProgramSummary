/*
 *      编码的理解
 *
 *  主题1： 编程时手动输入的字符串
 *            首先，.Net 表示为UTF16 的编码，但是不确定是小端还是大端（因为内部是通过什么字节序列转换为字符串这个并不重要），我们可以根据自己的需要将字符串转焊接为需要的字节序列
 *            因为，字符串可以通过 Encoding 对象，进行编码获得想要字节序列
 *            然后，方法1(推荐)：字节序列可以通过 Decoding 对象，进行解码得到字符串（同一个方法，或者不同方法中）
 *                  方法2：当在同一个方法中对字符串进行编码解码时，可以在使用Encoding进行编码之后，直接使用Encoding进行解码
 *
 *     
 *  主题2     文本读取、保存
 *
 *          一、 ANSI 系列
 *            ANSI 系列的编码方式(ASCII,GBK)等等，在写入文档时，就是正常的字节序列，没有其他附加
 *
 *          二、 UTF8 系列
 *            UTF8 采用 new UTF8Encoding(false) 是不带BOM 的UTF编码
 *            使用 new UTF8Encoding(true) 或者  Encoding.UTF8 获取时,是带有BOM的，但是需要自己手动添加到byte数组开头  GetPreamble() 返回的BOM头(只有true时，这个方法才有值)
 *
 *
 *         三、UTF16 系列
 *            Encoding.BigEndianUnicode 相当于 new UnicodeEncoding(true, true);
 *            Encoding.Unicode          相当于 new UnicodeEncoding(false, true);
 *            后面一个true表示：是否对 GetPreamble() 返回的BOM 赋值，对于UTF16来说，必须赋值
 *            所以，UTF16必须手动对 byte数组 添加 BOM 头
 *
 *
 *         以上三种， 
 *            写入均可以采用
 *                  文件流 + 二进制写入，字节数组可以由 Encoding.GetBytes 获得
 *
 *            读取均可以采用
 *                  文件流 + 二进制读取
 *
 *                  注意点： 读取的时候，首先需要判断读到的是什么格式，如果带有BOM头(UTF8,UTF16)则需要手动将BOM去掉，然后再进行解码
 *
 *                  解码方式：
 *                  方法一、字符串由 Encoding 对应的 Decoder 对象的 GetChars 方法来获取(推荐：文件读取，网络传输)
 *                  方法二、当然，在同一个方法中就进行编码和解码，可以直接采用 Encoding 的GetString 方法
 *
 *       写入采用
 *          File.WriteAllText
 *              一、 ANSI 系列   与上文一致
 *              二、 UTF8 系列   自动添加BOM头
 *              三、UTF16 系列   根据UTF16类型自动添加BOM头
 *       读取采用
 *          File.ReadAllText(Encoding)
 *              不管 Encoding 是什么，都是根据实际的编码读取的
 *              读到的就是 正常的字符串 ，字符串是不带字节顺序标记的，可以做和手动打出来的字符串一样的功能
 *
 *    
 *
 *
 *
 *  主题3     网络传输
 *
 *
 *  特别注意：
 *          Windows中使用记事本,执行过“保存”操作的UTF8编码的文档时，才会添加BOM开头，仅仅写入而没有手动保存过的话，则写入什么就是什么，不会自动BOM头
 *          英文：保存为ANSI时，可以理解为ASCII 或者 UTF8不带Bom，若是手动保存为UTF8,则会添加Bom头
 *          中文：保存为ANSI时，就是使用了Default
 *          
 */
using System;
using System.IO;
using System.Text;



namespace EncodingTest
{
    public enum MyEncodingType
    {
        ASCII_UTF8withoutBom,
        ANSI_Default,
        Utf8withBom,
        Utf8withoutBom,
        Utf16Big,
        Utf16Little
    }
    class Program
    {
        static void Main(string[] args)
        {
            string str = "你好";

            // 方式一
            string path = @"D:\Desktop\e.txt";
            //Encoding enc = Encoding.UTF8;
            //byte[] bytes = enc.GetBytes(str);
            //byte[] bb = new byte[3 + bytes.Length];
            //enc.GetPreamble().CopyTo(bb, 0);
            //bytes.CopyTo(bb, 3);
            //using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            //{
            //    using (BinaryWriter wr = new BinaryWriter(fs)) // 这里选择任何编码都一样，因为已经根据 指定编码 转换成了 byte数组, 这个就是直接二进制写入
            //    {
            //        wr.Write(bb);
            //    }
            //}
            //// 存入文件，在读取出来，读取bytes
            //Decoder de = enc.GetDecoder();
            //using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            //{
            //    using (BinaryReader rd = new BinaryReader(fs, enc)) // 这里选择任何编码都一样，因为会根据实际的编码读取
            //    {
            //        byte[] readBytes = rd.ReadBytes((int)rd.BaseStream.Length);
            //        // 方法一
            //        //Console.WriteLine(enc.GetString(readBytes));
            //        // 方法二
            //        char[] chars = new char[de.GetCharCount(readBytes, 0, readBytes.Length)];
            //        de.GetChars(readBytes, 0, readBytes.Length, chars, 0);
            //        Console.WriteLine(new string(chars));
            //    }
            //}
            //GetEncode(path);


            // 方式二 
            //string path2 = @"D:\Desktop\eb.txt";
            //File.WriteAllText(path2, str, Encoding.BigEndianUnicode);
            //string str1 = File.ReadAllText(path2, Encoding.UTF8);
            //byte[] bytes = Encoding.UTF8.GetBytes(str1);
            //Console.WriteLine(str1);
            //GetEncode(path2);


            //SwitchEncoding(path, new UTF8Encoding());
            SwitchEncoding(path, Encoding.UTF8);
            //SwitchEncoding(path, Encoding.BigEndianUnicode);
            //SwitchEncoding(path, Encoding.Unicode);
            //SwitchEncoding(path, Encoding.Default);

            //ConvertEncoding(path, new UTF8Encoding());
            //ConvertEncoding(path, Encoding.UTF8);
            //ConvertEncoding(path, Encoding.BigEndianUnicode);
            //ConvertEncoding(path, Encoding.Unicode);
            //ConvertEncoding(path, Encoding.Default);
            Console.WriteLine(GetEncode(path));
            Console.Read();
        }

        /// <summary>
        /// 对纯粹的字符串进行转码，
        ///     即与BOM头无关
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="srcEncoding"></param>
        /// <param name="targetEncoding"></param>
        private static string ConvertEncoding(string txt, Encoding srcEncoding, Encoding targetEncoding)
        {
            byte[] srcBytes = srcEncoding.GetBytes(txt);
            byte[] tarBytes = Encoding.Convert(srcEncoding, targetEncoding, srcBytes);
            return targetEncoding.GetString(tarBytes);
        }

        /// <summary>
        /// 转化文本的编码
        ///     不需要中间的string字符串
        /// </summary>
        /// <param name="path"></param>
        /// <param name="targetEncoding"></param>
        private static void ConvertEncoding(string path, Encoding targetEncoding)
        {
            // 获取编码
            MyEncodingType myEncodingType = GetEncode(path);
            Encoding srcEncoding = null;
            byte[] tarBytes;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                // 获取原文本
                using (BinaryReader rd = new BinaryReader(fs, Encoding.Default)) // 这里的编码并没有什么作用，只是为了第三个参数，读取后不关闭文件流
                {
                    switch (myEncodingType)
                    {
                        case MyEncodingType.ASCII_UTF8withoutBom:
                            srcEncoding = new UTF8Encoding(false);
                            break;
                        case MyEncodingType.ANSI_Default:
                            srcEncoding = Encoding.Default;
                            break;
                        case MyEncodingType.Utf8withBom:
                            if (targetEncoding == Encoding.ASCII)
                                throw new Exception("当前编码，无法转为ASCII码");
                            srcEncoding = Encoding.UTF8;
                            rd.ReadBytes(3);
                            break;
                        case MyEncodingType.Utf8withoutBom:
                            if (targetEncoding == Encoding.ASCII)
                                throw new Exception("当前编码，无法转为ASCII码");
                            srcEncoding = new UTF8Encoding(false);
                            break;
                        case MyEncodingType.Utf16Big:
                            if (targetEncoding == Encoding.ASCII)
                                throw new Exception("当前编码，无法转为ASCII码");
                            srcEncoding = Encoding.BigEndianUnicode;
                            rd.ReadBytes(2);
                            break;
                        case MyEncodingType.Utf16Little:
                            if (targetEncoding == Encoding.ASCII)
                                throw new Exception("当前编码，无法转为ASCII码");
                            srcEncoding = Encoding.Unicode;
                            rd.ReadBytes(2);
                            break;

                    }
                    byte[] srcBytes = rd.ReadBytes((int)rd.BaseStream.Length);
                    tarBytes = Encoding.Convert(srcEncoding, targetEncoding, srcBytes);
                }
            }

            //使用新的编码写入文本
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter writer = new BinaryWriter(fs, targetEncoding))
                {
                    // 转换成目标编码的字节数组
                    byte[] preamble = targetEncoding.GetPreamble();
                    //若有BOM头，则添上Bom头
                    if (preamble.Length > 0)
                    {
                        byte[] finBytes = new byte[tarBytes.Length + preamble.Length];
                        Buffer.BlockCopy(preamble, 0, finBytes, 0, preamble.Length);
                        Buffer.BlockCopy(tarBytes, 0, finBytes, preamble.Length, tarBytes.Length);
                        tarBytes = finBytes;
                    }
                    writer.Write(tarBytes);
                }
            }
        }

        /// <summary>
        /// 转化文本的编码
        ///     获取了中间的string字符串
        /// </summary>
        /// <param name="path"></param>
        /// <param name="targetEncoding"></param>
        private static void SwitchEncoding(string path, Encoding targetEncoding)
        {
            // 获取编码
            MyEncodingType myEncodingType = GetEncode(path);
            // 获取源文本
            string sourceStr = String.Empty;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                // 获取原文本
                using (BinaryReader rd = new BinaryReader(fs, Encoding.UTF8)) // 这里的编码并没有什么作用，只是为了第三个参数，读取后不关闭文件流
                {
                    // 获取解码器
                    Decoder de = null;
                    switch (myEncodingType)
                    {
                        case MyEncodingType.ASCII_UTF8withoutBom: // 源文本全为英文，也采用UTF8来解码
                            de = new UTF8Encoding(false).GetDecoder();
                            break;
                        case MyEncodingType.ANSI_Default:
                            if (targetEncoding == Encoding.ASCII)
                                throw new Exception("当前编码，无法转为ASCII码");
                            de = Encoding.Default.GetDecoder();
                            break;
                        case MyEncodingType.Utf8withBom:
                            if (targetEncoding == Encoding.ASCII)
                                throw new Exception("当前编码，无法转为ASCII码");
                            de = new UTF8Encoding(true).GetDecoder();
                            rd.ReadBytes(3);
                            break;
                        case MyEncodingType.Utf8withoutBom:
                            if (targetEncoding == Encoding.ASCII)
                                throw new Exception("当前编码，无法转为ASCII码");
                            de = new UTF8Encoding(false).GetDecoder();
                            break;
                        case MyEncodingType.Utf16Big:
                            if (targetEncoding == Encoding.ASCII)
                                throw new Exception("当前编码，无法转为ASCII码");
                            de = Encoding.BigEndianUnicode.GetDecoder();
                            rd.ReadBytes(2);
                            break;
                        case MyEncodingType.Utf16Little:
                            if (targetEncoding == Encoding.ASCII)
                                throw new Exception("当前编码，无法转为ASCII码");
                            de = Encoding.Unicode.GetDecoder();
                            rd.ReadBytes(2);
                            break;

                        default:
                            break;
                    }
                    byte[] readBytes = rd.ReadBytes((int)rd.BaseStream.Length); // 此时 rd.BaseStream.Length 大于剩下的 byte数组数量，但是没关系，只会读取剩下所有的
                    char[] chars = new char[de.GetCharCount(readBytes, 0, readBytes.Length)];
                    de.GetChars(readBytes, 0, readBytes.Length, chars, 0);
                    sourceStr = new string(chars);
                }
            }

            //使用新的编码写入文本
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter writer = new BinaryWriter(fs, targetEncoding))
                {
                    // 转换成目标编码的字节数组
                    byte[] targetBytes = targetEncoding.GetBytes(sourceStr);
                    byte[] preamble = targetEncoding.GetPreamble();
                    //若有BOM头，则添上Bom头
                    if (preamble.Length > 0)
                    {
                        byte[] finBytes = new byte[targetBytes.Length + preamble.Length];
                        Buffer.BlockCopy(preamble, 0, finBytes, 0, preamble.Length);
                        Buffer.BlockCopy(targetBytes, 0, finBytes, preamble.Length, targetBytes.Length);
                        targetBytes = finBytes;
                    }
                    writer.Write(targetBytes);
                }
            }
        }



        /// <summary>
        /// 获取源文件的编码格式
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static MyEncodingType GetEncode(string path)
        {
            MyEncodingType type = MyEncodingType.ANSI_Default;
            // 获取源文本的编码方式
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                BinaryReader reader = new BinaryReader(fs);     //这里选择任何一种编码都没关系，都是根据实际文本编码读取二进制
                byte[] bytes = reader.ReadBytes((int)fs.Length);
                if (IsASCII(bytes))
                {
                    type = MyEncodingType.ASCII_UTF8withoutBom; // 将ACSII 都看作UTF8 或 ASCII 都可以
                }
                else if (IsUtf8WithoutBom(bytes))
                {
                    if (IsUtf8WithBom(bytes))
                    {
                        type = MyEncodingType.Utf8withBom;
                    }
                    else
                    {
                        type = MyEncodingType.Utf8withoutBom;
                    }
                }
                else if (IsUtf16Big(bytes))
                {
                    type = MyEncodingType.Utf16Big;
                }
                else if (IsUtf16Little(bytes))
                {
                    type = MyEncodingType.Utf16Little;
                }
                else
                {
                    Console.WriteLine("当前编码方式：代码页：" + Encoding.Default.CodePage);
                    type = MyEncodingType.ANSI_Default;
                }
            }
            return type;
        }





        /// <summary>
        /// 判断是否 为带BOM的UTF-8
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static bool IsUtf8WithBom(byte[] bytes)
        {
            if (bytes.Length < 3)
            {
                return false;
            }
            byte[] UTF8 = new byte[] { 0xEF, 0xBB, 0xBF }; //带BOM  只会在字节的最前面
            if (bytes[0] == UTF8[0] && bytes[1] == UTF8[1] && bytes[2] == UTF8[2])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否 为不带BOM的UTF-8
        /// </summary>
        private static bool IsUtf8WithoutBom(byte[] bytes)
        {
            bool isUTF8 = false;
            bool isCouldUTF8 = false; // 用于标记是否满足UTF-8首字节的条件
            int oneNum = 0;     // 1 的个数
            for (int i = 0; i < bytes.Length; i++)
            {
                byte tempbyte = bytes[i];
                if (isCouldUTF8 == false) // 在不满足首字节的条件下，先判断首字节的条件
                {
                    // 判断是否满足UTF8字节首位的条件
                    if (tempbyte >= 0x80)   // 当前字节的大于等于 1000 0000，即超过ASCII 的部分，判断是否符合UTF-8的开头 110x xxxx,1110 xxxx,1111 0xxx
                    {
                        while (((tempbyte <<= 1) & 0X80) != 0)   //判断 首位1 后面连续跟了几个 1
                        {
                            oneNum++;   // 之后需要判断后面的几个byte字节是否满足UTF8条件
                        }
                        if (oneNum < 1 || oneNum > 6)     // 首位1后面没有跟1  或者 首位1后面跟了7个1，超过UTF-8的编码空间 ，都说明不是UTF-8
                        {
                            isUTF8 = false;
                            return isUTF8;
                        }
                        else
                        {
                            isCouldUTF8 = true;
                        }
                    }
                }
                else
                {
                    // 判断在满足首字节的前提下，是否满足后面的条件： 后面必须跟着 oneNum 个 10xx xxxx 形式的字节
                    if (oneNum > 0)
                    {
                        if ((tempbyte & 0xC0) == 0x80)
                        {
                            oneNum--;
                            isCouldUTF8 = true; //依然可能是UTF8
                            if (oneNum == 0)
                            {
                                // 满足了首字节条件，也满足了后面字节的条件
                                isUTF8 = true;
                                isCouldUTF8 = false; // 继续判断后一个字节组 是否满足 UTF-8的条件
                                                     //return isUTF8; // 满足一个之后，一般就够了，但是为了严密可以继续循环判断
                            }
                        }
                        else
                        {
                            isUTF8 = false;
                            return isUTF8;
                        }
                    }
                }
            }
            return isUTF8;
        }

        /// <summary>
        /// 判断是否为 UTF-16 高尾端序 或者 大端序
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static bool IsUtf16Big(byte[] bytes)
        {
            if (bytes.Length < 2)
            {
                return false;
            }
            byte[] Unicode16Big = new byte[] { 0xFE, 0xFF };
            if (bytes[0] == Unicode16Big[0] && bytes[1] == Unicode16Big[1])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否为 UTF-16 低尾端序 或者 小端序
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static bool IsUtf16Little(byte[] bytes)
        {
            if (bytes.Length < 2)
            {
                return false;
            }
            byte[] Unicode16Little = new byte[] { 0xFF, 0xFE };
            if (bytes[0] == Unicode16Little[0] && bytes[1] == Unicode16Little[1])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 每一个字节都小于 0x80 则判定为 ASCII
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static bool IsASCII(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] >= 0x80)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
