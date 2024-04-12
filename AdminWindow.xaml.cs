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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public List<Service> services = new List<Service>();
        public AdminWindow()
        {
            InitializeComponent();
        }
        public AdminWindow(string FirstName)
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
            Search();
        }
        public void Search()
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

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            string Article = ((Button)sender).Uid;
            AddServiceWindow addProductWindow = new AddServiceWindow(Article);
            addProductWindow.ShowDialog();
            Search();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddServiceWindow addProductWindow = new AddServiceWindow("");
            addProductWindow.ShowDialog();
            Search();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string Title = ((Button)sender).Uid;
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить этот товар из списка?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                App.Entity.Service.Remove(App.Entity.Service.FirstOrDefault(x => x.Title == Title));
                App.Entity.SaveChanges();
                Search();
            }
        }

    }
}
