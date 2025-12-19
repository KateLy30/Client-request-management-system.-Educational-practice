using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using customer_request_accounting_system.EntityFramework;
using customer_request_accounting_system.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace customer_request_accounting_system.ViewModels
{

    public partial class NewRequestViewModel : ObservableObject
    {
        private readonly INavigation _navigation;
        private readonly AppDbContext _context;

        public NewRequestViewModel(AppDbContext context, INavigation navigation)
        {
            _context = context;
            _navigation = navigation;
        }

        [ObservableProperty]
        public int idClient;
        [ObservableProperty]
        public string nameClient;
        [ObservableProperty]
        public int idEmployee;
        [ObservableProperty]
        public string nameEmployee;
        [ObservableProperty]
        public string status;
        [ObservableProperty]
        public string description;
        [ObservableProperty]
        public string priorityRequest;
        [ObservableProperty]
        public string categoryRequest;

        [RelayCommand]
        async Task ClickOkButton()
        {
            Request request = new Request
            {
                ClientId = IdClient,
                AssignedEmployee = IdEmployee,
                Status = Status,
                Description = Description,
                CreateRequestDate = DateTime.Now,
                Priority = PriorityRequest,
                Category = CategoryRequest
            };

            await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();

            await _navigation.PopModalAsync();
        }

        [RelayCommand]
        async Task TextChanged()
        {
            var client = await _context.Customers.FindAsync(IdClient);
            if (client == null)
                return;
            NameClient = client.Name;
        }
    }
}
