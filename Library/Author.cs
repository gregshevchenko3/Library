using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Author : Human
    {
        private string _biography;
        public string Biography {
            get
            {
                return _biography;
            }
            set
            {
                if (value == null || value.Length == 0)
                    throw new ArgumentException("Argument is invalid");
                _biography = value;
            }
        }
        public Author() : base() {
            Biography = "-";
        }
        public Author(string name, string surname, DateTime birthDate, Gender gender, string biography) : 
            base(name, surname, birthDate, gender)
        {
            Biography = biography;
        }
        public override string ToString()
        {
            return base.ToString() + $"Biography:\n {Biography}";
        }

    }
}
