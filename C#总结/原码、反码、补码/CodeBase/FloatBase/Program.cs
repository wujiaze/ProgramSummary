/*
 *   float double 浮点数
 *   bool
 *   char
 */
using System;

namespace FloatBase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----------浮点数----------");

            float f = -1f; // float 32位4个字节 13.625
            unsafe
            {
                float* p1 = &f;
                byte* b1 = (byte*)p1;
                for (int i = 0; i < sizeof(float); i++)
                {
                    Console.WriteLine("Address  {0}, Value  {1:X}", (int)b1, *b1);
                    b1++;
                }
            }
        }
    }
}
