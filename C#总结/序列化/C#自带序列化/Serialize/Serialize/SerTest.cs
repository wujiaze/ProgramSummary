/*
 *     c#序列化的顺序
 *      开始序列化：    执行 [OnSerializing] 方法    -->  序列化字段    -->  执行 [OnSerialized] 方法
 *      开始反序列化：  执行 [OnDeserializing] 方法  -->  反序列化字段  -->  执行 [OnDeserialized] 方法
 */
using System;
using System.Runtime.Serialization;

namespace Serialize
{

    [Serializable]
    public class SerTest : BaseClass
    {
        private int id;
        private float currentHp;
        private float maxHp;
        private float attack;
        private float defence;
        public SerTest(int id,  float maxHp, float attack, float defence)
        {
            this.id = id;
            this.maxHp = maxHp;
            this.currentHp = this.maxHp;
            this.attack = attack;
            this.defence = defence;
        }


        // 下面四个特性的方法，必须带有 StreamingContext 参数

        [OnSerializing]     // 这个特性表示，在序列化之前自动调用
        private void CaculatePowerRank(StreamingContext context)
        {
            this.powerRank = 0.5f * maxHp + 0.2f * attack + 0.3f * defence;
        }
        [OnSerialized]       // 这个特性表示，在序列化之后自动调用
        private void PrintOnSer(StreamingContext context)
        {
            Console.WriteLine(this.powerRank);
        }
        [OnDeserializing]   // 这个特性表示，在反序列化之前自动调用
        private void PrintOnDe(StreamingContext context)
        {
            Console.WriteLine(this.powerRank);
        }

        [OnDeserialized]    // 这个特性表示，在反序列化之后自动调用
        private void CaculatePowerRankOnDes(StreamingContext context) // 反序列化完成后，重新计算powerRank
        {
            this.powerRank = 0.3f * maxHp + 0.2f * attack + 0.3f * defence;
        }
        // 打印值
        public void Print()
        {
            Console.WriteLine(this.powerRank);
           
        }
    }
}
