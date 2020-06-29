using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using Zadatak_1.ViewModels;

namespace Zadatak_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PrintViewModel pvm = new PrintViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = pvm;
            pvm.Print.Count = 0;
            pvm.Print.Text = "";
            pvm.Print.Date = DateTime.Now.ToString("dd_MM_yyyy_HH_mm");
        }

        private void Button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < pvm.Print.Count; i++)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.WorkerReportsProgress = true;
                worker.DoWork += pvm.PrintCopy;
                worker.ProgressChanged += worker_ProgressChanged;
                worker.RunWorkerAsync();
            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbStatus.Value = e.ProgressPercentage;
        }
    }
}
