using System;
using System.Collections.Generic;
using System.Text;

namespace FamiliadaClientForms
{
    public class Team
    {
        public string Name { get; set; }
        public int Number { get; set; }

        public Team(string name, int number)
        {
            Name = name;
            Number = number;
        }

    }
}
