using Barchatnuye_brovki.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для AddServiceWindow.xaml
    /// </summary>
    public partial class AddServiceWindow : Window
    {
        public Service currentService;
        public AddServiceWindow(string serviceTitle)
        {
            InitializeComponent();
            if (serviceTitle != "")
            {
                currentService = App.Entity.Service.FirstOrDefault(x => x.Title == serviceTitle);
                Title.Text = currentService.Title;
                Cost.Text = currentService.Cost.ToString();
                DurationInSecond.Text = currentService.DurationInSeconds.ToString();
                Discount.Text = currentService.Discount.ToString();
                Image.Text = currentService.MainImagePath;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isAdd = false;
                if (currentService == null)
                {
                    currentService = new Service();
                    Random random = new Random();
                    //currentService.Title = random.Next(1000, 10000).ToString();
                    isAdd = true;
                }
                if (Title.Text.Length == 0 || Cost.Text.Length == 0 || DurationInSecond.Text.Length == 0 || Discount.Text.Length == 0)
                {
                    MessageBox.Show("Поля не заполнены!");
                }
                if (int.Parse(Cost.Text) <= 0 || int.Parse(DurationInSecond.Text) <= 0 || int.Parse(Discount.Text) <= 0)
                {
                    MessageBox.Show("Нельзя задать значения меньше нуля!");
                }
                else
                {
                    currentService.Title = Title.Text;
                    currentService.Cost = int.Parse(Cost.Text);
                    currentService.DurationInSeconds = int.Parse(DurationInSecond.Text);
                    currentService.Discount = int.Parse(Discount.Text);
                    currentService.MainImagePath = Image.Text;
                }

                if (isAdd)
                {
                    App.Entity.Service.Add(currentService);
                }
                App.Entity.SaveChanges();
                MessageBox.Show("Успешно сохранено!");
                Close();
            }
            catch
            {

            }

        }
    }
}
