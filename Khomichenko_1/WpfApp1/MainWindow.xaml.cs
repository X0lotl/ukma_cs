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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Button.Click += Button_Click;

        }
        private void Show_Message(string message)
        {
            MessageBox.Show(message);
        }


        private string GetChineseSign(int yearOfBirh) {

            string[] signs = { "Мавпа", "Півень", "Собака", "Свиня", "Щур", "Бик", "Тигр", "Кролик", "Дракон", "Змія", "Кінь", "Коза" };

            return signs[yearOfBirh % 12];
        }

        private static string GetStarSign(DateTime dateOfBirth)
        {
            int[] fromDate = { 21, 20, 21, 21, 22, 22, 23, 23, 22, 23, 22, 22 };
            string[] signs = { "Козоріг", "Водолій ", "Риби ", "Овен", "Телець", "Близнюки", "Рак", "Лев", "Діва", "Терези", "Скорпіон", "Стрілець", "Козоріг" };
            if (dateOfBirth.Day < fromDate[dateOfBirth.Month - 1])
            {
                return signs[dateOfBirth.Month - 1];
            }
            return signs[dateOfBirth.Month];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int age = 0;
            TextBlock.Text = "";

            if (DataPicker.SelectedDate is null)
            {
                Show_Message("Виберіть дату");
            }
            else
            {
                DateTime pickedDate = (DateTime)DataPicker.SelectedDate;
                DateTime now = DateTime.Now;

                if (pickedDate.Date > now.Date)
                {
                    Show_Message("Ви не народились");
                }
                else if (pickedDate.Date == now.Date)
                {
                    Show_Message("Вітаю з днем народження!");
                }
                else {
                    age = (int)((now.Date - pickedDate.Date).TotalDays / 365.2425);

                    if (age > 135)
                    {
                        Show_Message("Ви вже померли :(");
                    }
                    else {
                        TextBlock.Text += "Ваш вік: " + age.ToString() + "\n";
                        TextBlock.Text += "Ваш знак зодіаку(західна асторологія): " + GetStarSign(pickedDate) + "\n";
                        TextBlock.Text += "Ваш знак зодіаку(китайська асторологія): " + GetChineseSign(pickedDate.Year);
                    }

                }
            }
        }
    }
}
