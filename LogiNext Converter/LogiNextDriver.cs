using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace LogiNext_Converter
{
    public class LogiNextDriver
    {
        public string DriverName { get; }
        public string DriverID { get; }
        public decimal Cash { get { return cash; } }
        public decimal Card { get { return card; } }
        public decimal Online { get { return online; } }
        public decimal TotalDelivered { get { return totalDelivered; } }
        public int OrderCount { get { return orderCount; } }
        public int OrderCountOther { get { return orderCountOther; } }
        public decimal TotalOther { get { return totalOther; } }

        public decimal TotalOtherCOD {  get { return totalOtherCOD; } }

        private decimal cash;
        private decimal card;
        private decimal online;
        private decimal totalDelivered;
        private int orderCount;
        private int orderCountOther;
        private decimal totalOther;
        private decimal totalOtherCOD;

        private List<LogiNextTransaction> transactionList;

        public LogiNextDriver(string driverID, string driverName)
        {
            DriverName = driverName;
            DriverID = driverID;
            transactionList = new List<LogiNextTransaction>();
        }

        public void AddDriverDataRow(DataTable dtSummary)
        {
            DataRow dr = dtSummary.NewRow();
            dr["Driver"] = DriverName;
            dr["Cash"] = cash.ToString("F2");
            dr["Card"] = card.ToString("F2");
            dr["Online"] = online.ToString("F2");
            dr["COD Other"] = totalOtherCOD.ToString("F2");
            dr["Total"] = totalDelivered.ToString("F2");
            dr["Order Count"] = orderCount.ToString("F0");
            dr["  -  "] = " ";
            dr["ND - Order Count"] = orderCountOther.ToString("F0");
            dr["ND - Total"] = totalOther.ToString("F2");
            dtSummary.Rows.Add(dr);
        }

        public void AddTransaction(LogiNextTransaction newTransaction)
        {
            transactionList.Add(newTransaction);

            if (newTransaction.OrderStatus.Equals("Delivered", StringComparison.InvariantCultureIgnoreCase) || newTransaction.OrderStatus.Equals("Dostarczony", StringComparison.InvariantCultureIgnoreCase))
            {
                if (newTransaction.PaymentSubType.Equals("CASH", StringComparison.InvariantCultureIgnoreCase))
                {
                    cash += newTransaction.OrderValue;
                }
                else if (newTransaction.PaymentSubType.Equals("CARD_MANUAL", StringComparison.InvariantCultureIgnoreCase))
                {
                    card += newTransaction.OrderValue;
                }
                else if (newTransaction.PaymentSubType.Equals("COD OTHER", StringComparison.InvariantCultureIgnoreCase))
                {
                    totalOtherCOD += newTransaction.OrderValue;
                }
                else if (newTransaction.PaymentType.Equals("Prepaid", StringComparison.InvariantCultureIgnoreCase))
                {
                    online += newTransaction.OrderValue;
                }

                totalDelivered += newTransaction.OrderValue;
                orderCount++;
            }
            else
            {
                totalOther += newTransaction.OrderValue;
                orderCountOther++;
            }

        }

    }
}
