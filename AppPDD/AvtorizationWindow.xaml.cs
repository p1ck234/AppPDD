using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Windows.Threading;

namespace AppPDD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AvtorizationWindow : Window
    {
        public static ModelBD.BaseModel bd = new ModelBD.BaseModel();
        public AvtorizationWindow()
        {
            InitializeComponent();
            bd.Users.Load();
        }
        DispatcherTimer timer = new DispatcherTimer();
        int counter = 0;
        private void tbLogin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex input = new Regex(@"[a-zA-Z0-9]");
            Match match = input.Match(e.Text);
            if (!match.Success)
            {
                e.Handled = true;
            }
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            bd.Users.Load();
            try
            {
                if (bd.Users.Local.Where(x => x.login == tbLogin.Text & x.password == tbPass.Password & x.number == tbNumber.Text).FirstOrDefault() != null)
                {
                    Manager.Number = tbNumber.Text;
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    this.Close();
                }
                else
                {
                    Exp("Пользователя с такими данными нет!");
                    counter++;
                }
                if (counter >= 3)
                {
                    Inf("Вы ввели 3 раза неправильно пароль\nВвод пароля будет доступен через 10 секунд");
                    counter = 0;
                    timer = new DispatcherTimer();
                    timer.Tick += new EventHandler(timer_Tick);
                    timer.Interval = new TimeSpan(0, 0, 10);
                    btnEnter.Visibility = Visibility.Hidden;
                    timer.Start();
                }
            }
            catch (Exception ex)
            {
                Exp(ex.ToString());
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            btnEnter.Visibility = Visibility.Visible;
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public static void Exp(string error)
        {
            MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static void Inf(string inf)
        {
            MessageBox.Show(inf, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void tbNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
