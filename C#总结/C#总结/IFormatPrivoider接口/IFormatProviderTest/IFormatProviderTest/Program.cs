using System;
using System.Globalization;

namespace IFormatProviderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            /* IFormattable */
            Person person = new Person() {FirstName = "Tom", LastName = "Jack"};
            Console.WriteLine(person.ToString());
            Console.WriteLine(person.ToString("F"));                            // 需要继承这个 IFormattable 接口 ,才能自定义输出格式
            Console.WriteLine($"{person:L}");       
            Console.WriteLine(String.Format("{0:A}", person));                  // 内部使用了 IFormatProvider 对象         

            /* IFormatProvider：提供格式化对象接口 
             *
             * NET有三个定义定义好的
             * CultureInfo ：该类表示特定区域性（中国、欧洲、拉丁区。。。）
             * NumberFormatInfo ：该类提供的信息用于格式化数字
             * DateTimeFormatInfo：该类提供的信息用于格式化时间和日期
             */

            // CultureInfo
            int var = 1234567890;
            Console.WriteLine(var.ToString());
            Console.WriteLine(var.ToString("N"));                           // 由于在中国，所以默认的就是中文区域码
            Console.WriteLine(var.ToString("N", new CultureInfo("zh-CN"))); // 预定义的中文
            Console.WriteLine(CultureInfo.CurrentCulture);
            // 方法1： .NET46
            Console.WriteLine(var.ToString("N", new CultureInfo("de-DE")));  // 预定义的德语
            // 方法2：
            CultureInfo.CurrentCulture = new CultureInfo("fr-FR");           // 改变当前线程的区域性
            Console.WriteLine(var.ToString("N"));                            // 法国的符号不认识..  TODO 以后做到了全球化再说


            CultureInfo.CurrentCulture = new CultureInfo("zh-CN");
            DateTime date = new DateTime(2018,6,26);
            Console.WriteLine(date.ToString());
            Console.WriteLine(date.ToString("D"));
            Console.WriteLine(date.ToString("D",new CultureInfo("es-ES")));   // 预定义的西班牙语

            // NumberFormatInfo
            int num = 987654321;
            NumberFormatInfo info = new NumberFormatInfo();
            info.CurrencyDecimalDigits = 20;
            Console.WriteLine(string.Format(info,"{0:C}",num));
            Console.WriteLine(string.Format(info, "{0:C2}", num));

            // 
            Console.Read();
        }

    }
}
