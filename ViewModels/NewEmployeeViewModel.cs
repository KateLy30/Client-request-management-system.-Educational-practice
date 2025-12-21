using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using customer_request_accounting_system.EntityFramework;
using customer_request_accounting_system.Models;

namespace customer_request_accounting_system.ViewModels
{
    public partial class NewEmployeeViewModel : ObservableObject
    {
        private readonly INavigation _navigation;
        private readonly AppDbContext _context;
        public NewEmployeeViewModel(INavigation navigation, AppDbContext context)
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
        public string department;
        [ObservableProperty]
        public string post;
        [ObservableProperty]
        public DateTime dateOfEmployment;

        [RelayCommand]
        async Task ClickOkButton()
        {
            Employee employee = new Employee
            {
                Name = Name,
                Phone = Phone,
                Email = Email,
                Department = Department,
                Post = Post,
                DateOfEmployment = DateOfEmployment
            };
            _context.Employees.Add(employee);
            _context.SaveChanges();

            await _navigation.PopModalAsync();
        }

        [RelayCommand]
        async Task ClickCancelButton()
        {
            await _navigation.PopModalAsync();
        }
    }
}
