using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace AstroApp.App_Code
{
    public class ImageCompressor
    {
        public static byte[] CompressImage(byte[] originalImage, long quality)
        {
            using (var ms = new MemoryStream(originalImage))
            using (var originalImg = Image.FromStream(ms))
            {
                // Create a new bitmap with white background
                using (var bmp = new Bitmap(originalImg.Width, originalImg.Height, PixelFormat.Format24bppRgb))
                {
                    using (Graphics graphics = Graphics.FromImage(bmp))
                    {
                        // Fill with white background
                        graphics.Clear(Color.White);
                        // Draw the original image on top
                        graphics.DrawImage(originalImg, 0, 0, originalImg.Width, originalImg.Height);
                    }

                    // Compress to JPEG
                    var encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, quality);
                    var codecInfo = GetEncoderInfo("image/jpeg");

                    using (var output = new MemoryStream())
                    {
                        bmp.Save(output, codecInfo, encoderParameters);
                        return output.ToArray();
                    }
                }
            }
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            return ImageCodecInfo.GetImageEncoders().FirstOrDefault(c => c.MimeType == mimeType);
        }

        public static string SaveFile(string fileName, byte[] fileBytes, string uploadPath)
        {
            byte[] originalImageBytes = fileBytes;
            byte[] compressedImageBytes = CompressImage(originalImageBytes, 80);

            // Always save as jpg
            fileName = Guid.NewGuid().ToString() + ".jpg";
            string fullPath = Path.Combine(uploadPath, fileName);

            File.WriteAllBytes(fullPath, compressedImageBytes);
            return fileName;
        }

        public static byte[] GetFileBytes(HttpPostedFile uploadedFile)
        {
            if (uploadedFile != null && uploadedFile.ContentLength > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    uploadedFile.InputStream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            return null;
        }
    }

}