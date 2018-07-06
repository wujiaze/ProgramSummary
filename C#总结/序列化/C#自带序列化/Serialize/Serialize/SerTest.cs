using System;
using System.Collections.Generic;

namespace Serialize
{
    // 测试自定义的
    [Serializable]
    public class SerTest
    {
        /* 字段 */
        //[NonSerialized]
        public List<MyClass> ObjList;
        public Dictionary<int, MyClass> myDic;

        /* 属性 */
        public int id { get; set; }

        public float attack { get; set; }

        public float defence { get; set; }
       
        // Unity 中使用Vector3 试试 TODO
        public void Print()
        {
            
        }
    }
}
