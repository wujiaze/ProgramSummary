/*  
 *      和预定义的类型转换的区别：
 *      
 *      预定义的强制转换，只能是继承之类的关系
 *      而这里定义的类型转换，是自定义的（即使两个类或结构体之间可以毫无关系）
 *      并且类型转换，可以自定义的写成隐式转换或显示转换
 *      
 *      但是，实际意义上，并不是真的在两个毫无关系的对象之间转换，而是在方法内部返回了目标类型的对象
 */

namespace TestSwitch
{
    /*     
     *     类型转换 ： 类型转换的方法，只需要写在两个转换的类的中一个类中即可
     *     
     *     隐式转换：public static implicit operator
     *              
     *     显示转换：public static explicit operator
     *     
     *     内部具体怎么转换，根据实际修更改
     */
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();    // 隐式转换 Program -> MyClass
            MyClass myClass = program;

            MyClass my = new MyClass();         // 隐式转换 MyClass -> Program         
            Program pro = my;

            Program pro2 = new Program();       // 显示转换 Program -> objClass
            ObjClass obj = (ObjClass)pro2;

            ObjClass obj2 = new ObjClass();     // 显示转换 objClass -> Program
            Program pro3 = (Program)obj2;

        }
    }

    class MyClass
    {
        //  隐式转换 MyClass -> Program
        public static implicit operator Program(MyClass myClass)
        {
            Program program = new Program();
            return program;
        }
        //  隐式转换 Program -> MyClass
        public static implicit operator MyClass(Program program)
        {
            MyClass myclass = new MyClass();
            return myclass;
        }
    }

    class ObjClass
    {
        //  显示转换 Program -> objClass
        public static explicit operator ObjClass(Program program)
        {
            ObjClass myclass = new ObjClass();
            return myclass;
        }
        //  显示转换 objClass -> Program
        public static explicit operator Program(ObjClass program)
        {
            Program myclass = new Program();
            return myclass;
        }
    }
}
