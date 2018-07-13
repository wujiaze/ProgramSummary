using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public enum MyEnum
{
    my,
    your,
    his
}

public struct MyStruct
{
    public int x;
    public int y;
    public int z;
    public MyStruct(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

public class Class1
{
    public int x;
    public int y;
    public int z;
    public Class1(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Class1()
    {
        
    }
}

public interface IInterface
{

}

public class ClassFace: IInterface
{
    [XmlIgnore]
    public byte face;
    public MyClass my;
}

public class MyClass: ClassFace
{
    public readonly MyEnum enu1;
    private sbyte sby;

    public sbyte Sby {
        get { return sby; }
        set { sby = value; }
    }
    [XmlAttribute]
    public byte by;
    [XmlAttribute]
    public char ch;
    public short sh;
    public ushort ush;
    public uint uin;
    public long lo;
    public ulong ulo;
    public int num;
    public bool isbo;
    public MyStruct ms;
    public object obj;
    public string str;
    public Class1 c1;
    //public IInterface ii;
    public ClassFace cf;
    //public Action ac;
    public int[] arrint;
    public Class1[] ArrClass1s;
    public ArrayList arrlist;
    public List<int> list;
    public List<ArrayList> arrl;
    //public Hashtable has;
    //public Dictionary<float, byte> dic;
    //public Queue arrqueue;
    //public Queue<double> queue;
    //public Stack arrsta;
    //public Stack<decimal> sta;

    //public ArrayList[] a;
   
    public MyClass(MyEnum enu, sbyte sby, byte @by, char ch,
        short sh, ushort ush, uint uin, long lo, ulong ulo,
        int num, bool isbo, MyStruct ms, object obj, string str,
        Class1 c1, IInterface ii, Action ac, int[] arrint, Class1[] arrClass1S,
        ArrayList arrlist, List<int> list, Hashtable has, Dictionary<float, byte> dic,
        Queue arrqueue, Queue<double> queue, Stack arrsta, Stack<decimal> sta, List<ArrayList> arrl, ArrayList[] a, ClassFace cf, sbyte Sby)
    {
        this.enu1 = enu;
        this.sby = sby;
        this.@by = @by;
        this.ch = ch;
        this.sh = sh;
        this.ush = ush;
        this.uin = uin;
        this.lo = lo;
        this.ulo = ulo;
        this.num = num;
        this.isbo = isbo;
        this.ms = ms;
        this.obj = obj;
        this.str = str;
        this.c1 = c1;
        //this.ii = ii;
        //this.ac = ac;
        this.arrint = arrint;
        ArrClass1s = arrClass1S;
        this.arrlist = arrlist;
        this.list = list;
        //this.has = has;
        //this.dic = dic;
        //this.arrqueue = arrqueue;
        //this.queue = queue;
        //this.arrsta = arrsta;
        //this.sta = sta;
        this.arrl = arrl;
        //this.a = a;
        this.cf = cf;
        this.Sby = Sby;
    }
    public MyClass() { }

    public void Print()
    {
        Console.WriteLine("this.enu  " + enu1);
        Console.WriteLine("this.sby  " + this.sby);
        Console.WriteLine("this.@by  " + this.@by);
        Console.WriteLine("this.ch  " + this.ch);
        Console.WriteLine("this.sh  " + this.sh);
        Console.WriteLine("this.ush  " + this.ush);
        Console.WriteLine("this.uin  " + this.uin);
        Console.WriteLine("this.lo  " + this.lo);
        Console.WriteLine("this.ulo  " + this.ulo);
        Console.WriteLine("this.num  " + this.num);
        Console.WriteLine("this.isbo  " + this.isbo);
        Console.WriteLine("this.ms  " + this.ms);
        Console.WriteLine("this.obj  " + this.obj);
        Console.WriteLine("this.str  " + this.str);
        Console.WriteLine("this.c1  " + this.c1);
        Console.WriteLine("this.arrint  " + this.arrint.Length + "  /  "+ this.arrint[0]);
        Console.WriteLine("this.ArrClass1s  " + this.ArrClass1s + "   /   "+ this.ArrClass1s[1]);
        Console.WriteLine("this.arrlist  " + this.arrlist + "   /   " + this.arrlist[0]);
        Console.WriteLine("this.list  " + this.list + "   /   " + this.list[0]);
        Console.WriteLine("this.arrl  " + this.arrl + "   /   " + this.arrl[0][0]);
        //Console.WriteLine("this.Sby " + this.Sby);
    }
}

