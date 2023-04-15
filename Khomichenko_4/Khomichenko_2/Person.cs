using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            if (this.DateOfBirth.Date.Day == now.Date.Day && this.DateOfBirth.Date.Month == now.Date.Month)
            {
                return true;
            }

            return false;
        }

        public void IsValid() {
            IsDateValid();
            IsEmailValid();
        }

        private void IsEmailValid() {

            // Регулярний вираз для перевірки електронної пошти
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<=.{1,256})(@)(?!\.)([a-z0-9]([a-z0-9-]{0,61}[a-z0-9])?(\.))+[a-z0-9]([a-z0-9-]{0,126}[a-z0-9])?(?<!\.)$";

            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            if (!regex.IsMatch(this.Email)) {
                throw new CustomExeption("Введіть правильний email");
            }
        }

        private void IsDateValid()
        {
            DateTime now = DateTime.Now;

            this.Age = (int)((now.Date - this.DateOfBirth.Date).TotalDays / 365.2425);

            if (this.DateOfBirth > now.Date)
            {
                throw new CustomExeption("Ви ще не народились!");
            }

            if (this.Age > 135) {
                throw new Exception("Ви занадто старі!");
            }
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
