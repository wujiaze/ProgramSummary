using System;
using System.Configuration;
using System.Diagnostics;
using System.Timers;

namespace BGDefend
{
    class Program
    {
        private string _processName01;
        private string _processPath01;
        private Timer _aTimer;
        static void Main(string[] args)
        {
            Program program = new Program();
            program._processName01 = ConfigurationManager.AppSettings["Process01Name"];
            program. _processPath01 = ConfigurationManager.AppSettings["Process01Path"];
            program.CheckProcess();
            Console.ReadLine();
        }
        private void CheckProcess()
        {
            _aTimer = new Timer();
            _aTimer.Interval = 1000;
            _aTimer.Elapsed += AtimerEvent;
            _aTimer.AutoReset = true;
            _aTimer.Start();
        }
        private void AtimerEvent(object sender, ElapsedEventArgs e)
        {
            Process[] processes = Process.GetProcessesByName(_processName01);
            if (processes.Length == 0)
            {
                Process myProcess = new Process();
                myProcess.StartInfo.FileName = _processPath01;//要启动的程序位置
                myProcess.StartInfo.Verb = "Open";
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.Start();
            }
        }
    }
}
