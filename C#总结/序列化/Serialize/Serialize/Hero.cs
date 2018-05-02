using System;
using System.Collections.Generic;

namespace Serialize
{
    
    [Serializable]
    public class Hero
    {
        public int id { get; set; }

        public float attack { get; set; }

        public float defence { get; set; }
        [NonSerialized]
        public string name;
        public List<MyClass> myClist;
        public Dictionary<int, MyClass> myDic;
        // Unity 中使用Vector3 试试

    }
}
