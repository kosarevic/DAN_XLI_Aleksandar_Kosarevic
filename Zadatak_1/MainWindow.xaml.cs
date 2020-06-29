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
            btnStart.IsEnabled = false;
            btnCancel.IsEnabled = false;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            PrintViewModel.cancel = false;
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

        void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbStatus.Value += e.ProgressPercentage;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PrintViewModel.cancel = true;
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Printing process canceled.", "Notification");
        }

        private void LostFocus_TextBox(object sender, RoutedEventArgs e)
        {
            //if the age is 65 or more, user has option to check "Lifetime" check box.
            if (pvm.Print.Text != null && pvm.Print.Count != 0)
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
