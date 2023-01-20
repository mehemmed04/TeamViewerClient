using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TeamViewerClient.Commands;
using TeamViewerClient.Helper;
using TeamViewerClient.NetworkHelper;

namespace TeamViewerClient.ViewModels
{
    public class AppViewModel : BaseViewModel
    {
        private string ipText;

        public string IpText
        {
            get { return ipText; }
            set { ipText = value; OnPropertyChanged(); }
        }

        private int port;

        public int Port
        {
            get { return port; }
            set { port = value; OnPropertyChanged(); }
        }

        private string buttonContent;

        public string ButtonContent
        {
            get { return buttonContent; }
            set { buttonContent = value; OnPropertyChanged(); }
        }

        private string borderColor;

        public string BorderColor
        {
            get { return borderColor; }
            set { borderColor = value;OnPropertyChanged(); }
        }


        public RelayCommand ConnectCommand { get; set; }
        public AppViewModel()
        {
            ButtonContent = "Connect";
            IpText = StaticMembers.GetLocalIpAddress();
            Port = StaticMembers.GetLocalPort();
            BorderColor = "Green";
            ConnectCommand = new RelayCommand((o) =>
         {
             if (ButtonContent == "Connect")
             {
                 try
                 {
                     App.Current.Dispatcher.Invoke(() =>
                     {
                         Network.Start(IpText, Port);

                         Task.Run(() =>
                         {
                             while (true)
                             {
                                 DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
                                 var path = di.Parent.Parent.FullName;
                                 path = path + $@"\Images";
                                 DeleteFileHelper.DeleteLastImages(path, 5);
                             }

                         });

                         if (Network.Client.Connected)
                         {
                             //ButtonContent = "Disconnect";
                             //BorderColor = "Red";
                         }
                         else
                         {
                             MessageBox.Show("Can not connect . . .");
                         }
                     }); ;
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.Message);
                 }
             }
             //else if (ButtonContent == "Disconnect")
             //{
             //    Network.Client.LingerState = new LingerOption(true, 0);
             //    Network.Client.Close();
             //    ButtonContent = "Connect";
             //    BorderColor = "Green";
             //}
         });
        }

    }
}
