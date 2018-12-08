using FamiliadaClientForms.ViewModel;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FamiliadaClientForms.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ControlPanelPage : ContentPage
	{
		public ControlPanelPage (TcpClient tcpClient)
		{
			InitializeComponent ();
            this.BindingContext = new ControlPanelPageViewModel(this, tcpClient);
        }
	}
}