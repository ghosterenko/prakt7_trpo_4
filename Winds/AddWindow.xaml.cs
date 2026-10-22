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
using System.Windows.Shapes;

namespace Konkov_prakt7.Winds
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private Employe? _employe = null;
        private static EmployesApi api = new( );
        public AddWindow(Employe? employe)
        {
            InitializeComponent();
            if(employe != null)
            {
                Title = "Редактирование данных сотрудника";
                AddEditbtn.Content = "Изменить";
                _employe = employe;
            }
            stpanel.DataContext = _employe;
        }

        private void AddEditbtn_Click (object sender, RoutedEventArgs e)
        {
            if (_employe != null)
            {
                var res = api.PutEmployesAsync(_employe.Id, _employe);
                if (res.Result) MessageBox.Show("Успешно!");
                else MessageBox.Show("Ошибка");
            } 
        }
    }
}
