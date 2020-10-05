using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LogiNext_Converter
{
    public class LogiNextParser
    {

        public LogiNextDriverSummary ParseLogiNextCSV(string filePath)
        {
            LogiNextDriverSummary lnDriverSummary = new LogiNextDriverSummary();

            Regex csvParserRegex = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            
            List<string[]> fileContents = ReaderCSV.GetFileContents(filePath);

            List<LogiNextTransaction> tempTransactions = new List<LogiNextTransaction>();
            

            foreach (string[] csvLine in fileContents)
            {
                //string[] csvFields = csvParserRegex.Split(csvLine);
                tempTransactions.Add(new LogiNextTransaction(csvLine));
            }

            foreach (LogiNextTransaction transaction in tempTransactions)
            {
                lnDriverSummary.AddTransaction(transaction);
            }
            
            return lnDriverSummary;
        }
    }
}
