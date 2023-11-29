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
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("What task should we run?");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    //First task about reading the data and writing the names of files that caused exceptions
                    ReadingBadData fileProcessor = new ReadingBadData();
                    ReadingBadData.FileDelete("overflow.txt");
                    ReadingBadData.FileDelete("bad_data.txt");
                    ReadingBadData.FileDelete("no_file.txt");

                    for (int i = 10; i <= 29; i++)
                    {
                        string fileName = $"{i}.txt";
                        fileProcessor.ProcessFile(fileName);
                    }

                    if (fileProcessor.FileCount > 0)
                    {
                        Console.WriteLine($"Sum: {fileProcessor.GetSum}");                        
                        Console.WriteLine($"Arithmetic mean: {fileProcessor.CalculateAverage()}");
                        Console.WriteLine("Multiplications:");
                        foreach (var multiplication in fileProcessor.GetMultiplications)
                        {
                            Console.Write(multiplication + " ");
                        }
                    }
                    break;
                case 2:
                    //Second task about converting to gif
                    Console.WriteLine("Enter the folder path (or leave it empty for the current folder): ");
                    string folderPath = Console.ReadLine();

                    ImageProcessor imageProcessor = new ImageProcessor();
                    imageProcessor.ProcessImages(folderPath);
                    break;
                default:
                    break;
            }
                   
            Console.ReadKey();
        }
    }
}
