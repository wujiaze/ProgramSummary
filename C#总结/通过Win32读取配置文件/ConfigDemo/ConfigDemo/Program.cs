using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ConfigDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\Desktop\编程学习总结\ProgramSummary\C#总结\C#总结\通过Win32读取配置文件\config.ini";
            string str = ReadIniData("section1", "width", "500", path);
            Console.WriteLine(str);
            string path2 = @"D:\Desktop\编程学习总结\ProgramSummary\C#总结\C#总结\通过Win32读取配置文件\config.txt";
            string str2 = ReadIniData("section1", "width", "500", path2);
            Console.WriteLine(str2);

            Console.Read();
        }
        [DllImport("kernel32.dll",CharSet = CharSet.Auto)]
        private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static string ReadIniData(string section, string key, string def, string filePath)
        {
            if (File.Exists(filePath))
            {
                StringBuilder retVal = new StringBuilder();
                GetPrivateProfileString(section, key, def, retVal, 1024, filePath);
                return retVal.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
