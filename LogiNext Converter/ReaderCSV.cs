using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LogiNext_Converter
{
    public static class ReaderCSV
    {
        private static List<string> fileContents;
        public static List<string> GetFileContents(string filePath)
        {
            fileContents = new List<string>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                string currentLine;
                int lineCounter = 0;

                while ((currentLine = sr.ReadLine()) != null)
                {
                    if (lineCounter > 0)
                    {
                        fileContents.Add(currentLine);
                    }

                    lineCounter++;
                }
            }

            return fileContents;

        }

        
    }
}
