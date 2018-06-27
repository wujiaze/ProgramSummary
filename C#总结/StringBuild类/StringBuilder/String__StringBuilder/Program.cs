using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace String__StringBuilder
{
    class Program
    {
        /* 应用总结
         * 当需要对一个字符串进行大量的、未知的修改时，采用 StringBuilder
         * 优点：追加、替换字符，效率非常高
         * 缺点：删除、插入字符，效率不是很高
         *
         * 当不需要复杂操作时，采用 string 就可以了
         *
         * 一般而言，使用 StringBuilder 执行字符串的任何操作 ，使用  string 存储字符串和显示最终字符串
         */
        static void Main(string[] args)
        {
            /* 构造函数 */
            Console.WriteLine("-------------构造函数----------------");
            // 构造函数1  StringBuilder()
            // 在内部是使用了 StringBuilder(string.Empty, 0, 0, 16)

            // 构造函数2 StringBuilder(length);
            // 在内部是使用了 StringBuilder(string.Empty, 0, 0, length);

            // 构造函数3 public StringBuilder(string value)
            // 在内部是使用了  StringBuilder(value, 0, value.length, 16)               // 当 value.length > 16 时，内部会采用 value.length 的长度

            // 构造函数4 public StringBuilder(string value,int capacity)
            // 在内部是使用了 StringBuilder(value, 0, value.length, capacity)          // 当 value.length > capacity 时，内部会采用 value.length 的长度

            // 构造函数5 public StringBuilder(string value,int startIndex,int length,int capacity)
            // 内容 value 的子字符串 ，长度 length 和 capacity 较长的一个

            // 构造函数6 public StringBuilder(int capacity,int maxCapacity)
            // 没有内容，长度为 capacity ，最大长度为 maxCapacity   前5个构造函数的        maxCapacity 为 int.MaxValue

            /* 总结 构造函数本色纸上都是一样的，下面测试方法和属性*/

            /* 属性
            * Capacity
            * Chars[Int32]
            * Length
            * MaxCapacity
            */

            // 构造函数1
            StringBuilder builder = new StringBuilder();
            Console.WriteLine(builder.Length);                  // 实际内容的长度
            Console.WriteLine(builder.Capacity);                // builder 对象的当前内存容量
            Console.WriteLine(builder.MaxCapacity);             // builder 对象可扩大到的最大容量
            builder.Append("123456789abcdefghi");
            Console.WriteLine(builder.Length);                  // 实际内容的长度
            Console.WriteLine(builder.Capacity);                // builder 对象的当前内存容量(由于超过了16，容量翻倍)             
            Console.WriteLine(builder[0]);                      // 属性 Chars[Int32]，获取当前位置的字符

            // 构造函数2
            StringBuilder builderFix = new StringBuilder(16,32); // 这样一来，builderFix 对象的最大容量是 32 个字符

            /* 方法
             * Append
             * AppendFormat
             * AppendLine
             * Insert
             * Remove
             * Replace
             * Clear
             * CopyTo
             * ToString
             */

            /* 修改 StringBuilder 对象 */
            Console.WriteLine("-------------Append----------------");
            // Append 追加
            /*  方法分类
             *
             * 第一类
             *  Append(byte value)      Append(Decimal value)       Append(double value)        Append(short value)
             *  Append(int value)       Append(long value)          Append(sbyte value)         Append(float value)
             *  Append(ushort value)    Append(uint value)          Append(ulong value)
             * 内部都采用了 
             *   Append(value.ToString((IFormatProvider) CultureInfo.CurrentCulture))，直接附加到末尾                            关于 IFormatProvider ，会在另一篇解释
             *   就是将数字一个不变的转换成字符串形式，再添加到末尾
             * 第二类
             *  Append(bool value)      Append(object value)
             * 内部都采用了 Tostring(),直接附加到末尾
             *
             * 第三类
             *  Append(string value)    Append(string value, int startIndex, int count)
             *  Append(char value)      Append(char value, int repeatCount)
             *  直接附加到末尾（简单理解，实际 char 和string 还是有所不同的）
             *
             * 第四类
             *  Append(char[] value)    Append(char[] value, int startIndex, int charCount)
             *  内部都采用了  Append(char* value, int valueCount)
             *  第一个是value[0],value.Length   第二个是value[startIndex],charCount
             *  都是附加到末尾
             * 
             */
            StringBuilder appendBuilder = new StringBuilder();
            appendBuilder.Append(90.45);                        // 第一类
            appendBuilder.Append(true);                         // 第二类
            appendBuilder.Append(builder);                      // 第二类
            appendBuilder.Append('A',5);                        // 第三类
            appendBuilder.Append("你好");                       // 第三类
            appendBuilder.Append(new char[]{'e','f'});          // 第四类
            Console.WriteLine(appendBuilder.ToString());


            // AppendLine 追加行
            // 在内部使用了 Append(Environment.NewLine);
            Console.WriteLine("newLine 符号 需要通过断点来看 ： "+Environment.NewLine);      // 在当前平台是 \r\n
            StringBuilder appendLineBuilder = new StringBuilder();
            appendLineBuilder.AppendLine();                    
            appendLineBuilder.AppendLine("你好");
            Console.WriteLine(appendLineBuilder.ToString());

            // AppendFormat 追加特定格式的内容
            Console.WriteLine("-------------AppendFormat----------------");
            /*
             * 方法分类
             * AppendFormat(string format, object arg0)                                                         在内部使用了 AppendFormatHelper(null, format, new ParamsArray(arg0))
             * AppendFormat(string format, object arg0, object arg1)                                            在内部使用了 AppendFormatHelper(null, format, new ParamsArray(arg0,arg1))
             * AppendFormat(string format, object arg0, object arg1, object arg2)                               在内部使用了 AppendFormatHelper(null, format, new ParamsArray(arg0,arg1,arg2))
             * AppendFormat(string format, params object[] args)                                                在内部使用了 AppendFormatHelper(null, format, new ParamsArray(args))
             *
             * AppendFormat(IFormatProvider provider, string format, object arg0)                               在内部使用了 AppendFormatHelper(provider, format, new ParamsArray(arg0))
             * AppendFormat(IFormatProvider provider, string format, object arg0, object arg1)                  在内部使用了 AppendFormatHelper(provider, format, new ParamsArray(arg0,arg1))
             * AppendFormat(IFormatProvider provider, string format, object arg0, object arg1, object arg2)     在内部使用了 AppendFormatHelper(provider, format, new ParamsArray(arg0,arg1,arg2))
             * AppendFormat(IFormatProvider provider, string format, params object[] args)                      在内部使用了 AppendFormatHelper(provider, format, new ParamsArray(args))
             *
             * 总结：AppendFormatHelper(IFormatProvider provider, string format, ParamsArray args)
             */

            StringBuilder appendFormatBuilder = new StringBuilder();
            int ctr = 50;
            appendFormatBuilder.AppendFormat(null,"{0,12:X4}:{1:12}", ctr, ctr); // 通过某种格式，追加到末尾
            Console.WriteLine(appendFormatBuilder.ToString());

            
            // Insert 插入
            Console.WriteLine("-------------Insert----------------");
            /*
             * 方法分类
             * 第一类
             * Insert(int index, byte value)    Insert(int index, sbyte value)      Insert(int index, short value)      Insert(int index, int value)
             * Insert(int index, long value)    Insert(int index, float value)      Insert(int index, double value)     Insert(int index, Decimal value)
             * Insert(int index, ushort value)  Insert(int index, uint value)       Insert(int index, ulong value)
             * 内部统一使用了这个方法  Insert(int index, string value, int count) ==> Insert(index, value.ToString((IFormatProvider) CultureInfo.CurrentCulture), 1);
             *
             * 第二类
             * Insert(int index, string value)      Insert(int index, char value)   Insert(int index, char[] value)     Insert(int index, char[] value, int startIndex, int charCount)
             * 内部使用了这个方法  Insert(int index, char* value, int valueCount)
             *
             * 第三类
             * Insert(int index, bool value) Insert(int index, object value)
             * 内部统一使用了这个方法 Insert(int index, string value, int count)       Insert(index, value.ToString(), 1);
             */
            StringBuilder insertBuilder = new StringBuilder();
            insertBuilder.Append("123456789");
            insertBuilder.Insert(1, "abc");                              // 在下标为1的个、后面，插入字符串
            Console.WriteLine(insertBuilder.ToString());

            // Replace 替换
            Console.WriteLine("-------------Replace----------------");
            /*
             * 方法分类
             * Replace(string oldValue, string newValue)                            在内部使用了Replace(string oldValue, string newValue, 0 , this.Length)
             * Replace(string oldValue, string newValue, int startIndex, int count)
             *
             * Replace(char oldChar, char newChar)                                  在内部使用了Replace(oldChar, newChar, 0, this.Length)
             * Replace(char oldChar, char newChar, int startIndex, int count)
             */
            StringBuilder replaceBuilder = new StringBuilder();
            replaceBuilder.Append("123416781");
            replaceBuilder.Replace('1', 'A');                                       // 将 replaceBuilder 对象中所有的 1 替换为 A
            Console.WriteLine(replaceBuilder.ToString());
            replaceBuilder.Replace('A', 'p', 4, replaceBuilder.Length - 4);           // 从 replaceBuilder 对象下标 2 开始，将 replaceBuilder 对象中所有的 A 替换为 p
            Console.WriteLine(replaceBuilder.ToString());

            // Remove 删除
            Console.WriteLine("-------------Remove----------------");
            StringBuilder removeBuilder = new StringBuilder();
            removeBuilder.Append("123456789");
            removeBuilder.Remove(1, 2);
            Console.WriteLine(removeBuilder.ToString());



            /* 其他操作 */
            // Clear 清空对象    
            Console.WriteLine("-------------Clear----------------");            // 测试说明：一旦 StringBuilder 对象，内存扩大，即使 Clear 内容，不会改变内存空间
            StringBuilder clearBuilder = new StringBuilder();
            clearBuilder.Append("123456789abcdefhi");
            Console.WriteLine(clearBuilder.Length);                  
            Console.WriteLine(clearBuilder.Capacity);                
            Console.WriteLine(clearBuilder.MaxCapacity);
            clearBuilder.Clear();
            Console.WriteLine(clearBuilder.Length);
            Console.WriteLine(clearBuilder.Capacity);
            Console.WriteLine(clearBuilder.MaxCapacity);
            clearBuilder.Capacity = 16;                                         // 手动设置内存大小                    
            clearBuilder.Append("1");
            Console.WriteLine(clearBuilder.Length);
            Console.WriteLine(clearBuilder.Capacity);
            Console.WriteLine(clearBuilder.MaxCapacity);

            // CopyTo 拷贝对象
            Console.WriteLine("-------------CopyTo----------------");
            StringBuilder copyToBuilder = new StringBuilder();
            copyToBuilder.Append("123456");
            char[] target =new char[5];
            copyToBuilder.CopyTo(0, target,0,2);                        // 将 copyToBuilder 对象的字符串，拷贝到目标 字符数组
            foreach (char c in target)
            {
                Console.WriteLine(c);
            }
            // ToString 转换为字符串
            StringBuilder toStringBuilder = new StringBuilder();
            toStringBuilder.Append("123456");
            Console.WriteLine(toStringBuilder.ToString());          // 将 StringBuilder 的对象，全部转换成字符串
            Console.WriteLine(toStringBuilder.ToString(0,2));       // 将 StringBuilder 的对象的一部分转换成字符串

            // 字符串搜索等其他方法，转换成 string 进行操作

            Console.Read();
        }

    }
}
