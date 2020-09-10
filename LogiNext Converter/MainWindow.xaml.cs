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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filePath = FileDialogs.OpenFileDialog();
                if (filePath == string.Empty) return;

                dgSummary.ItemsSource = null;
                dgTransactions.ItemsSource = null;

                Task<bool>[] tasks = new Task<bool>[1];

                tasks[0] = Task.Factory.StartNew(() => ParseCSV(filePath));
                Task.WaitAll(tasks);
                dgSummary.ItemsSource = lnSummary.DriverList;
                dgTransactions.ItemsSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ParseCSV(string filePath)
        {
            try
            {
                LogiNextParser lnp = new LogiNextParser();
                lnSummary = lnp.ParseLogiNextCSV(filePath);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void DgSummary_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataGridRow dr = (DataGridRow)sender;
                if (dr == null)
                {
                    e.Handled = true;
                    return;
                }

                LogiNextDriver lnd = (LogiNextDriver)dr.Item;

                dgTransactions.ItemsSource = lnd.Transactions;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
  
        }
    }
}
