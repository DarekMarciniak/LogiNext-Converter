using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace LogiNext_Converter
{
    public static class FileDialogs
    {


        public static string OpenFileDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            ofd.Multiselect = false;

            if (ofd.ShowDialog() == true)
            {
                return ofd.FileName;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
