using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;

namespace WindowsService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }
        private string _processName01;
        private string _processPath01;
        private Timer _aTimer;
        protected override void OnStart(string[] args)
        {
            _processName01 = ConfigurationManager.AppSettings["Process01Name"];
            _processPath01 = ConfigurationManager.AppSettings["Process01Path"];
            // 服务开启执行
            CheckProcess();
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

        protected override void OnStop()
        {
            // 服务结束执行
            _aTimer.Stop();
            _aTimer.Close();
        }

        protected override void OnPause()
        {
            base.OnPause();
            // 服务暂停执行
            _aTimer.Stop();
        }

        protected override void OnContinue()
        {
            base.OnContinue();
            // 服务恢复执行
            _aTimer.Start();
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();
            //系统即将关闭执行
            _aTimer.Stop();
            _aTimer.Close();
        }

    }
}
