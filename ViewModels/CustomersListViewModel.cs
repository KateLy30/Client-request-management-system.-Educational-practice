using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using customer_request_accounting_system.EntityFramework;
using customer_request_accounting_system.Models;
using customer_request_accounting_system.Views;
using System.Collections.ObjectModel;

namespace customer_request_accounting_system.ViewModels
{
    public partial  class CustomersListViewModel : ObservableObject
    {
        private readonly AppDbContext _context;
        private readonly INavigation _navigation;
        public CustomersListViewModel(INavigation navigation, AppDbContext context)
        {
            _context = context;
            _navigation = navigation;
            LoadCustomers();
        }

        [ObservableProperty]
        private ObservableCollection<Customer>? customers;
        [ObservableProperty]
        public Customer? selectedCustomer;

        // метод обновления списка
        public void LoadCustomers()
        {
            var list = _context.Customers.ToList();
            Customers = new ObservableCollection<Customer>(list);
        }

        // кнопка назад
        [RelayCommand]
        async Task BackButton()
        {
            await _navigation.PopAsync();
        }

        // кнопка обновления списка
        [RelayCommand]
        private void UpdateListButton()
        {
            LoadCustomers();
        }

        // окно создания клиента
        [RelayCommand]
        async Task AddButton()
        {
            await _navigation.PushModalAsync(new NewCustomerPage());
            
        }

        // кнопка удаления данных выбранного клиента
        [RelayCommand]
        private void DeleteButton()
        {
            if (SelectedCustomer == null)
                return;
            _context.Customers.Remove(SelectedCustomer);
            _context.SaveChanges();
        }

        // кнопка редактирования данных выбранного клиента клиента
        [RelayCommand]
        private async Task EditButton()
        {
            if(SelectedCustomer == null) return;
            var existingCustomer = _context.Customers.Find(SelectedCustomer.Id);
            if (existingCustomer != null)
                await _navigation.PushModalAsync(new EditCustomerPage(SelectedCustomer));
        }
    }
}
