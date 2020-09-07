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
            List<LogiNextTransaction> tempTransactions = new List<LogiNextTransaction>();

            foreach (string csvLine in fileContents)
            {
                string[] csvFields = csvParserRegex.Split(csvLine);
                tempTransactions.Add(new LogiNextTransaction(csvFields));
            }

            return;
        }
    }
}
