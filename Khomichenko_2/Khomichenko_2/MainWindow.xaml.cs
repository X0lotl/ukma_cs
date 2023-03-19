using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Khomichenko_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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


                if (!person.IsDateValid())
                {
                    output.Content += "Введіть валідну \n дату народження \n (Не більше ніж сьогодні, \n але й ваш вік має бути меншим за 135)";
                    return;
                }

                if (person.IsBirthDay())
                {
                    output.Content = "Вітаю з днем народження \n";
                }

                output.Content += "Iм'я: " + person.FirstName + "\n";
                output.Content += "Прізвище: " + person.LastName + "\n";
                output.Content += "Email: " + person.Email + "\n";
                output.Content += "Дата народження: " + person.DateOfBirth.Date + "\n";

                output.Content += "Ви дорослий ? " + person.IsAdult() + "\n";
                output.Content += "Ваш знак зодіаку: " + person.SunSign() + "\n";
                output.Content += "Ваш китайський знак зодіаку: \n" + person.ChineseSign() + "\n";
                output.Content += "Сьогодні ваш день народження ? \n" + person.IsBirthDay() + "\n";
            }
        }
    }
}
