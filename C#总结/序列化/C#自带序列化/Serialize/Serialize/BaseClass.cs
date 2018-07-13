using System;


namespace Serialize
{
    [Serializable]
    public class BaseClass
    {
        [NonSerialized]
        protected float powerRank;

        public SerTest ser;
    }
}
