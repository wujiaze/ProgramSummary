using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionLearn
{
    // 所有的异常类 Exception 可以去MSDN官网查看，主要是 SystemException 和 ApplicationException
    class Program
    {
        static void Main(string[] args)
        {
            MyClass mcls = new MyClass();
            try
            {
                mcls.A();
            }
            catch (DivideByZeroException d)
            {
                Console.WriteLine("catch clause in Main()");
                Console.WriteLine(d.Message);
            }
            finally
            {
                Console.WriteLine("finally clause in Main()");
            }
            Console.WriteLine("After try statement in Main");
            Console.WriteLine("           --- keep runining");
            Console.ReadLine();

            string s = null;
            NewClass.PrintArg(s);
            NewClass.PrintArg("Hi");
            Console.ReadLine();

            string p = null;
            NextClass.PrintArg(p);
            Console.ReadLine();

            // 自定义异常类
            string mu = null;
            MyWarn.PrintArg(mu);
            Console.ReadLine();
        }
    }

    /// <summary>
    /// 处理异常执行顺序
    /// </summary>
    class MyClass
    {
        public void A()
        {
            try
            {
                B();
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("catch clause in A()");
            }
            finally
            {
                Console.WriteLine("finally clause in A()");
            }

        }

        private void B()
        {
            int x = 10, y = 0;
            try
            {
                x /= y;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("catch clause in B()");
            }
            finally
            {
                Console.WriteLine("finally clause in B()");
            }
        }
    }

    /// <summary>
    /// throw 对象 可以用在try 或者 catch ,一般用在try
    /// 功能：在编写程序的时候,根据猜测在适当的地方创建对应的异常类对象
    /// 这个异常可能不会中断程序运行，但是还是需要处理，此时我们就 抛出（throw）这个异常，
    /// 这时，就会去匹配Catch语句 ，原来try中的语句就不在执行了
    /// </summary>
    class NewClass
    {
        public static void PrintArg(string arg)
        {
            
            try
            {
                if (arg == null)
                {
                    ArgumentNullException myan = new ArgumentNullException("arg");
                    throw myan;
                }
                Console.WriteLine(arg + "  执行这一步");
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("Message:  {0}",e.Message);
            }
        }
    }

    /// <summary>
    ///  throw 无对象,只能用在catch语句中
    ///  功能：用于重新抛出当前Catch语句捕获的异常，在外层的Catch中寻求处理
    /// </summary>
    class NextClass
    {
        public static void PrintArg(string arg)
        {
            try
            {
                try
                {
                    if (arg == null)
                    {
                        ArgumentNullException myex = new ArgumentNullException("arg");
                        throw myex;
                    }
                    Console.WriteLine(arg);
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine("Message:  {0}", e.Message);
                    throw;
                }
            }
            catch
            {
                Console.WriteLine("Outer Catch: Handing an Exception");
            }
        }
    }
    /// <summary>
    /// 自定义异常类
    /// 说明：继承 ApplicationException 和 Exception 和 SystemException 的效果是类似的
    /// 四种构造函数：掌握前两种就够用了
    /// </summary>
    [Serializable]
    public class MyException : ApplicationException
    {
        public MyException():base(){}
        public MyException(string message) : base(message)
        {
        }
        public MyException(string message, Exception innerException) : base(message,innerException)
        {

        }
        public MyException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info,context)
        {
        }
    }
    public class MyWarn
    {
        public static void PrintArg(string arg)
        {
            try
            {
                if (arg == null)
                {
                    MyException myan = new MyException("arg");
                    throw myan;
                }
                Console.WriteLine(arg + "  执行这一步");
            }
            catch (MyException e)
            {
                Console.WriteLine("Message:  {0}", e.Message);
            }
        }
    }
}
