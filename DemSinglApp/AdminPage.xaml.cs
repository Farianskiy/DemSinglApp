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

namespace DemSinglApp
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        private Users _currentUser;

        public AdminPage(Users currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
        }

        private async void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                var context = DemSinglEntities.GetContext();
                var entries = context.ChangeTracker.Entries();

                foreach(var entry in entries)
                {
                    await entry.ReloadAsync();
                }
            }
        }
    }
}
