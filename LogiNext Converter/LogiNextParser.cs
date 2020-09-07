using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LogiNext_Converter
{
    public static class LogiNextParser
    {
        
        //private static List<string> fileContents;

        public static void ParseLogiNextCSV(string filePath)
        {
            Regex csvParserRegex = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            
            List<string> fileContents = ReaderCSV.GetFileContents(filePath);

            //TODO - refactor
            List<LogiNextTransaction> tempTransactions = new List<LogiNextTransaction>();
            Dictionary<string, LogiNextDriver> driverDict = new Dictionary<string, LogiNextDriver>();

            foreach (string csvLine in fileContents)
            {
                string[] csvFields = csvParserRegex.Split(csvLine);
                tempTransactions.Add(new LogiNextTransaction(csvFields));
            }

            foreach (LogiNextTransaction transaction in tempTransactions)
            {
                if (!driverDict.ContainsKey(transaction.Driver))
                {
                    LogiNextDriver newDriver = new LogiNextDriver(transaction.Driver);
                    driverDict.Add(newDriver.DriverName, newDriver);
                }

                driverDict[transaction.Driver].AddTransaction(transaction);
            }

            return;
        }
    }
}
