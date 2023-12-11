using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Exceptions
{
    internal class ImageProcessor
    {
        public void ProcessImages(string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath))
            {
                folderPath = Directory.GetCurrentDirectory();
            }

            try
            {
                string[] files = Directory.GetFiles(folderPath);

                foreach (string filePath in files)
                {
                    if (IsImageFile(filePath))
                    {
                        MirrorImage(filePath);
                    }
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }            
        }
        private bool IsImageFile(string filePath)
        {
            string[] validExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            string fileExtension = Path.GetExtension(filePath).ToLower();
            return Array.Exists(validExtensions, ext => ext == fileExtension);
        }

        private void MirrorImage(string filePath)
        {
            try
            {
                using (Image image = Image.FromFile(filePath))
                {
                    image.RotateFlip(RotateFlipType.RotateNoneFlipX);

                    string newFilePath = Path.Combine(
                        Path.GetDirectoryName(filePath),
                        Path.GetFileNameWithoutExtension(filePath) + "-mirrored.gif"
                    );

                    image.Save(newFilePath, ImageFormat.Gif);
                    Console.WriteLine($"Mirrored and saved as GIF: {Path.GetFileName(newFilePath)}");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"File {Path.GetFileNameWithoutExtension(filePath)} should be an image but it's corrupted.");
            }
        }
    }
}
