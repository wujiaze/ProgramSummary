
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfAwait
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnProcess_Click(object sender,RoutedEventArgs e)
        {
            btnProcess.IsEnabled = false;
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            int completePercent = 0;
            for (int i = 0; i < 10; i++)
            {
                if (_cancellationToken.IsCancellationRequested)
                break;
                try
                {
                    await Task.Delay(500, _cancellationToken);
                    completePercent = 10 * (i + 1);
                }
                catch (TaskCanceledException exception)
                {
                    completePercent = i * 10;
                }

                ProgressBar.Value = completePercent;
            }

            string message = _cancellationToken.IsCancellationRequested
                ? string.Format("Process was canceled at {0}%.", completePercent)
                : "Process completed normally.";
            MessageBox.Show(message, "Completion Stauts");
            ProgressBar.Value = 0;
            btnProcess.IsEnabled = true;
            btnCancel.IsEnabled = true;
        }

        private  void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (!btnProcess.IsEnabled)
            {

                btnCancel.IsEnabled = false;
                _cancellationTokenSource.Cancel();
            }
        }
    }
}
