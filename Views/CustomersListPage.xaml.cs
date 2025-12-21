using customer_request_accounting_system.EntityFramework;
using customer_request_accounting_system.ViewModels;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace customer_request_accounting_system.Views;

public partial class CustomersListPage : ContentPage
{
    private readonly AppDbContext appDbContext = new AppDbContext();
    public CustomersListPage()
	{
		InitializeComponent();
		BindingContext = new CustomersListViewModel(Navigation,  appDbContext);
	}
}