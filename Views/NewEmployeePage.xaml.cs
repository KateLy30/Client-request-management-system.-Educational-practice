using customer_request_accounting_system.EntityFramework;
using customer_request_accounting_system.ViewModels;

namespace customer_request_accounting_system.Views;

public partial class NewEmployeePage : ContentPage
{
    private readonly AppDbContext _dbContext = new AppDbContext();
    public NewEmployeePage()
	{
		InitializeComponent();
		BindingContext = new NewEmployeeViewModel(Navigation,  _dbContext);	
	}
}