using FamiliadaClientForms.Model;
using FamiliadaClientForms.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Sockets;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FamiliadaClientForms
{
    class ConnectPageViewModel :INotifyPropertyChanged
    {
        ConnectionDetails _connectionDetails = new ConnectionDetails();
        private INavigation _navigation;

        public ConnectPageViewModel(INavigation navigation)
        {
            _navigation = navigation;
            IP = "192.168.0.25";
            Port = "6969";
            Connect = new Command(OnConnect);
        }
        public string IP
        {
            get { return _connectionDetails.IP; }
            set
            {
                _connectionDetails.IP = value;
                OnPropertyChanged("IP");
            }
        }

        public string Port
        {
            get { return _connectionDetails.Port; }
            set
            {
                _connectionDetails.Port = value;
                OnPropertyChanged("Port");
            }
        }

        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public ICommand Connect { get; set; }
        private void OnConnect()
        {
            try
            {
                if (!int.TryParse(Port, out int port)) throw new Exception("Nieprawidłowy format portu");

                TcpClient tcpClient = new TcpClient();
                tcpClient.Connect(IP, port);
                _navigation.PushAsync(new ControlPanelPage(tcpClient));
                tcpClient.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }
    }
}
