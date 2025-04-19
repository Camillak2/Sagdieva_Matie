using Sagdieva_Matie.DB;
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

namespace Sagdieva_Matie.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public static List<User> users { get; set; }
        public static List<Worker> workers { get; set; }

        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string login = LoginTbx.Text.Trim();
                string password = PasswordPbx.Password.Trim();

                workers = new List<Worker>(DBConnection.matie.Worker.ToList());
                var currentWorker = workers.FirstOrDefault(i => i.Login.Trim() == login && i.Password.Trim() == password);
                DBConnection.logginedWorker = currentWorker;

                users = new List<User>(DBConnection.matie.User.ToList());
                var currentClient = users.FirstOrDefault(i => i.Login.Trim() == login && i.Password.Trim() == password);
                DBConnection.logginedUser = currentClient;

                if (currentWorker != null && currentWorker.Role.Name == "Модератор")
                {
                    NavigationService.Navigate(new ProductsPage());
                }

                else

                {
                    MessageBox.Show("Неверный логин или пароль. Попробуйте снова.");
                }
            }
            catch
            {
                MessageBox.Show("Возникла ошибка");
            }
        }
    }
}
