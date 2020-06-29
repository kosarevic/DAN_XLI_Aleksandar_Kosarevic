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
        public static bool printing = false;
        public static int workerIteration = 0;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = pvm;
            pvm.Print.Count = 0;
            pvm.Print.Text = "";
            pvm.Print.Date = DateTime.Now.ToString("dd_MM_yyyy_HH_mm");
            btnStart.IsEnabled = false;
            btnCancel.IsEnabled = false;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            PrintViewModel.cancel = false;
            if (!printing)
            {
                printing = true;
                pbStatus.Value = 0;
                btnCancel.IsEnabled = true;

                for (int i = 0; i < pvm.Print.Count; i++)
                {
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.WorkerReportsProgress = true;
                    worker.DoWork += pvm.PrintCopy;
                    worker.ProgressChanged += Worker_ProgressChanged;
                    worker.RunWorkerAsync();
                }
            }
            else
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Printing alredy in progress.", "Notification");
            }
        }

        void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbStatus.Value += e.ProgressPercentage;
            if (pbStatus.Value == 100)
            {
                btnCancel.IsEnabled = false;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PrintViewModel.cancel = true;
            btnCancel.IsEnabled = false;
            printing = false;
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Printing process canceled.", "Notification");
        }

        private void LostFocus_TextBox(object sender, RoutedEventArgs e)
        {
            if (pvm.Print.Text != "" && pvm.Print.Count != 0)
            {
                btnStart.IsEnabled = true;
            }
            else
            {
                btnStart.IsEnabled = false;
            }
        }
    }
}
