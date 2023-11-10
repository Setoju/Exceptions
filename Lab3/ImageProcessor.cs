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

            if (Directory.Exists(folderPath))
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
            else
            {
                MessageBox.Show("Directory doesn't exist");
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
    }
}
