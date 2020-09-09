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
        public decimal OrderCount { get { return orderCount; } }
        public decimal OrderCountOther { get { return orderCountOther; } }
        public decimal TotalOther { get { return totalOther; } }

        public decimal TotalOtherCOD {  get { return totalOtherCOD; } }

        private decimal cash;
        private decimal card;
        private decimal online;
        private decimal totalDelivered;
        private decimal orderCount;
        private decimal orderCountOther;
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
            dr["Cash"] = cash;
            dr["Card"] = card;
            dr["Online"] = online;
            dr["COD Other"] = totalOtherCOD;
            dr["Total"] = totalDelivered;
            dr["Order Count"] = orderCount;
            dr["Non Delivered - Order Count"] = orderCountOther;
            dr["Non Delivered - Total Amount"] = totalOther;
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

                totalDelivered += newTransaction.PlannedCashAtDelivery;
                orderCount++;
            }
            else
            {
                totalOther += newTransaction.PlannedCashAtDelivery;
                orderCountOther++;
            }

        }

    }
}
