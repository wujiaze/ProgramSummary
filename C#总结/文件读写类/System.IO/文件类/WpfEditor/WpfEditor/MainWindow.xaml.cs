using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace WpfEditor
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

        private void OnOpen(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new OpenFileDialog()
            {
                Title = "Simple Editor - Open File",
                CheckPathExists = true,
                CheckFileExists = true,
                Filter = "Text files (*.txt)|*.txt|All files|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            if (dlg.ShowDialog()==true)
            {
                text1.Text = File.ReadAllText(dlg.FileName);
            }
        }

        private void OnSave(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new OpenFileDialog()
            {
                Title = "Simple Editor - Save as",
                DefaultExt = "txt",
                Filter = "Text files (*.txt)|*.txt|All files|*.*",
            };
            if (dlg.ShowDialog() == true)
            {
                File.WriteAllText(dlg.FileName,text1.Text);
            }
        }
    }
}
