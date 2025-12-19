using customer_request_accounting_system.EntityFramework;
using customer_request_accounting_system.ViewModels;

namespace customer_request_accounting_system;
public partial class MainPage : ContentPage
{
    private readonly AppDbContext appDbContext = new AppDbContext();
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainWindowViewModel(Navigation, appDbContext);
    }

}
