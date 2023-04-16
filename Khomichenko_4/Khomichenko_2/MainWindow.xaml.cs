using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Xml.Serialization;

namespace Khomichenko_2
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Person> _originalPersons;
        private ObservableCollection<Person> GenerateData()
        {
            string[] names = { "Danylo", "Artem", "Oksana", "Antonina", "Georgy", "Myhailo", "Vova", "Mykyta", "Oleksiy" };
            string[] lastNames = { "Khomichenko", "Tarasenko", "Shevchenko", "Bondarchuk", "Nestorov", "Porubiy", "Lashko", "Zelenskyi", "Smith", "Ford" };

            ObservableCollection<Person> data = new ObservableCollection<Person>();



            for (int i = 0; i < 50; i++)
            {
                Person newPerson = new Person();

                newPerson.FirstName = names[RandomIndex(names.Length)];
                newPerson.LastName = lastNames[RandomIndex(lastNames.Length)];
                newPerson.Email = RandomEmail(new Random().Next(10) + 5);
                newPerson.DateOfBirth = RandomDateTime();
                newPerson.Index = i + 1;
                try
                {
                    newPerson.IsValid();
                    data.Add(newPerson);
                }
                catch (CustomExeption ex)
                {
                    output.Content = ex.Message;
                }

            }


            return data;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "data.xml";
            SaveDataToFile(fileName);
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "data.xml";
            LoadDataFromFile(fileName);
        }

        private void SaveDataToFile(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Person>));

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, _originalPersons);
            }
        }

        private void LoadDataFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Person>));

                using (StreamReader reader = new StreamReader(fileName))
                {
                    _originalPersons = (ObservableCollection<Person>)serializer.Deserialize(reader);
                }

                // Оновити таблицю
                table.ItemsSource = new ObservableCollection<Person>(_originalPersons);
            }
        }

        private int RandomIndex(int arrayLength)
        {
            var random = new Random();
            return random.Next(arrayLength);
        }

        private DateTime RandomDateTime()
        {
            DateTime start = new DateTime(1901, 1, 1);
            var range = DateTime.Now - start;
            var randomValue = new TimeSpan((long)(new Random().NextDouble() * range.Ticks));
            return start + randomValue;
        }

        private string RandomEmail(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var localPart = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());


            return $"{localPart}@gmail.com";
        }

        public MainWindow()
        {
            InitializeComponent();

            _originalPersons = GenerateData();

            table.ItemsSource = _originalPersons;
        }

        private void AddUserToTable(Person person)
        {
            person.Index = _originalPersons.Last().Index + 1;

            _originalPersons.Add(person);

            table.ItemsSource = _originalPersons;
        }

        private void Filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_originalPersons == null)
                return;

            string indexFilter = IndexFilter.Text.ToLower();
            string firstNameFilter = FirstNameFilter.Text.ToLower();
            string lastNameFilter = LastNameFilter.Text.ToLower();
            string emailFilter = EmailFilter.Text.ToLower();
            string ageFilter = AgeFilter.Text.ToLower();
            string isAdultFilter = IsAdultFilter.Text.ToLower();
            string sunSignFilter = SunSignFilter.Text.ToLower();
            string chineseSignFilter = ChineseSignFilter.Text.ToLower();
            string isBirthdayFilter = IsBirthdayFilter.Text.ToLower();

            var filteredList = _originalPersons.Where(person =>
                person.Index.ToString().Contains(indexFilter) &&
                person.FirstName.ToLower().Contains(firstNameFilter) &&
                person.LastName.ToLower().Contains(lastNameFilter) &&
                person.Email.ToLower().Contains(emailFilter) &&
                person.Age.ToString().Contains(ageFilter) &&
                person.IsAdult.ToString().ToLower().Contains(isAdultFilter) &&
                person.SunSign.ToLower().Contains(sunSignFilter) &&
                person.ChineseSign.ToLower().Contains(chineseSignFilter) &&
                person.IsBirthDay.ToString().ToLower().Contains(isBirthdayFilter)
            ).ToList();

            table.ItemsSource = new ObservableCollection<Person>(filteredList);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string firstname = firstnameInput.Text;
            string lastname = lastnameInput.Text;
            string email = emailInput.Text;


            if (dateOfBirthPicker.SelectedDate == null || firstname == "" || lastname == "" || email == "")
            {
                output.Content = "Введіть всі потрібні данні!!!!";
            }
            else
            {
                Person person = new Person();
                output.Content = "";
                DateTime dateOfBirth = (DateTime)dateOfBirthPicker.SelectedDate;

                person.FirstName = firstname;
                person.LastName = lastname;
                person.Email = email;
                person.DateOfBirth = dateOfBirth;


                try
                {
                    person.IsValid();

                    if (person.IsBirthDay)
                    {
                        output.Content = "Вітаю з днем народження \n";
                    }

                    AddUserToTable(person);

                }
                catch (CustomExeption ex)
                {
                    output.Content = ex.Message;
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Ви дійсно хочете видалити цей елемент?", "Видалити елемент", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Button button = (Button)sender;
                Person personToDelete = (Person)button.DataContext;

                _originalPersons.Remove(personToDelete);

                ObservableCollection<Person> persons = (ObservableCollection<Person>)table.ItemsSource;
                persons.Remove(personToDelete);
            }
        }
    }
}
