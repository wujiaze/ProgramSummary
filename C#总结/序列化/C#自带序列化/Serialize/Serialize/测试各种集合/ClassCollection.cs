using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialize.测试各种集合
{
    [Serializable]
    class ClassCollection
    {
        public readonly int x;
        private List<SerTest> ListSers;
        public Dictionary<string, SerTest> DictSers;
        public Stack<SerTest> StaSers;
        public Queue<SerTest> QueSers;
        public float Myfloat { get; set; }
        public ClassCollection()
        {
            x = 5;
        }

        public ClassCollection(List<SerTest> listSers, Dictionary<string, SerTest> dictSers, Stack<SerTest> staSers, Queue<SerTest> queSers)
        {
            ListSers = listSers;
            DictSers = dictSers;
            StaSers = staSers;
            QueSers = queSers;
        }

        public void Print()
        {
            Console.WriteLine(ListSers == null);
            Console.WriteLine(DictSers);
            Console.WriteLine(StaSers);
            Console.WriteLine(QueSers);
        }

        public void PrintDetail()
        {
            Console.WriteLine(ListSers[0]);
            Console.WriteLine(DictSers["1"]);
            Console.WriteLine(StaSers.Peek());
            Console.WriteLine(QueSers.Peek());
        }
    }
}
