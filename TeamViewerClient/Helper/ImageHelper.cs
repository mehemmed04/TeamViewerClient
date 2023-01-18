using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamViewerClient.Helper
{
    public class ImageHelper
    {
        public static string GetScreenshot()
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
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
                path = path + $"/image{year}{month}{day}{hour}{minute}{second}{msecond}.png";


                bmp.Save(path);
                return path;// saves the image
            }
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
