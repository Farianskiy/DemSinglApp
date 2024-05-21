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

namespace DemSinglApp
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void BtnAuth_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPassword.Password;

            using (var context = DemSinglEntities.CreateContext())
            {
                var user = context.Users.FirstOrDefault(User => User.Login == username && User.Password == password);


                if (user != null)
                {
                    var roles = user.Roles.Select(Role => Role.RoleName).ToList();

                    MessageBox.Show("Успешная авторизация!");

                    if (roles.Contains("Admin"))
                    {
                        Manager.MainFrame.Navigate(new AdminPage(user));
                    }
                    else if (roles.Contains("Manager"))
                    {
                        Manager.MainFrame.Navigate(new ManagerPage());
                    }
                    else
                    {
                        Manager.MainFrame.Navigate(new UserPage(user));
                    }
                }
                else
                {
                    MessageBox.Show("Неверное имя пользователя или пароль!");
                }
            }
        }
    }
}
