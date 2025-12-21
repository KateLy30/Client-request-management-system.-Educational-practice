using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using customer_request_accounting_system.EntityFramework;
using customer_request_accounting_system.Models;
using customer_request_accounting_system.Views;
using System.Collections.ObjectModel;

namespace customer_request_accounting_system.ViewModels
{
    public partial class EmployeesListViewModel : ObservableObject 
    {
        private readonly AppDbContext _context;
        private readonly INavigation _navigation;
        public EmployeesListViewModel(INavigation navigation, AppDbContext context)
        {
            _navigation = navigation;
            _context = context;
            LoadEmployees();
        }

        [ObservableProperty]
        public ObservableCollection<Employee> employees;
        [ObservableProperty]
        public Employee? selectedEmployee;

        [RelayCommand]
        private void LoadEmployees()
        {
            var list = _context.Employees.ToList();
            Employees = new ObservableCollection<Employee>(list);
        }

        // кнопка обновления списка
        [RelayCommand]
        private void UpdateListButton()
        {
            LoadEmployees();
        }

        [RelayCommand]
        async Task BackButton()
        {
            await _navigation.PopAsync();
        }

        // окно создания сотрудника
        [RelayCommand]
        async Task AddButton()
        {
            await _navigation.PushModalAsync(new NewEmployeePage());

        }

        // кнопка удаления данных выбранного клиента
        [RelayCommand]
        private void DeleteButton()
        {
            if (SelectedEmployee == null)
                return;
            _context.Employees.Remove(SelectedEmployee);
            _context.SaveChanges();
        }

        // кнопка редактирования данных выбранного клиента клиента
        [RelayCommand]
        private void EditButton()
        {

        }
    }
}
