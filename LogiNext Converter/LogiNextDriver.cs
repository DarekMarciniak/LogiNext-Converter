using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogiNext_Converter
{
    public class LogiNextDriver
    {
        public string DriverName { get; }
        public decimal Cash { get { return cash; } }
        public decimal Card { get { return card; } }
        public decimal Online { get { return online; } }
        public decimal TotalDelivered { get { return totalDelivered; } }
        public decimal OrderCount { get { return orderCount; } }
        public decimal OrderCountOther { get { return orderCountOther; } }
        public decimal TotalOther { get { return totalOther; } }

        private decimal cash;
        private decimal card;
        private decimal online;
        private decimal totalDelivered;
        private decimal orderCount;
        private decimal orderCountOther;
        private decimal totalOther;

        private List<LogiNextTransaction> transactionList;

        public LogiNextDriver(string driverName)
        {
            DriverName = driverName;
            transactionList = new List<LogiNextTransaction>();
        }

        public void AddTransaction(LogiNextTransaction newTransaction)
        {
            transactionList.Add(newTransaction);

            if (newTransaction.OrderStatus.Equals("Delivered", StringComparison.InvariantCultureIgnoreCase) || newTransaction.OrderStatus.Equals("Dostarczony", StringComparison.InvariantCultureIgnoreCase))
            {
                if (newTransaction.PaymentSubType.Equals("CASH", StringComparison.InvariantCultureIgnoreCase))
                {
                    cash += newTransaction.PlannedCashAtDelivery;
                }
                else if (newTransaction.PaymentSubType.Equals("CARD_MANUAL", StringComparison.InvariantCultureIgnoreCase))
                {
                    card += newTransaction.PlannedCashAtDelivery;
                }
                else if (newTransaction.PaymentType.Equals("Prepaid", StringComparison.InvariantCultureIgnoreCase))
                {
                    online += newTransaction.PlannedCashAtDelivery;
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
