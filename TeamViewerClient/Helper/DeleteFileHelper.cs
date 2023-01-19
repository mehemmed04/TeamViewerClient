using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamViewerClient.Helper
{
    public class DeleteFileHelper
    {
        public static void DeleteLastImages(string directoryPath, int seconds)
        {
            seconds = -1 * seconds;
            string[] files = Directory.GetFiles(directoryPath);

            foreach (string file in files)
            {
                DateTime creationTime = File.GetCreationTime(file);

                if (creationTime < DateTime.Now.AddSeconds(seconds))
                {
                    // Delete the file
                    File.Delete(file);
                }
            }

        }
    }
}
