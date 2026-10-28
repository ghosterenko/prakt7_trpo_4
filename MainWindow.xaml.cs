using Konkov_prakt7.Winds;
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
    public partial class MainWindow : Window
    {
        static EmployesApi api = new();
        public ObservableCollection<Employe> employes { get; set; } = new ObservableCollection<Employe>();
        public List<Employe> AllEmploes = new();

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                AllEmploes = await api.GetEmployesAsync();
                ListEmployes.ItemsSource = employes;
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ApplyFilters()
        {
            employes.Clear();
            if (AllEmploes == null) return;

            string pos = TbPosition.Text?.Trim() ?? "";
            if (!decimal.TryParse(TbMinSalary.Text, out decimal minSal))
            {
                return;
            }

            foreach (var item in AllEmploes)
            {
                if (item.IsActive == true)
                    item.IsActiveStr = "Действующий сотрудник";
                else item.IsActiveStr = "Не активынй сотрудник";

                if ((string.IsNullOrEmpty(pos) || item.Position.ToLower().Contains(pos.ToLower())) && (minSal <= 0 || item.Salary >= minSal))
                {
                    employes.Add(item);
                }
            }
        }

        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ListEmployes.SelectedValue == null)
            {
                MessageBox.Show("Объект не выбран");
            }
            else
            {
                MessageBoxResult res = MessageBox.Show("Вы точно хотите удалить выбранную запись?", "Удаление", MessageBoxButton.YesNo);

                if (res == MessageBoxResult.Yes)
                {
                    Employe? temp = ListEmployes.SelectedValue as Employe;
                    await api.DeleteEmployesAsync(temp.Id);
                    MessageBox.Show("Удалено");
                    LoadData();
                }
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddWindow(null);
            window.Owner = this;
            window.ShowDialog();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ListEmployes.SelectedValue == null)
            {
                MessageBox.Show("Объект не выбран");
            }
            else
            {
                var window = new AddWindow(ListEmployes.SelectedItem as Employe);
                window.Owner = this;
                window.ShowDialog();
            }
        }

        private void TbPosition_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void TbMinSalary_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

    }
}