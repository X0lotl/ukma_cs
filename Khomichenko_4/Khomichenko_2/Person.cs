using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Khomichenko_2
{
    public class Person : INotifyPropertyChanged
    {
        private int _index = 0;
        private string _firstName = "";
        private string _lastName = "";
        private string _email = "";
        private DateTime _dateOfBirth;
        private int _age = 0;
        private bool _isAdult = false;
        private string _sunSign = "";
        private string _chineseSign = "";
        private bool _isBirthDay = false;

        public string ChineseSign
        {
            get { return _chineseSign; }
        }

        public string SunSign
        {
            get { return _sunSign; }
        }

        public bool IsAdult
        {
            get { return _isAdult; }
        }

        public int Index
        {
            get { return _index; }
            set
            {
                if (_index != value)
                {
                    _index = value;
                    OnPropertyChanged(nameof(Index));
                }
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                if (_dateOfBirth != value)
                {
                    _dateOfBirth = value;
                    ReCalculate();

                    OnPropertyChanged(nameof(DateOfBirth));
                    OnPropertyChanged(nameof(Age));
                    OnPropertyChanged(nameof(SunSign));
                    OnPropertyChanged(nameof(ChineseSign));
                    OnPropertyChanged(nameof(IsAdult));
                    OnPropertyChanged(nameof(IsBirthDay));
                }
            }
        }

        public int Age
        {
            get { return _age; }
        }

        public bool IsBirthDay
        {
            get { return _isBirthDay; }
        }

        public bool CalculateIsBirthDay()
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

            this._age = (int)((now.Date - this.DateOfBirth.Date).TotalDays / 365.2425);

            if (this.DateOfBirth > now.Date)
            {
                throw new CustomExeption("Ви ще не народились!");
            }

            if (this.Age > 135) {
                throw new CustomExeption("Ви занадто старі!");
            }
        }

        public void ReCalculate()
        {
            DateTime now = DateTime.Now;

            this._age = (int)((now.Date - this.DateOfBirth.Date).TotalDays / 365.2425);
            this._chineseSign = CalculateChineseSign();
            this._sunSign = CalculateSunSign();
            this._isAdult = CalculateIsAdult();
            this._isBirthDay = CalculateIsBirthDay();
        }

        private bool CalculateIsAdult() 
        {
            return this.Age >= 18;
        }

        public string CalculateSunSign()
        {
            int[] fromDate = { 21, 20, 21, 21, 22, 22, 23, 23, 22, 23, 22, 22 };
            string[] signs = { "Козоріг", "Водолій ", "Риби ", "Овен", "Телець", "Близнюки", "Рак", "Лев", "Діва", "Терези", "Скорпіон", "Стрілець", "Козоріг" };
            if (this.DateOfBirth.Day < fromDate[this.DateOfBirth.Month - 1])
            {
                return signs[this.DateOfBirth.Month - 1];
            }
            return signs[this.DateOfBirth.Month];
        }

        public string CalculateChineseSign()
        {
            string[] signs = { "Мавпа", "Півень", "Собака", "Свиня", "Щур", "Бик", "Тигр", "Кролик", "Дракон", "Змія", "Кінь", "Коза" };

            return signs[this.DateOfBirth.Year % 12];
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
