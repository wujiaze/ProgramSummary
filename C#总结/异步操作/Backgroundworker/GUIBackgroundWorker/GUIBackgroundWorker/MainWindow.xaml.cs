
using System.ComponentModel;
using System.Threading;
using System.Windows;

// BackgroundWorker 类 适用场景：新建线程，后台处理任务，并可以不时的与主线程通信 （新线程 通知 主线程）
namespace GUIBackgroundWorker
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker bgWorker = new BackgroundWorker();
        public MainWindow()
        {
            InitializeComponent();
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.DoWork += DoWorkHandler;
            bgWorker.ProgressChanged += ProgressChangedHandler;
            bgWorker.RunWorkerCompleted += RunWorkerCompletedHandler;
        }

        private void RunWorkerCompletedHandler(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar.Value = (double)e.Result  ;
            if (e.Cancelled)
                MessageBox.Show("后台线程被取消");
            else
                MessageBox.Show("后台线程全部完成");
           
        }

        private void ProgressChangedHandler(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void DoWorkHandler(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            
            for (int i = 0; i < 10; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;          // 退出后台线程，执行 RunWorkerCompletedHandler 方法
                }
                else
                {
                    worker.ReportProgress(i*10);
                    Thread.Sleep(500);
                }
            }

            e.Result = 50d;
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            if (!bgWorker.IsBusy)
            {
                bgWorker.RunWorkerAsync();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            progressBar.Value = 0;
            bgWorker.CancelAsync();
        }
    }
}
