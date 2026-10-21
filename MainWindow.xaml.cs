using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Konkov_prakt7
{
    public partial class MainWindow :Window
    {
        static EmployesApi api = new( );
        public ObservableCollection<Employe> employes { get; set; } = new ObservableCollection<Employe>();
        public List<Employe> AllEmploes = new( );
        public MainWindow ()
        {
            InitializeComponent( );
            LoadData( );
        }

        private async Task LoadData()
        {
            try
            {
                AllEmploes = await api.GetEmployesAsync( );
                ListEmployes.ItemsSource = employes;
                ApplyFilters( );
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ApplyFilters()
        {
            if (AllEmploes != null)
            {
                foreach (var item in AllEmploes)
                {
                    if (item.IsActive == true)
                        item.IsActiveStr = "Действующий сотрудник";
                    else item.IsActiveStr = "Не активынй сотрудник";
                    employes.Add(item);
                }
            }
        }
    }
}