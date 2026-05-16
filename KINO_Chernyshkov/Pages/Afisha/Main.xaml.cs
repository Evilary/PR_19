using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using KINO_Chernyshkov.Classes;

namespace KINO_Chernyshkov.Pages.Afisha
{
    public partial class Main : Page
    {
        List<AfishaContext> AllAfishas = AfishaContext.Select();

        public Main()
        {
            InitializeComponent();

            foreach (AfishaContext item in AllAfishas)
            {
                parent.Children.Add(new Items.Item(item, this));
            }
        }

        private void AddRecord(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.Afisha.Add());
        }

        private void OpenKinoteatr(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Pages.Kinoteatr.Main());
        }
    }
}
