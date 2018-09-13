using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FamiliadaClientForms
{
	public partial class MainPage : ContentPage
	{

		public MainPage()
		{
			InitializeComponent();
            this.BindingContext = new ConnectPageViewModel();
        }

        private void ButtonConnect_Clicked(object sender, EventArgs e)
        {
            Connect();
        }

        private void Connect()
        {
            //try
            //{
            //    string IP = FindViewById<EditText>(Resource.Id.editTextIP).Text;
            //    if (!int.TryParse(FindViewById<EditText>(Resource.Id.editTextPort).Text, out int port)) throw new Exception("Port musi być liczbą");

            //    TcpClient tcpclnt = new TcpClient();
            //    tcpclnt.Connect(IP, port);
            //    tcpclnt.Close();
            //}

            //catch (Exception e)
            //{
            //    Console.WriteLine("Error..... " + e.StackTrace);
            //}
        }

    }
}
