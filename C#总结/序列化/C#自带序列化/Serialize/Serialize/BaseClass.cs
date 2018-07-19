using System;


namespace Serialize
{
    [Serializable]
    public class BaseClass
    {
        [NonSerialized]
        protected float powerRank;// 这个数据很重要，不想序列化
    }
}
