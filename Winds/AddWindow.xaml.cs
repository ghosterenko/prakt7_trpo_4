using System;
using System.Windows;

namespace Konkov_prakt7.Winds
{
    public partial class AddWindow : Window
    {
        private Employe _employe;
        private Employe? _original;
        private static EmployesApi api = new();

        public AddWindow(Employe? employe)
        {
            InitializeComponent();

            if (employe != null)
            {
                Title = "Редактирование данных сотрудника";
                AddEditbtn.Content = "Изменить";
                _employe = employe;

                _original = new Employe
                {
                    Id = employe.Id,
                    FullName = employe.FullName,
                    Position = employe.Position,
                    Salary = employe.Salary,
                    HireDate = employe.HireDate,
                    IsActive = employe.IsActive
                };
            }
            else
            {
                _employe = new() { HireDate = DateOnly.FromDateTime(DateTime.Today) };
            }

            stpanel.DataContext = _employe;
        }

        private async void AddEditbtn_Click(object sender, RoutedEventArgs e)
        {
            AddEditbtn.Focus();

            if (string.IsNullOrWhiteSpace(_employe.FullName) ||
                string.IsNullOrWhiteSpace(_employe.Position))
            {
                MessageBox.Show("Заполните ФИО и Позицию");
                return;
            }

            if (_employe.Salary < 0)
            {
                MessageBox.Show("Зарплата не может быть отрицательной");
                return;
            }

            try
            {
                bool ok;

                if (_employe.Id == 0)
                {
                    ok = await api.PostEmploesAsync(_employe);
                }
                else
                {
                    bool nameChanged = _original!.FullName != _employe.FullName;
                    bool posChanged = _original.Position != _employe.Position;
                    bool salChanged = _original.Salary != _employe.Salary;
                    bool dateChanged = _original.HireDate != _employe.HireDate;
                    bool actChanged = _original.IsActive != _employe.IsActive;

                    bool FullChanged = nameChanged || posChanged || salChanged || dateChanged;

                    if (!FullChanged && actChanged)
                    {
                        EmployeeDto dto = new EmployeeDto { IsActive = _employe.IsActive };
                        ok = await api.PatchEmployesAsync(_employe.Id, dto);
                    }
                    else
                    {
                        ok = await api.PutEmployesAsync(_employe.Id, _employe);
                    }
                }

                if (ok)
                {
                    MessageBox.Show("Успешно");
                    Close();
                }
                else
                {
                    MessageBox.Show("Ошибка");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}