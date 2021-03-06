﻿using System;
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
        public decimal TotalOtherCOD { get { return totalOtherCOD; } }

        private decimal cash;
        private decimal card;
        private decimal online;
        private decimal totalDelivered;
        private int orderCount;
        private int orderCountOther;
        private decimal totalOther;
        private decimal totalOtherCOD;

        public List<LogiNextTransaction> Transactions {get { return transactionList; } }
        private List<LogiNextTransaction> transactionList;

        public LogiNextDriver(string driverID, string driverName)
        {
            DriverName = driverName;
            DriverID = driverID;
            transactionList = new List<LogiNextTransaction>();
        }

        public LogiNextDriver(decimal cash, decimal card, decimal online, decimal totalDelivered, int orderCount, int orderCountOther, decimal totalOther, decimal totalOtherCOD)
        {
            //used to create Total in the driver summary;
            this.DriverName = "Total";
            this.cash = cash;
            this.card = card;
            this.online = online;
            this.totalDelivered = totalDelivered;
            this.orderCount = orderCount;
            this.orderCountOther = orderCountOther;
            this.totalOther = totalOther;
            this.totalOtherCOD = totalOtherCOD;
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

        public string ToString()
        {
            return DriverName;
        }

    }
}
