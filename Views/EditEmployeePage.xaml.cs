using customer_request_accounting_system.Models;
using customer_request_accounting_system.ViewModels;

namespace customer_request_accounting_system.Views;

public partial class EditEmployeePage : ContentPage
{
	public EditEmployeePage(Employee employee)
	{
		InitializeComponent();
		BindingContext = new EditEmployeeViewModel(Navigation, employee);
	}
}