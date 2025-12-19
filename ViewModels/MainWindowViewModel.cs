using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using customer_request_accounting_system.EntityFramework;
using customer_request_accounting_system.Models;
using customer_request_accounting_system.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace customer_request_accounting_system.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly AppDbContext _context;
        private readonly INavigation _navigation;
        public MainWindowViewModel( INavigation navigation, AppDbContext context)
        {
            _navigation = navigation;
            _context = context;
            LoadRequestsAsync();
        }

        [ObservableProperty]
        private ObservableCollection<Request> requests;

        [RelayCommand]
        private async Task LoadRequestsAsync()
        {
            var list = await _context.Requests.ToListAsync();
            Requests = new ObservableCollection<Request>(list);
        }


        [ObservableProperty]
        public Request? selectedRequest;

        [RelayCommand]
        private async Task CreateRequest()
        {
            await _navigation.PushModalAsync(new NewRequestPage());
        }

        [RelayCommand]
        async Task EditButton()
        {
        }
        [RelayCommand]
        private async Task UpdateListButton()
        {
           await LoadRequestsAsync();
        }

        [RelayCommand]
        async Task OpenListCustomer()
        {
            await _navigation.PushAsync(new CustomersListPage());
        }

    }
}
