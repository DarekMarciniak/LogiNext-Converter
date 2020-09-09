using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace LogiNext_Converter
{
    public class LogiNextDriverSummary
    {
        private Dictionary<string, LogiNextDriver> driverDict = new Dictionary<string, LogiNextDriver>();
        private DataTable dtSummary;
        //TODO REMOVE
        public List<LogiNextDriver> DriverList = new List<LogiNextDriver>();
        
        public LogiNextDriverSummary()
        {
            DataTableProvider dtp = new DataTableProvider();
            dtSummary = dtp.SummaryDataTable;

        }
        public void AddDriverTransaction(LogiNextTransaction transaction)
        {
            if (!driverDict.ContainsKey(transaction.DriverID))
            {
                LogiNextDriver newDriver = new LogiNextDriver(transaction.DriverID, transaction.Driver);
                driverDict.Add(newDriver.DriverID, newDriver);
                DriverList.Add(newDriver);
            }

            driverDict[transaction.Driver].AddTransaction(transaction);
        }

        public DataTable GetSummaryTable()
        {
            foreach (LogiNextDriver driver in driverDict.Values)
            {
                Debug.WriteLine(driver.DriverName);
                driver.AddDriverDataRow(dtSummary);
            }

            return dtSummary;
        }
    }
}
