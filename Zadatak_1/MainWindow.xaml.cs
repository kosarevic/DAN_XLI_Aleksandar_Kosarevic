using System;
using System.Collections.Generic;
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
        int count = 0;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = pvm;
            pvm.Print.Text = "";
            pvm.Print.Date = DateTime.Now.ToString("dd_MM_yyyy_HH_mm");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < count; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(pvm.PrintCopy)); 
            }

        }
    }
}
