using System;
using System.Windows;
using System.Windows.Controls;
using KINO_Chernyshkov.Classes;

namespace KINO_Chernyshkov.Pages.Afisha.Items
{
    public partial class Item : UserControl
    {
        AfishaContext Afisha;
        Main main;

        public Item(AfishaContext Afisha, Main main)
        {
            InitializeComponent();

            idKinoteatr.Text = Afisha.IdKinoteatr.ToString();
            name.Text = Afisha.Name;
            timePrice.Text = Afisha.Time.ToString("dd.MM.yyyy HH:mm") + " | " + Afisha.Price;

            this.Afisha = Afisha;
            this.main = main;
        }

        private void EditRecord(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.Afisha.Add(this.Afisha));
        }

        private void DeleteRecord(object sender, RoutedEventArgs e)
        {
            Afisha.Delete();
            main.parent.Children.Remove(this);
        }
    }
}
