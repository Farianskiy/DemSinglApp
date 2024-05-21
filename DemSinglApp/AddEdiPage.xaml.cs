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
    /// Логика взаимодействия для AddEdiPage.xaml
    /// </summary>
    public partial class AddEdiPage : Page
    {
        private Requests _currentRequest = new Requests();
        private Users _currentUser;

        public AddEdiPage(Users currentUser, Requests selectedRequest)
        {
            InitializeComponent();

            if(selectedRequest != null)
            {
                _currentRequest = selectedRequest;  
            }

            _currentUser = currentUser;
            DataContext = _currentRequest;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentRequest.EquipmentType))
                errors.AppendLine("Укажите вид оргтехники!");

            if (string.IsNullOrWhiteSpace(_currentRequest.Model))
                errors.AppendLine("Укажите модель!");

            if (string.IsNullOrWhiteSpace(_currentRequest.ProblemDescription))
                errors.AppendLine("Укажите проблему!");

            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());

                return;
            }

            // Заполнение данных заявки
            _currentRequest.EquipmentType = txtType.Text;
            _currentRequest.Model = txtModel.Text;
            _currentRequest.ProblemDescription = txtDescription.Text;
            _currentRequest.RequestDate = DateTime.Now;
            _currentRequest.RequestStatusId = 1;
            _currentRequest.UserId = _currentUser.UserId;


            if (_currentRequest.RequestId == 0)
            {
                DemSinglEntities.GetContext().Requests.Add(_currentRequest);
            }

            try
            {
                DemSinglEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохраненна");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        
    }
}
