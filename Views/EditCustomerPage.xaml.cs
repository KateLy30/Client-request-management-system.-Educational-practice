using customer_request_accounting_system.Models;
using customer_request_accounting_system.ViewModels;

namespace customer_request_accounting_system.Views;

public partial class EditCustomerPage : ContentPage
{
	public EditCustomerPage(Customer customer )
	{
		InitializeComponent();
		BindingContext = new EditCustomerViewModel(Navigation, customer);
	}
}