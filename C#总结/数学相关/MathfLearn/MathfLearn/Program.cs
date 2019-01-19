/*
 *      Math 类解析
 *      一、分类
 *          1、基础类
 *              1.1、静态常数
 *                      Math.E                                     自然对数的底
 *                      Math.PI                                    圆周率 π 的值
 *              1.2、三角函数(参数弧度角)
 *                      Math.Asin                                  通过反正弦求弧度角
 *                      Math.Acos                                  通过反余弦求弧度角
 *                      Math.Atan                                  通过反正切求弧度角
 *                      Math.Atan2                                 通过反正切求弧度角(推荐，因为这个可以求90度，即x=0，也能求值)
 *                      Math.Sin                                   弧度角的正弦值
 *                      Math.Cos                                   弧度角的余弦值
 *                      Math.Tan                                   弧度角的正切值
 *              1.3、数值转换
 *                      Math.Abs                                   绝对值
 *                      Math.Ceiling
 *                      Math.Exp                                   自然指数 e 的次方数
 *                      Math.Floor                                 向下取整(返回 float)
 *                      Math.Log                                   取对数 f：值  p: 对数的底(默认为 自然指数 e)
 *                      Math.Log10                                 取对数 f: 值     对数的底为10
 *                      Math.Pow                                   取指数 f: 底  p: 指数
 *                      Math.Round                                 返回最近的整数(但类型不变)，当0.5结尾时，返回最近的两个数之中的偶数
 *                      Math.Sqrt                                  开方数(小于0，返回NaN)
 *              1.4、数值处理
 *                      Math.Max                                   最大值(两个数，一个数组)
 *                      Math.Min                                   最小值(两个数，一个数组)
 *                      Math.Sign                                  返回值 1：正 0：负。特别的 0为返回为1，表示正
 */
using System;

namespace MathfLearn
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * BigMul
               * Cosh
               * Sinh
               * Tanh
               * DivRem
               * IEEERemainder
               * Truncate
             */
            // todo 与Lua 语言比较学习
        }
    }
}
