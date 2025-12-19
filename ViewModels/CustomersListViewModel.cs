using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using customer_request_accounting_system.EntityFramework;
using customer_request_accounting_system.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

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
        }

        [ObservableProperty]
        private ObservableCollection<Customer> customers;
        [ObservableProperty]
        public Customer? selectedCustomer;

        [RelayCommand]
        async Task BackButton()
        {
            await _navigation.PopAsync();
        }

        [RelayCommand]
        async Task AddButton()
        {
            await _navigation.PushModalAsync(new NewCustomerPage());
        }

        [RelayCommand]
        async Task DeleteButton()
        {
            if (SelectedCustomer == null)
                return;
            _context.Customers.Remove(SelectedCustomer);
            await _context.SaveChangesAsync();
        }
    }
}
