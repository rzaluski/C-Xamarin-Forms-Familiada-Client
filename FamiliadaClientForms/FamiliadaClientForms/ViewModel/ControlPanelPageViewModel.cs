using Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FamiliadaClientForms.ViewModel
{
    class ControlPanelPageViewModel :INotifyPropertyChanged
    {
        private TcpClient _tcpClient;
        private Page _page;
        private Question _currentQuestion { get; set; }

        public ControlPanelPageViewModel(Page page, TcpClient tcpClient)
        {
            _page = page;
            _tcpClient = tcpClient;
            RandQuestionCommand = new Command(OnRandQuestion);
            AnswerCommand = new Command<Button>(OnAnswer);
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

        public Question CurrentQuestion
        {
            get { return _currentQuestion; }
            set
            {
                _currentQuestion = value;
                OnPropertyChanged("CurrentQuestion");
            }
        }

        public ICommand RandQuestionCommand { get; set; }
        void OnRandQuestion()
        {
            Stream stream = _tcpClient.GetStream();
            ASCIIEncoding asen = new ASCIIEncoding();
            string msgString = JMessage.CreateMessage("RandQuestion", null);
            byte[] b = asen.GetBytes(msgString);
            stream.Write(b, 0, b.Length);

            byte[] bb = new byte[1000];
            int k = stream.Read(bb, 0, 1000);
            msgString = System.Text.Encoding.ASCII.GetString(bb, 0, k);
            JMessage msg = JMessage.Deserialize(msgString);
            CurrentQuestion = JMessage.Deserialize<Question>(msg.ObjectJson);
        }

        public ICommand AnswerCommand { get; set; }
        void OnAnswer(Button button)
        {
            Answer answer = button.BindingContext as Answer;
            CurrentQuestion.Answers.Remove(answer);
            Stream stream = _tcpClient.GetStream();
            ASCIIEncoding asen = new ASCIIEncoding();
            string msgString = JMessage.CreateMessage("Answer", answer);
            byte[] b = asen.GetBytes(msgString);
            stream.Write(b, 0, b.Length);
        }
    }
}
