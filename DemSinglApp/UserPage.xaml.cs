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
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private Users _currentUser;

        public UserPage(Users currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
        }

        private void BtnAdd_Clik(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEdiPage(_currentUser, null));
        }

        private void BtnEdi_Clik(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEdiPage(_currentUser, (sender   as Button).DataContext as Requests));
        }

        private void BtnDelete_Clik(object sender, RoutedEventArgs e)
        {
            var requestsForRemoving = DGridRepair.SelectedItems.Cast<Requests>().ToList();

            // Проверка принадлежности заявок текущему пользователю
            if (requestsForRemoving.Any(r => r.UserId != _currentUser.UserId))
            {
                MessageBox.Show("Вы можете удалять только свои записи!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (MessageBox.Show($"Вы точно хотите удалить следующие {requestsForRemoving.Count} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DemSinglEntities.GetContext().Requests.RemoveRange(requestsForRemoving);
                    DemSinglEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    LoadRequests();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadRequests();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                LoadRequests();
            }
        }

        private void LoadRequests()
        {
            var context = DemSinglEntities.GetContext();
            var query = context.Requests.Where(r => r.UserId == _currentUser.UserId);

            if (int.TryParse(TxtSearchRequestId.Text, out int requestId))
            {
                query = query.Where(r => r.RequestId == requestId);
            }

            DGridRepair.ItemsSource = query.ToList();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TxtSearchRequestId.Text = string.Empty;
            LoadRequests();
        }
    }
}
