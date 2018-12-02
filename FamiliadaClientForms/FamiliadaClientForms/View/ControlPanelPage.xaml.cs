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
            this.BindingContext = new ControlPanelPageViewModel(tcpClient);
            List<Answer> answers = new List<Answer>();
            answers.Add(new Answer("1", 1));
            answers.Add(new Answer("2", 1));
            answers.Add(new Answer("3", 1));
            listViewAnswers.ItemsSource = answers;
		}
	}
}