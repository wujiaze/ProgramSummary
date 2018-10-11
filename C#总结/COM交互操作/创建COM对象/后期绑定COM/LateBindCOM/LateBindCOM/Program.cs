using System;
using System.Reflection;

namespace LateBindCOM
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * 后期绑定 操作COM类型的对象
             *  采用了反射技术，比较麻烦，不推荐，只有当设计时不知道具体的对象接口，才使用后期绑定
             *
             *  根据下列步骤即可
             *
             *
             */

            /* 步骤1 获取类型*/
            // 方法1
            Type comtype = Type.GetTypeFromProgID("Word.ApplicationClass");//命名空间+类名（似乎有问题？todo 以后在说）
            Console.WriteLine("comtype " + comtype);
            // 方法2
            Type comtype2 = Type.GetTypeFromCLSID(new Guid("000209FF-0000-0000-C000-000000000046"));//Guid
            Console.WriteLine("comtype2 " + comtype2);
            /* 步骤2 创建实例 */
            Object comObj = Activator.CreateInstance(comtype2);
            /* 步骤3 调用方法 */

            // 方法1
            Object[] methodArgs = new object[] { "需要调用方法的具体参数" };
            // 之后可将 result 强转为具体类型
            Object result = comtype2.InvokeMember("Name", BindingFlags.InvokeMethod, null, comObj, methodArgs);

            // 方法2 可以得到 methodArgs 中的返回值
            ParameterModifier modifier = new ParameterModifier(2);
            modifier[0] = true;     // 第一个参数是输入输出参数，COM的方法会更新这个参数
            modifier[1] = false;    // 第二个参数是输入参数，   COM的方法不会更新这个参数 
            ParameterModifier[] modifiers = new ParameterModifier[] { modifier };
            // 之后可将 result 强转为具体类型
            Object result2 =comtype2.InvokeMember("Name2", BindingFlags.InvokeMethod, null, comObj, methodArgs, modifiers, null, null);

            Console.Read();
        }
    }
}
