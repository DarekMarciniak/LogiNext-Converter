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
        //private DataTable dtSummary;
 
        public List<LogiNextDriver> DriverList { get { return driverList; } }
        private List<LogiNextDriver> driverList = new List<LogiNextDriver>();

        private decimal cashTotal;
        private decimal cardTotal;
        private decimal onlineTotal;
        private decimal otherCodTotal;
        private decimal totalCODDelivered;
        private int orderCountTotal;
        private decimal otherTotal;
        private int otherOrderCountTotal;

        public decimal Cash { get { return cashTotal; } }
        public decimal Card { get { return cardTotal; } }
        public decimal Online { get { return onlineTotal; } }
        public decimal OtherCOD { get { return otherCodTotal; } }
        public decimal TotalCOD { get { return totalCODDelivered; } }
        public int OrderCount { get { return orderCountTotal; } }
        public decimal Other { get { return otherTotal; } }
        public int OtherOrderCount { get { return otherOrderCountTotal; } }

        public LogiNextDriverSummary()
        {
            //DataTableProvider dtp = new DataTableProvider();
            //dtSummary = dtp.SummaryDataTable;

        }
        public void AddDriverTransaction(LogiNextTransaction transaction)
        {
            if (!driverDict.ContainsKey(transaction.DriverID))
            {
                LogiNextDriver newDriver = new LogiNextDriver(transaction.DriverID, transaction.DriverName);
                driverDict.Add(newDriver.DriverID, newDriver);
                driverList.Add(newDriver);
            }

            driverDict[transaction.DriverName].AddTransaction(transaction);
  
        }

        private void AddToTotals(LogiNextDriver driver)
        {
            cashTotal += driver.Cash;
            cardTotal += driver.Card;
            onlineTotal += driver.Online;
            otherCodTotal += driver.TotalOtherCOD;
            totalCODDelivered += driver.TotalDelivered;
            orderCountTotal += driver.OrderCount;
            otherOrderCountTotal += driver.OrderCountOther;
            otherTotal += driver.TotalOther;
        }

        public void AddTotal()
        {
            foreach (LogiNextDriver driver in driverList)
            {
                AddToTotals(driver);
            }
            LogiNextDriver totalDriver = new LogiNextDriver(cashTotal, cardTotal, onlineTotal, totalCODDelivered, orderCountTotal, otherOrderCountTotal, otherTotal, otherCodTotal);
            driverList.Add(totalDriver);
        }

        //public DataTable GetSummaryTable()
        //{
        //    foreach (LogiNextDriver driver in driverDict.Values)
        //    {
        //        Debug.WriteLine(driver.DriverName);
        //        driver.AddDriverDataRow(dtSummary);
        //    }

        //    return dtSummary;
        //}
    }
}
