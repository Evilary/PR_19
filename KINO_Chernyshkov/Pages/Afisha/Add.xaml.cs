using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using KINO_Chernyshkov.Classes;

namespace KINO_Chernyshkov.Pages.Afisha
{
    public partial class Add : Page
    {
        AfishaContext afisha;
        List<KinoteatrContext> allKinoteatrs = KinoteatrContext.Select();

        public Add(AfishaContext afisha = null)
        {
            InitializeComponent();

            foreach (KinoteatrContext item in allKinoteatrs)
            {
                kinoteatr.Items.Add(item.Id + " - " + item.Name);
            }

            if (afisha != null)
            {
                this.afisha = afisha;
                name.Text = afisha.Name;
                date.SelectedDate = afisha.Time.Date;
                time.Text = afisha.Time.ToString("HH:mm");
                price.Text = afisha.Price.ToString();

                for (int i = 0; i < allKinoteatrs.Count; i++)
                {
                    if (allKinoteatrs[i].Id == afisha.IdKinoteatr)
                    {
                        kinoteatr.SelectedIndex = i;
                        break;
                    }
                }

                bthAdd.Content = "Изменить";
            }

            this.afisha = afisha;
        }

        private void AddRecord(object sender, RoutedEventArgs e)
        {
            int priceInt = -1;
            DateTime dateValue;
            TimeSpan timeValue;

            if (kinoteatr.SelectedIndex < 0)
            {
                MessageBox.Show("Необходимо выбрать кинотеатр");
                return;
            }
            if (name.Text == "")
            {
                MessageBox.Show("Необходимо указать наименование фильма");
                return;
            }
            if (date.SelectedDate.HasValue == false)
            {
                MessageBox.Show("Необходимо указать дату сеанса");
                return;
            }
            if (time.Text == "" || TimeSpan.TryParse(time.Text, out timeValue) == false)
            {
                MessageBox.Show("Необходимо указать время сеанса в формате ЧЧ:мм");
                return;
            }
            if (price.Text == "" || int.TryParse(price.Text, out priceInt) == false)
            {
                MessageBox.Show("Необходимо указать стоимость билета");
                return;
            }

            dateValue = date.SelectedDate.Value.Date + timeValue;

            int idKinoteatr = allKinoteatrs[kinoteatr.SelectedIndex].Id;

            try
            {
                if (this.afisha == null)
                {
                    AfishaContext newAfisha = new AfishaContext(
                        0,
                        idKinoteatr,
                        name.Text,
                        dateValue,
                        priceInt
                    );
                    newAfisha.Add();
                    MessageBox.Show("Запись успешно добавлена.");
                }
                else
                {
                    afisha = new AfishaContext(
                        afisha.Id,
                        idKinoteatr,
                        name.Text,
                        dateValue,
                        priceInt
                    );
                    afisha.Update();
                    MessageBox.Show("Запись успешно обновлена.");
                }

                MainWindow.init.OpenPage(new Pages.Afisha.Main());
            }
            catch (Exception exp)
            {
                Debug.WriteLine(exp.Message);
                MessageBox.Show("Ошибка при сохранении данных.");
            }
        }
    }
}
