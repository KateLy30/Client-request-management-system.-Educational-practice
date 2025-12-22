using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using customer_request_accounting_system.EntityFramework;
using customer_request_accounting_system.Models;

namespace customer_request_accounting_system.ViewModels
{
    public partial class EditCustomerViewModel : ObservableObject 
    {
        private readonly INavigation _navigation;

        [ObservableProperty]
        private Customer customer;

        public EditCustomerViewModel(INavigation navigation, Customer customer) 
        {
            Customer = customer;
            _navigation = navigation;
        }

        [RelayCommand]
        async Task ClickOkButton()
        {
            using (var dbcontext = new AppDbContext())
            {
                var transaction = dbcontext.Database.BeginTransaction();
                try
                {
                    dbcontext.Customers.Update(Customer);
                    dbcontext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // откат транзакции при ошибке
                    throw;

                }
            }

            // закрытие окна ввода данных
            await _navigation.PopModalAsync();
        }

        [RelayCommand]
        async Task ClickCancelButton()
        {
            await _navigation.PopModalAsync();
        }
    }
}
