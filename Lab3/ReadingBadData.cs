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
        private int _correctFiles;
        private List<int> _multiplications = new List<int>();

        public void ProcessFile(string fileName)
        {
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    int firstNumber = int.Parse(reader.ReadLine());
                    int secondNumber = int.Parse(reader.ReadLine());
                    try
                    {                        
                        int product = 0;
                        checked
                        {
                            product = firstNumber * secondNumber;
                        }
                        _sum += product;
                        _multiplications.Add(product);
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
            catch(Exception e) when (e is OverflowException || e is FormatException)
            {
                Console.WriteLine($"File {fileName} has incorrect data.");
                string outputFileName = "bad_data.txt";
                File.AppendAllText(outputFileName, $"{fileName} (Bad Data)\n");
            }            
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File {fileName} doesn't exist.");
                string outputFileName = "no_file.txt";
                File.AppendAllText(outputFileName, $"{fileName} (File Not Found)\n");
            }           
            catch (ArgumentNullException)
            {
                Console.WriteLine($"File {fileName} has no data.");
            }
        }        


        public long CalculateAverage()
        {
            try
            {
                return _sum / _correctFiles;
            }
            catch (DivideByZeroException e)
            {
                throw e;
            }            
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
        public List<int> GetMultiplications
        {
            get
            {
                return _multiplications;
            }
        }

        public static void FileDelete(string filePath)
        {
            try
            {                
                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the file: {ex.Message}");
            }
        }
    }
}
