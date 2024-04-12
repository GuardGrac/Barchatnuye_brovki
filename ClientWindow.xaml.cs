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
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public List<Service> services = new List<Service>();
        public ClientWindow(string FirstName)
        {
            InitializeComponent();
            services = App.Entity.Service.ToList();
            ServiceItemsControl.ItemsSource = services;
            ///FirstName.Text = FirstName;
            int foundsLines = services.Count;
            int AllLines = App.Entity.Service.Count();
            SearchResultCount.Text = $"{foundsLines} из {AllLines}";
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Autorisation autorisation = new Autorisation();
            autorisation.Show();
            Close();
        }

        private void Image_Initialized(object sender, EventArgs e)
        {
            Image image = (Image)sender;
            string uid = image.Uid;
            string imagePath = "Resources/image/" + App.Entity.Service.FirstOrDefault(x => x.Title == uid).MainImagePath;
            if (imagePath == "Resources/image/")
            {
                imagePath += "beauty_logo.png";
            }
            Uri uri = new Uri(imagePath, UriKind.Relative);
            BitmapImage bitmapImage = new BitmapImage(uri);
            image.Source = bitmapImage;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = SearchBox.Text;
            if (SearchBox.Text != "")
            {
                services = App.Entity.Service.Where(x => x.Title.Contains(query)).ToList();
                ServiceItemsControl.ItemsSource = services;
                int foundsLines = services.Count;
                int AllLines = App.Entity.Service.Count();
                SearchResultCount.Text = $"{foundsLines} из {AllLines}";
            }
            else
            {
                services = App.Entity.Service.ToList();
                ServiceItemsControl.ItemsSource = services;
                int foundsLines = services.Count;
                int AllLines = App.Entity.Service.Count();
                SearchResultCount.Text = $"{foundsLines} из {AllLines}";
            }
        }

    }
}
