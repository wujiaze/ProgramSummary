
using System;
using System.Threading;
using System.Threading.Tasks;

// 适用场景： 多核处理器；并行的程序互不影响
// 没有新开线程，是并行编程
namespace TaskParellelLibraryTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Parallel.For(0, 14, i => { Console.WriteLine("{0}的平方是{1}", i, i * i); });
            Console.WriteLine("------------------");
            string[] squares = new[]
            {
                "We", "hold", "these", "truths", "to", "be", "self-evident", "that", "all", "men", "are", "created",
                "equal"
            };
            Parallel.ForEach(squares, i => { Thread.Sleep(500); Console.WriteLine("\"{0}\" has \"{1}\" letters", i, i.Length); });
            Console.WriteLine("**************");
            Console.Read();
        }
    }
}
