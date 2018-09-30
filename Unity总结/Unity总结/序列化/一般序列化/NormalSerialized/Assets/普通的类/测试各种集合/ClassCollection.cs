using System;
using System.Collections.Generic;
using UnityEngine;

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
           Debug.Log(ListSers == null);
           Debug.Log(DictSers);
           Debug.Log(StaSers);
           Debug.Log(QueSers);
        }

        public void PrintDetail()
        {
           Debug.Log(ListSers[0]);
           Debug.Log(DictSers["1"]);
           Debug.Log(StaSers.Peek());
           Debug.Log(QueSers.Peek());
        }
    }
}
