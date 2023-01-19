using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TeamViewerClient.Helper;

namespace TeamViewerClient.NetworkHelper
{
    public class Network
    {
        public static TcpClient Client { get; set; } = new TcpClient();
        public static void Start(string ipString, int port)
        {
            Client = new TcpClient();
            var ip = IPAddress.Parse(ipString);
            var ep = new IPEndPoint(ip, port);

            try
            {
                Client.Connect(ep);
                if (true)
                {
                    MessageBox.Show("Connected");
                    var writer = Task.Run(async () =>
                    {
                        while (true)
                        {

                            byte[] data = ImageHelper.GetBytesOfScreenshot();
                            var stream = Client.GetStream();
                            stream.Write(data, 0, data.Length);
                            await Task.Delay(25);
                        }
                    });

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



    }
}
