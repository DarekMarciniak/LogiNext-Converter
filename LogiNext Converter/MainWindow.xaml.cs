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
            LogiNextParser lnp = new LogiNextParser();
            lnSummary = lnp.ParseLogiNextCSV(@"C:\Users\10287407\Documents\Temp\tests\OrderReport_2020_09_05_08_45_46.csv");

            tempTable = lnSummary.GetSummaryTable();
            
            dgSummary.ItemsSource = tempTable.DefaultView;
            return;

        }
    }
}
