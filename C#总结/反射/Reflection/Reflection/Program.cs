
using System;
using System.Reflection;
using System.Runtime.Remoting;

namespace Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 查询一些基本常用的信息
            // 首先获取 类的Type对象 ：方法1和方法2 得到的都是同一个对象(RefClass类型的Type类型信息，不包含特定的实例对象信息)
            // 方法1
            Type type = typeof(RefClass);
            // 方法2
            RefClass rf1 = new RefClass();
            Type type2 = rf1.GetType();
            // 通过反射，设置类实例的各种信息
            FieldInfo fi = type.GetField("fieldPublic");
            Console.WriteLine(fi.GetValue(rf1));
            fi.SetValue(rf1, 10);
            Console.WriteLine(fi.GetValue(rf1));


            #endregion  创建实例

            Console.WriteLine(type.Name);       // 类名
            Console.WriteLine(type.FullName);   // 命名空间 + 类名
            // 方法1 ：通过反射获取类型，由这个类型来创建实例


            Console.WriteLine("----------------------创建实例---------------------");
            // 方法2 :通过程序集，来创建指定类型的实例

            // 获取程序集的几种方法，任选一种来创建实例
            Assembly assembly = Assembly.GetExecutingAssembly();  // 当前执行的程序集
            Assembly assembly2 = type.Assembly;    // 获取某个类型所在的程序集
            Assembly assembly3 = Assembly.GetAssembly(type); // 获取某个类型所在的程序集

            // 使用默认构造函数创建实例
            // 通过程序集创建
            // typeName:类型的完整名字  ignoreCase:忽略大小写
            Object FinalObj1 = assembly.CreateInstance(type.FullName, true);
            // 通过Activator类创建
            // assemblyName：程序集名字(null: 表示当前程序集)  typeName:类型的完整名字
            ObjectHandle handler = Activator.CreateInstance(null, type.FullName);  // 得到的是经过包装的类型实例，需要去包装
            Object FinalObj2 = handler.Unwrap(); // 去掉包装就是想要的类型实例

            // 使用有参数的构造函数创建实例
            // 特别注意：根据参数的数量，顺序，类型，会匹配对应的构造函数
            // 通过程序集创建
            // BindingFlags:限定对类型成员的搜索,这里不需要限定，采用Default默认
            // Binder ：封装了CreateInstance绑定对象(RefClass)的规则，我们几乎永远都会传递null进去，实际上使用的是预定义的DefaultBinder
            // object[] : 它包含我们传递进去的参数，有参数的构造函数将会使用这些参数
            // CultureInfo ： 它包含了关于语言和文化的信息(简单点理解就是什么时候ToString("c")应该显示“￥”，什么时候应该显示“＄”)。
            // object[] activationAtrributes : 表示添加的特性
            object[] parameters = new object[2];
            parameters[0] = "你好";
            parameters[1] = 10;
            Object FinalObj3 = assembly.CreateInstance(type.FullName, true, BindingFlags.Default, null, parameters, null, null);


            // 通过Activator类创建
            // assemblyName：程序集名字(null: 表示当前程序集)  typeName:类型的完整名字
            // 其实Activator类，就是先获取了Assembly，之后跟上面的方法是类似的
            object[] parameters2 = new object[3];
            parameters2[0] = 5;
            parameters2[1] = 10;
            parameters2[2] = 1;
            ObjectHandle handler2 = Activator.CreateInstance(null, type.FullName, true, BindingFlags.Default, null, parameters2, null, null);  // 得到的是经过包装的类型实例，需要去包装
            Object FinalObj4 = handler2.Unwrap(); // 去掉包装就是想要的类型实例

            // 一种应用：创建程序集中所有以Class结尾的类的实例
            Type[] types = type.Assembly.GetTypes();
            foreach (Type item in types)
            {
                if (item.Name.EndsWith("Class", true, null))
                {
                    Activator.CreateInstance(item);
                }
            }

            Console.WriteLine("----------------------调用方法---------------------");
            // 反射调用类型的方法
            // 方法1：将上面方法获取的实例，强制转换到对应的类型，再来调用方法
            RefClass rf2 = (RefClass)FinalObj4;
            rf2.Debug();

            // 方法2 ：通过反射技术来调用方法
            // InvokeMember 方法

            // string name : 成员名
            // BindingFlags ： 成员类型
            // Binder ： 采用默认null
            // object target ：若是实例方法，就填入需要调用方法的具体实例; 若是静态方法，就填入null 或 typeof(类型)
            // object[] args : 方法需要的参数
            type.InvokeMember("Add", BindingFlags.InvokeMethod, null, rf1, null);
            int result = (int)type.InvokeMember("Add", BindingFlags.InvokeMethod, null, rf2, null);  // 若是方法有返回值，还可以接收返回值
            Console.WriteLine("result = " + result);
            // 静态方法
            Object[] parameters3 = new Object[2];
            parameters3[0] = 16;
            parameters3[1] = 19;
            type.InvokeMember("Add", BindingFlags.InvokeMethod, null, null, parameters3);
            type.InvokeMember("Add", BindingFlags.InvokeMethod, null, typeof(RefClass), parameters3);

            // MethodInfo.Invoke 方法--------------------推荐，因为可以调用重载方法和引用参数
            Console.WriteLine("*****************推荐，因为可以调用重载方法和引用参数*******************");
            // 调用方法
            // 特别注意：这个就相当于InvokeMember方法已经填好了 string name : 成员名 BindingFlags ： 成员类型 
            // 最后就是执行
            MethodInfo mi = type.GetMethod("Add", BindingFlags.Instance | BindingFlags.Public, null, new Type[] { }, null);
            mi.Invoke(rf1, null);
            // 调用重载方法
            MethodInfo mi2 = type.GetMethod("Add", BindingFlags.Instance | BindingFlags.Public, null, new Type[] { typeof(string), typeof(int) }, null);
            Object[] parameters5 = new Object[2];
            parameters5[0] = "hello";
            parameters5[1] = 9;
            mi2.Invoke(rf2, parameters5);
            // 静态
            MethodInfo mi3 = type.GetMethod("Add", BindingFlags.Static | BindingFlags.Public);
            Object[] parameters4 = new Object[2];
            parameters4[0] = 6;
            parameters4[1] = 9;
            mi3.Invoke(null, parameters4);
            mi3.Invoke(typeof(RefClass), parameters4);
            // 调用带引用参数方法
            // ref
            MethodInfo mi4 = type.GetMethod("Add", BindingFlags.Instance | BindingFlags.Public, null, new Type[] { typeof(string), typeof(int).MakeByRefType() }, null);
            Object[] parameters6 = new Object[2];
            parameters6[0] = "world";
            parameters6[1] = 9;
            mi4.Invoke(rf1, parameters6);
            Console.WriteLine(parameters6[1]);
            // out
            MethodInfo mi5 = type.GetMethod("Add", BindingFlags.Instance | BindingFlags.Public, null, new Type[] { typeof(int), typeof(int).MakeByRefType() }, null);
            Object[] parameters7 = new Object[2];
            parameters7[0] = 8;
            mi5.Invoke(rf1, parameters7);
            // 用parameters7[1] 来接收 out 的参数
            Console.WriteLine(parameters7[1]);
            Console.ReadKey();
        }
    }
}
