using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialize
{
    [Serializable]
   public class MyClass
   {
        /* 字段 */
        //[NonSerialized]
        // todo 修饰符  private  internal  protected
        public int number;
        public string name;
        //public List<int> IntList;
        //public Stack<string> StrStack;
        //public Queue<double> DouQueue;
        //public Dictionary<byte, float> BFdict;
        // TODO 构造函数 存在与否

       public MyClass()
       {
           
       }

           public MyClass(int number, string name)
           {
                this.number = number;
                this.name = name;
                //IntList = new List<int>(); IntList.Add(10);
                //StrStack = new Stack<string>(); StrStack.Push("Hell好）");
                //DouQueue = new Queue<double>(); DouQueue.Enqueue(15.33);
                //BFdict = new Dictionary<byte, float>(); BFdict.Add(151,1.2f);
           }
        public void Print()
        {
            Console.WriteLine(number);
            Console.WriteLine(name);
            //Console.WriteLine(IntList + " / "+ IntList.Count + "/"+ IntList[0]);
            //Console.WriteLine(StrStack + " / " + StrStack.Count + "/" + StrStack.Pop());
            //Console.WriteLine(DouQueue + " / " + DouQueue.Count + "/" + DouQueue.Dequeue());
            //Console.WriteLine(BFdict + " / " + BFdict.Count);
            //if (BFdict!=null && BFdict.Count>0)
            //{
            //    foreach (KeyValuePair<byte, float> keyValuePair in BFdict)
            //    {
            //        Console.WriteLine("Key: "+keyValuePair.Key + " / Value: "+ keyValuePair.Value);
            //    }
            //}
        }
   }
}
