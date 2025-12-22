using customer_request_accounting_system.EntityFramework;
using customer_request_accounting_system.Models;
using customer_request_accounting_system.ViewModels;

namespace customer_request_accounting_system.Views;

public partial class NewRequestPage : ContentPage
{
    private readonly AppDbContext _dbContext = new AppDbContext();
    public NewRequestPage()
    {
        InitializeComponent();
        BindingContext = new NewRequestViewModel(_dbContext, Navigation);
    }
}