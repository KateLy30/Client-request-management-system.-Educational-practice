using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using customer_request_accounting_system.EntityFramework;
using customer_request_accounting_system.Models;
using System.Threading.Tasks;

namespace customer_request_accounting_system.ViewModels
{
    public partial class NewCustomerViewModel : ObservableObject 
    {
        private readonly INavigation _navigation;
        private readonly AppDbContext _context;
        public NewCustomerViewModel(INavigation navigation, AppDbContext context)
        {
            _navigation = navigation;
            _context = context;
        }

        [ObservableProperty]
        public string name;
        [ObservableProperty]
        public string phone;
        [ObservableProperty]
        public string email;
        [ObservableProperty]
        public string city;

        [RelayCommand]
        async Task ClickOkButton()
        {
            Customer customer = new Customer
            {
                Name = Name,
                Phone = Phone,
                CityOfResidence = City
            };
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            await _navigation.PopModalAsync();
        }

        [RelayCommand]
        async Task ClickCancelButton()
        {
            await _navigation.PopModalAsync();
        }


    }
}
