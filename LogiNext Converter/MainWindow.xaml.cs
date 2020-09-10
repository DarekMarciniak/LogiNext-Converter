using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading.Tasks;

namespace LogiNext_Converter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LogiNextDriverSummary lnSummary = new LogiNextDriverSummary();

        //todo remove
        DataTable tempTable = new DataTable();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string filePath = @"C:\Users\10287407\Documents\Temp\tests\OrderReport_2020_08_20_08_05_38.csv";  //Todo fix FileDialogs.OpenFileDialog();
            if (filePath == string.Empty) return;

            Task<bool>[] tasks = new Task<bool>[1];

            tasks[0] = Task.Factory.StartNew(() => ParseCSV(filePath));
            Task.WaitAll(tasks);
            dgSummary.ItemsSource = lnSummary.DriverList; //tempTable.DefaultView;

        }

        private bool ParseCSV(string filePath)
        {
            LogiNextParser lnp = new LogiNextParser();
            lnSummary = lnp.ParseLogiNextCSV(filePath);

            tempTable = lnSummary.GetSummaryTable();
            return true;
            

        }
    }
}
