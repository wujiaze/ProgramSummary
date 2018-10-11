using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFormatProviderTest
{
    // 测试 IFormattable 接口
    public class Person:IFormattable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString() => FirstName + " " + LastName;
        public string ToString(string format) => ToString(format,null);
        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case "A":
                    return ToString();
                    break;
                case "F":
                    return FirstName ;
                    break;
                case "L":
                    return LastName;
                    break;
                default:
                    throw new FormatException($"无效的字符串格式{format}");
                    break;
            }
        }
        
    }
}
