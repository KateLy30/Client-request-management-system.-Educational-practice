using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using customer_request_accounting_system.EntityFramework;
using customer_request_accounting_system.Models;

namespace customer_request_accounting_system.ViewModels
{
    public partial class RequestListViewModel : ObservableObject
    {
        private readonly AppDbContext _context;
        private readonly INavigation _navigation;
        public RequestListViewModel(INavigation navigation, AppDbContext context)
        {
            _navigation = navigation;
            _context = context;
        }
        [RelayCommand]
        private async Task ButtonBack()
        {
            await _navigation.PopAsync();
        }

    }
}
