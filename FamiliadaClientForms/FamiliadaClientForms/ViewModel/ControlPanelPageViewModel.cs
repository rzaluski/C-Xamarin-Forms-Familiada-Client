using Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private bool _isRoundOn;
        private bool _isQuestionSubmitted;
        private bool _isFirstTeamPicked;
        private ObservableCollection<Team> _teams = new ObservableCollection<Team>();
        private Team _selectedTeam;
        public ControlPanelPageViewModel(Page page, TcpClient tcpClient)
        {
            _page = page;
            _tcpClient = tcpClient;
            RandQuestionCommand = new Command(OnRandQuestion);
            AnswerCommand = new Command<Button>(OnAnswer);
            SubmitQuestionCommand = new Command(OnSubmitQuestion);
            IncorrectAnswerCommand = new Command(OnIncorrectAnswer);
            Teams.Add(new Team("Drużyna Lewa", 0));
            Teams.Add(new Team("Drużyna Prawa", 1));
            IsQuestionSubmitted = false;
            IsFirstTeamPicked = false;
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

        public Team SelectedTeam
        {
            get { return _selectedTeam; }
            set
            {
                _selectedTeam = value;
                OnPropertyChanged("SelectedTeam");
            }
        }

        public bool IsRoundOn
        {
            get { return _isRoundOn; }
            set
            {
                _isRoundOn = value;
                OnPropertyChanged("IsRoundOn");
            }
        }

        public bool IsQuestionSubmitted
        {
            get { return _isQuestionSubmitted; }
            set
            {
                _isQuestionSubmitted = value;
                OnPropertyChanged("IsQuestionSubmitted");
                OnPropertyChanged("ShowPicker");
            }
        }

        public bool IsFirstTeamPicked
        {
            get { return _isFirstTeamPicked; }
            set
            {
                _isFirstTeamPicked = value;
                OnPropertyChanged("IsFirstTeamPicked");
                OnPropertyChanged("ShowPicker");
            }
        }

        public bool ShowPicker
        {
            get { return !IsFirstTeamPicked && IsQuestionSubmitted; }
        }

        public ICommand RandQuestionCommand { get; set; }
        void OnRandQuestion()
        {
            SendMessage("RandQuestion", null);

            JMessage msg = ReadMessage();
            CurrentQuestion = JMessage.Deserialize<Question>(msg.ObjectJson);
        }

        public ICommand AnswerCommand { get; set; }
        void OnAnswer(Button button)
        {
            if (!IsFirstTeamPicked)
            {
                SendMessage("FirstAnsweringTeam", SelectedTeam.Number);
                IsFirstTeamPicked = true;
            }
            Answer answer = button.BindingContext as Answer;
            CurrentQuestion.Answers.Remove(answer);
            SendMessage("CorrectAnswer", answer);

            if(IsRoundOn)
            {
                JMessage msg = ReadMessage();
                IsRoundOn = JMessage.Deserialize<bool>(msg.ObjectJson);
            }
        }

        public ICommand IncorrectAnswerCommand { get; set; }
        void OnIncorrectAnswer()
        {
            if(!IsFirstTeamPicked)
            {
                SendMessage("FirstAnsweringTeam", SelectedTeam.Number);
                IsFirstTeamPicked = true;
            }
            SendMessage("IncorrectAnswer", null);

            JMessage msg = ReadMessage();
            IsRoundOn = JMessage.Deserialize<bool>(msg.ObjectJson);
        }

        public ICommand SubmitQuestionCommand { get; set; }
        void OnSubmitQuestion()
        {
            SendMessage("SubmitQuestion", null);
            IsQuestionSubmitted = true;
            IsRoundOn = true;
        }

        private void SendMessage(string header, Object obj)
        {
            Stream stream = _tcpClient.GetStream();
            ASCIIEncoding asen = new ASCIIEncoding();
            string msgString = JMessage.CreateMessage(header, obj);
            byte[] b = asen.GetBytes(msgString);
            stream.Write(b, 0, b.Length);
        }

        private JMessage ReadMessage()
        {
            Stream stream = _tcpClient.GetStream();
            byte[] bb = new byte[1000];
            int k = stream.Read(bb, 0, 1000);
            string msgString = System.Text.Encoding.ASCII.GetString(bb, 0, k);
            return JMessage.Deserialize(msgString);
        }

        public ObservableCollection<Team> Teams
        {
            get { return _teams; }
            set
            {
                _teams = value;
                OnPropertyChanged("Teams");
            }
        }
    }
}
