using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 特性
{
    // 特性类的后缀以Attribute结尾
    // 继承System.Attribute
    // 一般特性类不需要被继承 sealed
    // 一般情况下，特性类用来表示目标结构的一些状态（即只需要定义字段、属性）
    
    [AttributeUsage(AttributeTargets.Class,Inherited = false,AllowMultiple = true)]// 表示我们自定义的特性用在哪些目标上
    public sealed class TestAttribute :Attribute
    {
        public string Description { get;}
        public string Verscription { get; set; }
        public int ID { get; set; }

        // 构造函数
        public TestAttribute(string str)
        {
            Description = str;
        }
    }
}
