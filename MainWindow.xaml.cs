using Barchatnuye_brovki.Models;
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

namespace Barchatnuye_brovki
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Autorisation : Window
    {
        Random random = new Random();
        string correctAnswer = "";
        int WrongTries = 0;
        public Autorisation()
        {
            InitializeComponent();
        }
        public void GenerateCaptcha()
        {
            CaptchaPanel.Children.Clear();
            correctAnswer = "";
            for (int i = 0; i < 4; i++)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.FontSize = 14;
                textBlock.TextDecorations = TextDecorations.Strikethrough;
                textBlock.Margin = new Thickness(0, random.Next(-10, 10), 0, 0);
                string tempChar = GenerateRandomChar();
                correctAnswer += tempChar;
                textBlock.Text = tempChar;
                CaptchaPanel.Children.Add(textBlock);
            }
        }
        public string GenerateRandomChar()
        {
            string chars = "QWERTYUIOPASDFGHJKLZXCVBNM123456789";
            char randomChar = chars[random.Next(chars.Length)];
            return randomChar.ToString();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Client client = App.Entity.Client.FirstOrDefault(x => x.Email == LoginBox.Text && x.Pass == PasswordBox.Password);
                if (client != null)
                {
                    switch (client.RoleID)
                    {
                        case 2:
                            ClientWindow userWindow = new ClientWindow(client.FirstName);
                            userWindow.Show();
                            Close();
                            break;

                        case 1:
                            AdminWindow adminWindow = new AdminWindow(client.FirstName);
                            adminWindow.Show();
                            Close();
                            break;

                        default:
                            MessageBox.Show("Обнаружена ошибка. Проверьте введённые данные");
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Обнаружена ошибка связанная с ролью пользователя. Проверьте введённые данные");
                    WrongTries++;
                }
                if (WrongTries > 2)
                {
                    LoginBorder.Visibility = Visibility.Hidden;
                    CaptchaBorder.Visibility = Visibility.Visible;
                    GenerateCaptcha();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка.Возможно оборвалось подключение к базе данных");
            }

        }

        private void RegLink_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            Close();
        }

        private void Guest_Click(object sender, RoutedEventArgs e)
        {
            GuestWindow guestWindow = new GuestWindow();
            guestWindow.Show();
            Close();
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (AnswerBox.Text == correctAnswer)
            {
                LoginBorder.Visibility = Visibility.Visible;
                CaptchaBorder.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Неверно, попробуйте ещё раз");
                GenerateCaptcha();
            }
            AnswerBox.Text = "";

        }
    }
}
