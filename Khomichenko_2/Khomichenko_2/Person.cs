using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khomichenko_2
{
    internal class Person
    {
        private string _firstName = "";
        private string _lastName = "";
        private string _email = "";
        private DateTime _dateOfBirth;
        private int _age;

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public bool IsBirthDay()
        {
            DateTime now = DateTime.Now;
            if (this.DateOfBirth.Date == now.Date)
            {
                return true;
            }

            return false;
        }

        public bool IsDateValid()
        {
            DateTime now = DateTime.Now;

            this.Age = (int)((now.Date - this.DateOfBirth.Date).TotalDays / 365.2425);

            if (this.DateOfBirth > now.Date || this.Age > 135)
            {
                return false;
            }

            return true;
        }

        public bool IsAdult() 
        {
            return this.Age >= 18;
        }

        public string SunSign()
        {
            int[] fromDate = { 21, 20, 21, 21, 22, 22, 23, 23, 22, 23, 22, 22 };
            string[] signs = { "Козоріг", "Водолій ", "Риби ", "Овен", "Телець", "Близнюки", "Рак", "Лев", "Діва", "Терези", "Скорпіон", "Стрілець", "Козоріг" };
            if (this.DateOfBirth.Day < fromDate[this.DateOfBirth.Month - 1])
            {
                return signs[this.DateOfBirth.Month - 1];
            }
            return signs[this.DateOfBirth.Month];
        }

        public string ChineseSign()
        {
            string[] signs = { "Мавпа", "Півень", "Собака", "Свиня", "Щур", "Бик", "Тигр", "Кролик", "Дракон", "Змія", "Кінь", "Коза" };

            return signs[this.DateOfBirth.Year % 12];
        }
    }
}
