using System.Threading.Tasks;
using System.Windows;

namespace MessagePump
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnDoStuff_Click(object sender, RoutedEventArgs e)
        {
            btnDoStuff.IsEnabled = false;
            lblStatus.Content = "Doing Stuff";
            //Thread.Sleep(4000);
            await Task.Delay(4000);
            await Task.Yield();
            lblStatus.Content = "Not Doing Anything";
            btnDoStuff.IsEnabled = true;
        }
    }
}
