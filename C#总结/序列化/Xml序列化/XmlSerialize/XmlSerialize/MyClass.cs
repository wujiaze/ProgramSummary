using System;
using System.Collections;
using System.Collections.Generic;


public enum MyEnum
{
    my,
    your,
    his
}

public struct MyStruct
{

}

public class Class1
{

}

public interface IInterface
{

}

public class ClassFace: IInterface
{
    
}

public class MyClass
{
    public MyEnum enu;
    public sbyte sby;
    public byte by;
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
        Queue arrqueue, Queue<double> queue, Stack arrsta, Stack<decimal> sta, List<ArrayList> arrl, ArrayList[] a)
    {
        this.enu = enu;
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
    }
    public MyClass() { }

    public void Print()
    {

    }
}

