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
        private Page _page;
        public ControlPanelPageViewModel(Page page, TcpClient tcpClient)
        {
            _page = page;
            _tcpClient = tcpClient;
            RandQuestion = new Command(OnRandQuestion);
        }

        public ICommand RandQuestion { get; set; }
        void OnRandQuestion()
        {
            Stream stream = _tcpClient.GetStream();
            ASCIIEncoding asen = new ASCIIEncoding();
            TableCommand tableCommand = new TableCommand("RandQuestion");
            string msgString = JMessage.CreateMessage("TableCommand", tableCommand);
            byte[] b = asen.GetBytes(msgString);
            stream.Write(b, 0, b.Length);
        }
    }
}
