using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Question
    {
        public string QuestionText { get; set; }
        public ObservableCollection<Answer> Answers { get; set; } = new ObservableCollection<Answer>();

        public Question(string _QuestionText)
        {
            QuestionText = _QuestionText;
        }
    }
}
