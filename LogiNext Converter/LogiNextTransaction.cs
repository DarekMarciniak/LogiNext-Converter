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
        public string OrderState;
        public string OrderDate;
        public string PaymentType;  
        public decimal ActualCashAtDelivery;
        public string PaymentSubType;
        public string TransactionID;
        public string Driver;
        public string TripNumber;

        private string actualCashAtDeliveryParsed;

        private Dictionary<string, int> indexDictionary;

        public LogiNextTransaction(string[] csvLineFields)
        {
            InitializeIndexDictionary();

            OrderNumber = NormalizeFieldValue(csvLineFields[indexDictionary["OrderNumber"]]);
            Destination = NormalizeFieldValue(csvLineFields[indexDictionary["Destination"]]));
            OrderState = NormalizeFieldValue(csvLineFields[indexDictionary["OrderState"]]);
            OrderDate = NormalizeFieldValue(csvLineFields[indexDictionary["OrderDate"]]);
            PaymentType = NormalizeFieldValue(csvLineFields[indexDictionary["PaymentType"]]);
            actualCashAtDeliveryParsed = NormalizeFieldValue(csvLineFields[indexDictionary["ActualCashAtDelivery"]]);
            PaymentSubType = NormalizeFieldValue(csvLineFields[indexDictionary["PaymentSubType"]]);
            TransactionID = NormalizeFieldValue(csvLineFields[indexDictionary["TransactionID"]]);
            Driver = NormalizeFieldValue(csvLineFields[indexDictionary["Driver"]]);
            TripNumber = NormalizeFieldValue(csvLineFields[indexDictionary["TripNumber"]]);

            if (actualCashAtDeliveryParsed == "") actualCashAtDeliveryParsed = "0";

            ActualCashAtDelivery = Convert.ToDecimal(actualCashAtDeliveryParsed, System.Globalization.CultureInfo.InvariantCulture);
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
            indexDictionary.Add("OrderState", 10);
            indexDictionary.Add("OrderDate", 13);
            indexDictionary.Add("PaymentType", 16);
            indexDictionary.Add("ActualCashAtDelivery", 21);
            indexDictionary.Add("PaymentSubType", 22);
            indexDictionary.Add("TransactionID", 23);
            indexDictionary.Add("Driver", 30);
            indexDictionary.Add("TripNumber", 34);
        }
    }
}
