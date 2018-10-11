using System;
//using Word;

namespace EarlyBindCOM
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * 前期绑定  操作COM类型的对象
             *
             * 步骤1：根据COM类型生成互操作程序集（interop assembly）
             *     方法1: 使用类型库导入程序（TlbImp.exe）
             *           第一步：打开 C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Visual Studio 2017\Visual Studio Tools\VS 2017的开发人员命令提示符 
             *                   或者 D:\ProgrammingSoftware\VisualStudio2017\Common7\Tools\LaunchDevCmd.bat  这个方法写在了上方工具栏的 “工具/外部工具” 可直接打开
             *           第二步：运行 TlbImp "需要使用的dll文件的目录（看方法2）" /out:获取的互操作程序集位置("x:xxx\xxx\xx.dll")
             *           第三步：将获得的dll文件添加到本工程的引用
             *           第四步：之后就可以调用Net一样，调用COM组件
             *     方法2：（推荐）
             *          第一步：在工程的引用中添加需要的COM组件（没有方法1灵活）
             *          第二步：之后就可以调用Net一样，调用COM组件
             *
             *     方法3：创建自定义的互操作程序集
             *           第一步:选择dll文件中，根据需要的类或接口，新建一个文件 详见 ApplicationClass.cs
             *           第二步:在接口中添加需要的方法
             *           第三步:之后就可以调用Net一样，调用COM组件,不过能用的方法就只有自己添加的方法
             */

            Application app = new Application();    // COM组件，可以创建借口实例
            
            Console.Read();
        }
    }
}
