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
                if (Client.Connected)
                {
                    MessageBox.Show("Connected");
                    var writer = Task.Run(() =>
                    {
                        while (true)
                        {
                            var text = ImageHelper.GetBytesOfScreenshot();
                            var stream = Client.GetStream();
                            var bw = new BinaryWriter(stream);
                            bw.Write(text);
                            Task.Delay(200);
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
