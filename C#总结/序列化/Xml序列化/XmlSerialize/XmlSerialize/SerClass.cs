//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Xml.Serialization;

//public struct MyStruct
//{
//    int myint;
//}
//public enum MyEnum
//{
//    myenum
//}
//public class class1
//{
//    int cla1;
//    public class1() { }
//}
//namespace XmlSerialize
//{
//    [Serializable]//用于网络传输，1.什么都不加测试，加上[Serializable]测试，加上[XmlAttribute("AreaName")] 在测试  todo ,测试xml文档添加注释
//    public class SerClass
//    {
//        // 所有数据类型
//        // 值类型
//        [XmlAttribute("AreaName")]
//        public sbyte pubsby;
//        public byte pubbyt;
//        public short pubsho;
//        public ushort pubush;
//        public float pubfloat;
//        public double pubdou;
//        public int pubint;
//        public uint pubuin;
//        public char pubcha;
//        public long publon;
//        public ulong pubulo;
//        public decimal pubdec;
//        public bool pubboo;
//        public MyStruct pubms;
//        public MyEnum pubme;
//        // 引用类型
//        public object pubobj;
//        public string pubstr;
//        // dynamic 就相当于 var 一般不需要用
//        //public dynamic pubdyn;
//        public class1 pubcla;
//        // 数组
//        public int[] pubarrint;
//        // 集合
//        public ArrayList pubarr;
//        public List<int> publis;
//        // 属性 
//        public int Pubint
//        {
//            get { return pubint; }
//            set { pubint = value; }
//        }
//        public SerClass() {
          
//        }
//    }
//}
///* xml 序列化的要求
// * 1、非静态（.net是基于对象序列化的），public
// * 2、类必须带有无参构造函数
// */
///* xml 无法序列化的情况
// * 1、不支持 实现IDictionary接口的类：HashTable  Dictionary
// * 2、不支持 栈和队列 Stack Queue
// * 3、不支持 多维数组
// * 4、不支持 接口和委托
// */
