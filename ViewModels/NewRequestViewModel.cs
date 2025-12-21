using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using customer_request_accounting_system.EntityFramework;
using customer_request_accounting_system.Models;
using Microsoft.EntityFrameworkCore;
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
            using (var dbcontext = new AppDbContext())
            {
                var transaction = dbcontext.Database.BeginTransaction();
                try
                {
                    var client = dbcontext.Customers.FirstOrDefault(x => x.Id == IdClient);
                    var employee = dbcontext.Employees.FirstOrDefault(x => x.Id == IdEmployee); 

                    Request request = new Request
                    {
                        ClientId = IdClient,
                        ClientName = client!.Name,
                        AssignedEmployee = IdEmployee,
                        EmployeeName = employee!.Name,
                        Status = Status.ToString(),
                        Description = Description,
                        CreateRequestDate = DateTime.Now,
                        Priority = PriorityRequest,
                        Category = CategoryRequest
                    };

                    dbcontext.Requests.Add(request);
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
