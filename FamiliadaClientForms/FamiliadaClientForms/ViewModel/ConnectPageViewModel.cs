using FamiliadaClientForms.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FamiliadaClientForms
{
    class ConnectPageViewModel :INotifyPropertyChanged
    {
        ConnectionDetails _connectionDetails = new ConnectionDetails();
        
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

        public ConnectPageViewModel()
        {
            IP = "192.168.0.25";
            Port = "6969";
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
    }
}
