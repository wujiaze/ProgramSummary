using System;
using System.Diagnostics;
using System.Reflection;

namespace 正则
{
    class AttributesSample
    {
        public void Mymethod(int int1m, out string str2m, ref string str3m)
        {
            str2m = "in Mymethod";
        }

        public static int Main(string[] args)
        {
            Type MyType = Type.GetType("AttributesSample");
            Console.WriteLine(MyType+"11111");
            MethodBase Mymethodbase = MyType.GetMethod("Mymethod");
            Console.WriteLine("Mymethodbase = " + Mymethodbase);
            MethodAttributes Myattributes = Mymethodbase.Attributes;
            PrintAttributes(typeof(System.Reflection.MethodAttributes), (int)Myattributes);
            return 0;
        }


        public static void PrintAttributes(Type attribType, int iAttribValue)
        {
            if (!attribType.IsEnum)
            {
                Console.WriteLine("This type is not an enum.");
                return;
            }

            FieldInfo[] fields = attribType.GetFields(BindingFlags.Public | BindingFlags.Static);
            for (int i = 0; i < fields.Length; i++)
            {
                int fieldvalue = (Int32)fields[i].GetValue(null);
                if ((fieldvalue & iAttribValue) == fieldvalue)
                {
                    Console.WriteLine(fields[i].Name);
                }
            }
        }
    }
}
