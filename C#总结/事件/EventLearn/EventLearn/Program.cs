using System;
/*
 *      事件和委托的区别
 *          事件就是，针对某一个行为，封装了一个私有委托，这个委托只用于这个行为
 *          委托是类型
 *          事件是成员，事件没有委托成员灵活，因为他是针对具体内容
 *          委托对象和事件的使用方法是一样的
 */
/*
 *  EventHandler : C#自带的委托
 *      目的：将委托统一起来使用
 *      参数 object sender：触发事件的对象
 *           EventArgs e  ： 传递的数据，EventArgs 内部只有一个无参构造函数，所以一般用于不需要参数的方法
 *                                      当需要传递参数或保存参数时，需要自己继承  EventArgs 类进行扩展
 */
namespace EventLearn
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 原生用法 */
            MyClass m= new MyClass();
            MyClass2 m2 = new MyClass2();
            m.DoCount();

            /* 扩展用法 */
            MyExtendClass me = new MyExtendClass();
            MyExtendClass2 me2 = new MyExtendClass2();
            me.DoCount();

            Console.Read();
        }
    }
    /* 原生使用 */
    public class MyClass
    {
        public static event EventHandler Counted;

        public void DoCount()
        {
            Counted?.Invoke(this, null); // this:表示调用本方法的实例对象
        }
    }

    public class MyClass2
    {
        public MyClass2()
        {
            MyClass.Counted += Count;
        }
        private void Count(object source, EventArgs e)
        {
            Console.WriteLine("11");
        }
    }
    /* 扩展使用 */

    public class MyArgEventsArgs : EventArgs    // 后缀是一种通俗写法
    {
        public int Value;
    }

    public class MyExtendClass
    {
        public static event EventHandler Counted;
        public static event EventHandler<MyArgEventsArgs> Counted2; // 使用泛型好处：不需要强转类型了
        public void DoCount()
        {
            MyArgEventsArgs args = new MyArgEventsArgs();
            args.Value = 9;
            Counted?.Invoke(this, args); // this:表示调用本方法的实例对象
            Counted2?.Invoke(this,args);
        }
    }

    public class MyExtendClass2
    {
        public MyExtendClass2()
        {
            MyExtendClass.Counted += Count;
            MyExtendClass.Counted2 += Count2;
        }

        private void Count2(object sender, MyArgEventsArgs e)
        {
            Console.WriteLine(e.Value);
        }

        private void Count(object source, EventArgs e)
        {
            MyArgEventsArgs args = (MyArgEventsArgs) e;
            Console.WriteLine(args.Value);
        }
    }
}
