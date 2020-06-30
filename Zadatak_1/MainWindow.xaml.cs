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
        //Static boolean value made to keep track if printing process is ongoing.
        public static bool printing = false;
        //Static int value made to keep track of last copy iteration.
        public static int workerIteration = 0;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = pvm;
            //Singleton object is set to desired default values.
            pvm.Print.Count = 0;
            pvm.Print.Text = "";
            pvm.Print.Date = DateTime.Now.ToString("dd_MM_yyyy_HH_mm");
            //Buttons are disabled initialy, untill conditions are met for enabling them.
            btnStart.IsEnabled = false;
            btnCancel.IsEnabled = false;
        }
        /// <summary>
        /// Method exectuted when Print button is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, EventArgs e)
        {
            //Bool value of cancel event set to false if previously was made true.
            PrintViewModel.cancel = false;
            if (!printing)
            {
                //bool value changed to true, signaling that printing has started.
                printing = true;
                //Progress bar reseted to 0 for each new batch of printing process.
                pbStatus.Value = 0;
                //Cancel button enabled when printing has started.
                btnCancel.IsEnabled = true;

                for (int i = 0; i < pvm.Print.Count; i++)
                {
                    //Initialization of BackgroundWorker threads, number depends on user inserted number of copies.
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.WorkerReportsProgress = true;
                    worker.DoWork += pvm.PrintCopy;
                    worker.ProgressChanged += Worker_ProgressChanged;
                    worker.RunWorkerAsync();
                }
            }
            else
            {
                //Mesaage shown if user pressed print button while printing was executing.
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Printing alredy in progress.", "Notification");
            }
        }
        /// <summary>
        /// Method keeps track of progress bar percentage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbStatus.Value += e.ProgressPercentage;
            //Condition added to disable Cancel button if copy proces is finished.
            if (pbStatus.Value == 100)
            {
                btnCancel.IsEnabled = false;
            }
        }
        /// <summary>
        /// Method contains logic for engaging Cancel button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Bool value signaling that cancelation is engaged set to true.
            PrintViewModel.cancel = true;
            //Button disabled immediately after engaging it.
            btnCancel.IsEnabled = false;
            //Printing bool value set to false, signaling that printing process was aborted.
            printing = false;
            //Message displayed to the user, informing that printing process has been canceled.
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Printing process canceled.", "Notification");
        }
        /// <summary>
        /// Method keeps track of changes in TextBox area of application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LostFocus_TextBox(object sender, RoutedEventArgs e)
        {
            //Button that starts printing process is disabled untill these conditions are met.
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
