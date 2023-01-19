using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Encoder = System.Drawing.Imaging.Encoder;

namespace TeamViewerClient.Helper
{
    public class ImageHelper
    {
        public static string GetScreenshot()
        {
            //    Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            //    using (Graphics g = Graphics.FromImage(bmp))
            //    {
            //        g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
            var path = "../../Images/";
            Directory.CreateDirectory(path);
            DateTime date = DateTime.Now;
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;
            int hour = date.Hour;
            int minute = date.Minute;
            int second = date.Second;
            int msecond = date.Millisecond;
            path = path + $"/image{year}{month}{day}{hour}{minute}{second}{msecond}.jpg";


            //        bmp.Save(path);
            //        return path;// saves the image


            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size);
                    // Save the image to a file with a quality of 50
                    ImageCodecInfo codec = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                    EncoderParameters encoderParams = new EncoderParameters(1);
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 50L);
                    bitmap.Save(path, codec, encoderParams);
                }

            }
            return path;
        }
        public static byte[] GetBytesOfScreenshot()
        {
            var imagePath = GetScreenshot();
            var image = new Bitmap(imagePath);
            ImageConverter imageconverter = new ImageConverter();
            var imagebytes = ((byte[])imageconverter.ConvertTo(image, typeof(byte[])));
            return imagebytes;
        }
    }
}
