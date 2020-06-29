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
    class PrintViewModel : INotifyPropertyChanged
    {
        public static int Id = 0;
        public static readonly object TheLock = new object();
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

        public PrintViewModel()
        {
            Print = new Print();
        }

        public void PrintCopy(object sender, DoWorkEventArgs e)
        {
            lock (TheLock)
            {
                if (!cancel)
                {
                    Thread.Sleep(1000);
                    print.Id = ++Id;
                    string path = "..//../Files/" + print.Id + "." + print.Date + ".txt";
                    File.WriteAllText(path, print.Text);
                    double progress = 100 / print.Count;
                    (sender as BackgroundWorker).ReportProgress(Convert.ToInt32((double)100 / print.Count));
                }
                else
                {
                    (sender as BackgroundWorker).Dispose();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
