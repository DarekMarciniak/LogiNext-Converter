using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogiNext_Converter
{
    public class LogiNextTransaction
    {
        public string OrderNumber { get; }
        public string Destination;
        public string OrderStatus;
        public string OrderDate;
        public string PaymentType;  
        public decimal ActualCashAtDelivery;
        public decimal PlannedCashAtDelivery;
        public string PaymentSubType;
        public string TransactionID;
        public string Driver;
        public string TripNumber;

        private string actualCashAtDeliveryParsed;
        private string plannedCashAtDeliveryParsed;

        private Dictionary<string, int> indexDictionary;

        public LogiNextTransaction(string[] csvLineFields)
        {
            InitializeIndexDictionary();

            OrderNumber = NormalizeFieldValue(csvLineFields[indexDictionary["OrderNumber"]]);
            Destination = NormalizeFieldValue(csvLineFields[indexDictionary["Destination"]]);
            OrderStatus = NormalizeFieldValue(csvLineFields[indexDictionary["OrderStatus"]]);
            OrderDate = NormalizeFieldValue(csvLineFields[indexDictionary["OrderDate"]]);
            PaymentType = NormalizeFieldValue(csvLineFields[indexDictionary["PaymentType"]]);
            actualCashAtDeliveryParsed = NormalizeFieldValue(csvLineFields[indexDictionary["ActualCashAtDelivery"]]);
            plannedCashAtDeliveryParsed = NormalizeFieldValue(csvLineFields[indexDictionary["PlannedCashAtDelivery"]]);
            PaymentSubType = NormalizeFieldValue(csvLineFields[indexDictionary["PaymentSubType"]]);
            TransactionID = NormalizeFieldValue(csvLineFields[indexDictionary["TransactionID"]]);
            Driver = NormalizeFieldValue(csvLineFields[indexDictionary["Driver"]]);
            TripNumber = NormalizeFieldValue(csvLineFields[indexDictionary["TripNumber"]]);

            if (actualCashAtDeliveryParsed == "" || actualCashAtDeliveryParsed == "-") actualCashAtDeliveryParsed = "0";
            if (plannedCashAtDeliveryParsed == "" || plannedCashAtDeliveryParsed == "-") plannedCashAtDeliveryParsed = "0";
            if (PaymentSubType == "" || PaymentSubType == "-") PaymentSubType = "NO SUB-TYPE";

            ActualCashAtDelivery = Convert.ToDecimal(actualCashAtDeliveryParsed, System.Globalization.CultureInfo.InvariantCulture);
            PlannedCashAtDelivery = Convert.ToDecimal(plannedCashAtDeliveryParsed, System.Globalization.CultureInfo.InvariantCulture);

        }

        private string NormalizeFieldValue(string fieldValue)
        {
            if (fieldValue.StartsWith(@"""") && fieldValue.EndsWith(@""""))
            {
                return fieldValue.Substring(1, fieldValue.Length - 2);
            }

            if (fieldValue.StartsWith(@"=(""") & fieldValue.EndsWith(@""")"))
            {
                return fieldValue.Substring(3, fieldValue.Length - 5);
            }

            return fieldValue;
        }

        private void InitializeIndexDictionary()
        {
            indexDictionary = new Dictionary<string, int>();
            indexDictionary.Add("OrderNumber", 1);
            indexDictionary.Add("Destination", 6);
            indexDictionary.Add("OrderStatus", 27);
            indexDictionary.Add("OrderDate", 13);
            indexDictionary.Add("PaymentType", 16);
            indexDictionary.Add("PlannedCashAtDelivery", 20);
            indexDictionary.Add("ActualCashAtDelivery", 21);
            indexDictionary.Add("PaymentSubType", 22);
            indexDictionary.Add("TransactionID", 23);
            indexDictionary.Add("Driver", 30);
            indexDictionary.Add("TripNumber", 34);
        }
    }
}
