using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exceptions
{
    internal class ReadingBadData
    {
        private long _sum;
        private int _product;
        private int _correctFiles;

        public void ProcessFile(string fileName)
        {
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    try
                    {
                        int firstNumber = int.Parse(reader.ReadLine());
                        int secondNumber = int.Parse(reader.ReadLine());

                        _sum += firstNumber + secondNumber;
                        _product *= firstNumber * secondNumber;
                        _correctFiles++;
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine($"Cannot convert to int32 {fileName}");
                        string outputFileName = "overflow.txt";
                        File.AppendAllText(outputFileName, $"{fileName} (Cannot convert to int32)");
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File {fileName} doesn't exist.");
                string outputFileName = "no_file.txt";
                File.AppendAllText(outputFileName, $"{fileName} (File Not Found)\n");
            }
            catch (FormatException)
            {
                Console.WriteLine($"File {fileName} has incorrect data.");
                string outputFileName = "bad_data.txt";
                File.AppendAllText(outputFileName, $"{fileName} (Bad Data)\n");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"File {fileName} has no data.");
            }
        }        


        public double CalculateAverage()
        {
            if (_correctFiles > 0)
            {
                return (double)_product / _correctFiles;
            }

            return 0;
        }

        public int FileCount
        {
            get
            {
                return _correctFiles;
            }            
        }
        public long GetSum
        {
            get
            {
                return _sum;
            }
        }

        public long GetProduct
        {
            get
            {
                return _product;
            }
        }

        public static void FileDelete(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);                    
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the file: {ex.Message}");
            }
        }
    }
}
