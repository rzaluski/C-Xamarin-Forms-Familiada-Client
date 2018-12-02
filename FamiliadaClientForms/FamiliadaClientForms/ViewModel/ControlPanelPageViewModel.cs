using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FamiliadaClientForms.ViewModel
{
    class ControlPanelPageViewModel
    {
        private TcpClient _tcpClient;
        public ControlPanelPageViewModel(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
            RandQuestion = new Command(OnRandQuestion);
        }

        public ICommand RandQuestion { get; set; }
        void OnRandQuestion()
        {
            Stream stream = _tcpClient.GetStream();
            ASCIIEncoding asen = new ASCIIEncoding();
            TableCommand tableCommand = new TableCommand("RandQuestion");
            JMessage msg = JMessage.FromValue<TableCommand>(tableCommand);
            string msgString = JMessage.Serialize(msg);
            byte[] b = asen.GetBytes(msgString);
            stream.Write(b, 0, b.Length);
        }
    }
}
