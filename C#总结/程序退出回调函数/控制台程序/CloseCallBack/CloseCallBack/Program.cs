using System.IO;
using System.Text;


namespace CloseCallBack
{
    class Program
    {
        static void Main(string[] args)
        {
            ExitTool.SetCloseEvent();
            ExitTool.OnCloseEvent = () =>
            {
                using (FileStream fs = new FileStream(@"D:\Desktop\q.txt", FileMode.Create))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes("OnCloseEvent");
                    fs.Write(bytes, 0, bytes.Length);
                }
            };

            // 其他程序
            


            ExitTool.WaitCloseProject();
        }
    }
}
