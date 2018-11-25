/*
 *      首先明确一个通用的规则，
 *              一个数字 123456789 (0x075BCD15)，那么 高位字节为 07 ,低位字节为 15
 *      小端系统(已验证)
 *          数据的存储
 *              1.1、int由4个字节组成，那么 低位地址 存储 低位字节，高位地址 存储 高位字节
 *              1.2、int[] arr数组，  那么 低位地址 存储  arr[0]， 高位地址 存储 arr[count-1]  同时说明了：数组元素随着索引的增加，地址是增加的
 *
 *      大端系统(todo 没有验证过)
 *          数据的存储
 *              1.1、int由4个字节组成，那么 低位地址 存储 高位字节，高位地址 存储 低位字节
 *              1.2、int[] arr数组，  那么 低位地址 存储  arr[count-1]， 高位地址 存储 arr[0]  同时说明了：数组元素随着索引的增加，地址是减少的        
 */
using System;

namespace PointLearn.小端大端内存理解
{
    class PointandMemory
    {
        public static void MemoryOrderInt()
        {
            int number = 123456789; // 表示为16进制，0x75BCD15
            unsafe
            {
                int* pint = &number;
                byte* pbyte = (byte*)pint;
                for (int i = 0; i < sizeof(int); i++)
                {
                    Console.WriteLine("Address {0} , Value {1:X}", (int)pbyte, *pbyte);
                    pbyte++;
                }
            }
        }

        public static void MemoryOrderIntArr()
        {
            int[] arr = { 1, 2, 3, 4, 5 };
            unsafe
            {
                fixed (int* p = arr)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int* temp = &p[i];
                        Console.WriteLine("Address {0}, Value {1}", (int)temp, *temp);
                    }
                }
            }
        }

    }
}
