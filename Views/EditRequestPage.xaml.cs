using customer_request_accounting_system.Models;
using customer_request_accounting_system.ViewModels;

namespace customer_request_accounting_system.Views;

public partial class EditRequestPage : ContentPage
{
	public EditRequestPage(Request request)
	{
		InitializeComponent();
		BindingContext = new EditRequestViewModel(request, Navigation);
	}
}