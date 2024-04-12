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
using System.Windows.Shapes;

namespace Barchatnuye_brovki
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Autorisation autorisation = new Autorisation();
            autorisation.Show();
            Close();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            Client client = new Client();
            client.FirstName = FirstName.Text;
            client.LastName = SecondName.Text;
            client.Email = Email.Text;
            client.Pass = PasswordBox.Password;
            client.RoleID = 2;
            App.Entity.Client.Add(client);
            App.Entity.SaveChanges();
            ClientWindow clientWindow = new ClientWindow(client.FirstName);
            clientWindow.Show();
            Close();
        }
    }
}
