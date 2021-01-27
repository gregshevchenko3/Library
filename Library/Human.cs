using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library { 
    enum Gender
    { 
        FEMALE,
        MALE,
        UNDEFINED,
    }
    abstract class Human
    {
        private string _name;
        private string _surname;
        private DateTime _birthDate;
        private Gender _gender;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if(value.Length >= 3)
                {
                    _name = value;
                } 
                else
                {
                    throw new ArgumentException("Passed argument 'name' is invalid");
                }
            }
        }
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                if (value.Length >= 3)
                {
                    _surname = value;
                }
                else
                {
                    throw new ArgumentException("Passed argument 'surname' is invalid");
                }
            }
        }
        public Gender Gender
        {
            get
            { 
                return _gender;
            }
            set
            {
                if(Enum.IsDefined(typeof (Gender), value))
                {
                    _gender = value;
                }
                else
                {
                    throw new ArgumentException("Passed argument 'Gender' must be value of type MyCompany.MyApp.Gender");
                }
            }
        }
        public DateTime BirthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                if (value > DateTime.Now)
                {
                    throw new ArgumentException("Passed argument 'BirthDate' is invalid");
                }
                else
                {
                    _birthDate = value;
                }
            }
        }
        public Human()
        {
            Name = "Nobody";
            Surname = "Noone";
            BirthDate = DateTime.Now;
            Gender = Gender.UNDEFINED;
        }
        public Human(string name, string surname, DateTime birthDate, Gender gender)
        {
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
            this.Gender = gender;
        }
        public virtual void Show()
        {
            Console.WriteLine(
                $"\tName: {Name}\n" +
                $"\tSurname: {Surname}\n"+
                $"\tBirth Date: {BirthDate.ToShortDateString()}\n" +
                $"\tGender: {Gender}"
                );
        }
        public override string ToString()
        {
            return $"\tName: {Name}\n" +
                $"\tSurname: {Surname}\n" +
                $"\tBirth Date: {BirthDate.ToShortDateString()}\n" +
                $"\tGender: {Gender}\n";
        }
    }
}
