using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zadatak_1.Models;

namespace Zadatak_1.ViewModels
{
    /// <summary>
    /// Class containg logic necessary for application functionality.
    /// </summary>
    class PrintViewModel : INotifyPropertyChanged
    {
        //static variable keeps track of id of each printing iteration.
        public static int Id = 0;
        //object made for purpose of locking selected part of the code, to prevent multi-thredead execution.
        public static readonly object TheLock = new object();
        //Boolean value added to simulate pressing of the cancel button.
        public static bool cancel = false;

        private Print print;

        public Print Print
        {
            get { return print; }
            set
            {
                if (print != value)
                {
                    print = value;
                    OnPropertyChanged("Print");
                }
            }
        }
        //Constructor for initializing singleton object at class initialization.
        public PrintViewModel()
        {
            Print = new Print();
        }
        /// <summary>
        /// Method responsible for generating each printing object iteraion.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PrintCopy(object sender, DoWorkEventArgs e)
        {
            //TheLock prevents multiple threads from executing below part of code.
            lock (TheLock)
            {
                //If user press cancel, code will be skipped and copy wont be made.
                if (!cancel)
                {
                    Thread.Sleep(1000);
                    print.Id = ++Id;
                    string path = "..//../Files/" + print.Id + "." + print.Date + ".txt";
                    File.WriteAllText(path, print.Text);
                    //Value of static number incremeneted by each iteration to signal when last iteration takes place.
                    MainWindow.workerIteration++;

                    if (MainWindow.workerIteration != print.Count)
                    {
                        (sender as BackgroundWorker).ReportProgress(100 / print.Count);
                    }
                    else
                    {
                        //since progress bar doesnt get to 100 if there is uneven number of copies, when last copy is made, status is set to 100%.
                        (sender as BackgroundWorker).ReportProgress(100);
                        MainWindow.printing = false;
                        MainWindow.workerIteration = 0;
                    }
                }
                else
                {
                    //Worker thread gets disposed if cancel button is pressed.
                    (sender as BackgroundWorker).Dispose();
                    MainWindow.printing = false;
                    MainWindow.workerIteration = 0;
                }
            }
        }
        /// <summary>
        /// Event handler for changing elements of the Print object in interface window, raising event for each modification.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
