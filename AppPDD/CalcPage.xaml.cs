using AppPDD.ModelBD;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace AppPDD
{
    /// <summary>
    /// Interaction logic for CalcPage.xaml
    /// </summary>
    public partial class CalcPage : Page
    {
        public CalcPage()
        {
            InitializeComponent();
            dtgTovarTable.ItemsSource = AvtorizationWindow.bd.Machines.Local.Where(x => x.number.Contains(Manager.Number));
        }

        private void sortDate_Checked(object sender, RoutedEventArgs e)
        {
            AvtorizationWindow.bd.Machines.Load();
            dtgTovarTable.ItemsSource = AvtorizationWindow.bd.Machines.Local.Where(x => x.number.Contains(Manager.Number)).OrderBy(x => x.date);
        }

        private void sortStatus_Checked(object sender, RoutedEventArgs e)
        {
            AvtorizationWindow.bd.Machines.Load();
            dtgTovarTable.ItemsSource = AvtorizationWindow.bd.Machines.Local.Where(x => x.number.Contains(Manager.Number)).OrderBy(x => x.status);
        }

        private void sortId_Checked(object sender, RoutedEventArgs e)
        {
            AvtorizationWindow.bd.Machines.Load();
            dtgTovarTable.ItemsSource = AvtorizationWindow.bd.Machines.Local.Where(x => x.number.Contains(Manager.Number)).OrderBy(x => x.id);
        }

        Machine selectEntites = new Machine();

        private void btnOpl_Click(object sender, RoutedEventArgs e)
        {
            selectEntites = (Machine)dtgTovarTable.SelectedItem;
            AvtorizationWindow.bd.Machines.Load();
            Machine current = new Machine();
            if (selectEntites != null)
            {
                try
                {
                    current.number = selectEntites.number;
                    current.fine = selectEntites.fine;
                    current.date = selectEntites.date;
                    if (MessageBox.Show("Вы действительно хотите оплатить штраф с карты?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (selectEntites.status == true)
                        {
                            current.status = false;
                            AvtorizationWindow.bd.Machines.Remove(selectEntites);
                            AvtorizationWindow.bd.Machines.Add(current);
                            AvtorizationWindow.bd.SaveChanges();
                            dtgTovarTable.ItemsSource = AvtorizationWindow.bd.Machines.Local.Where(x => x.number.Contains(Manager.Number)).OrderBy(x => x.id);
                            AvtorizationWindow.Inf("Штраф оплачен");
                        }
                        else AvtorizationWindow.Inf("Штраф уже оплачен");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                AvtorizationWindow.Exp("Вы ничего не выбрали!");
            }
        }
    }
}
