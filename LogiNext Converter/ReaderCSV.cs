using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LogiNext_Converter
{
    public static class ReaderCSV
    {
        private static List<string[]> fileContents;
        public static List<string[]> GetFileContents(string filePath)
        {
            try
            { 
                fileContents = new List<string[]>();
                Regex csvParserRegex = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");


                using (StreamReader sr = new StreamReader(filePath, Encoding.UTF32, true))
                {
                    string currentLine;
                    string concatenatedLine = "";
                    int lineCounter = 0;
                    bool concatenationStarted = true;
                    int concatenatedLineFieldCount = 0;
                    int expectedFieldCount = 110;
                    int currentLineFieldCount = 0;

                    while ((currentLine = sr.ReadLine()) != null)
                    {
                        if (lineCounter > 0)
                        {
                            string[] currentLineFields = csvParserRegex.Split(currentLine);
                            currentLineFieldCount = currentLineFields.Length;

                            if (currentLineFieldCount >= expectedFieldCount)
                            {
                                fileContents.Add(currentLineFields);
                            }
                            else
                            {
                                concatenatedLine +=  " " + currentLine;
                                concatenatedLineFieldCount = csvParserRegex.Split(concatenatedLine).Length;

                                if (concatenatedLineFieldCount >= expectedFieldCount)
                                {
                                    fileContents.Add(csvParserRegex.Split(concatenatedLine));
                                    concatenatedLine = string.Empty;
                                    concatenatedLineFieldCount = 0;
                                }
                            }
                            
                        }

                        lineCounter++;
                    }
                }

                return fileContents;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error: " + ex.Message, "Exception", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return fileContents;
            }

        }

        
    }
}
