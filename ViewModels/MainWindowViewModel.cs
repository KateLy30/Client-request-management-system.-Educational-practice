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
    //TODO: добавить обработку ошибок и валидацию данных
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly AppDbContext _context;
        private readonly INavigation _navigation;
        public MainWindowViewModel( INavigation navigation, AppDbContext context)
        {
            _navigation = navigation;
            _context = context;
            LoadRequests();
        }

        [ObservableProperty]
        public string? searchText;

        [ObservableProperty]
        private ObservableCollection<Request>? requests;
        [ObservableProperty]
        public Request? selectedRequest;

        // поиск по ID
        [RelayCommand]
        private void Search()
        {
            var query = _context.Requests.AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                //string lowerSearch = SearchText.ToLower();
                query = query.Where(e => e.ClientId.ToString().Contains(SearchText));
            }

            Requests.Clear();
            foreach (var req in query.ToList())
                Requests.Add(req);

        }

        // метод обновления списка
        [RelayCommand]
        private void LoadRequests()
        {
            var list = _context.Requests.ToList();
            Requests = new ObservableCollection<Request>(list);
        }

        // окно создание новой заявки
        [RelayCommand]
        private async Task CreateRequest()
        {
            await _navigation.PushModalAsync(new NewRequestPage());
        }

        // кнопка удаление выбранной заявки
        [RelayCommand]
        private void DeleteButton()
        {
            if (SelectedRequest == null)
                return;
            _context.Requests.Remove(SelectedRequest);
            _context.SaveChanges();
        }

        // кнопка редактирование выбранной заявки
        [RelayCommand]
        private async Task EditButton()
        {
            if(SelectedRequest == null) return;
            var existingRequest = _context.Requests.Find(SelectedRequest.Id);

            if (existingRequest != null)
                await _navigation.PushModalAsync(new EditRequestPage(SelectedRequest));
        }

        // кнопка обновления списка
        [RelayCommand]
        private void UpdateListButton()
        {
           LoadRequests();
        }

        // окно списка клиентов
        [RelayCommand]
        async Task OpenListCustomer()
        {
            await _navigation.PushAsync(new CustomersListPage());
        }

        // окно списка сотрудников
        [RelayCommand]
        async Task OpenListEmployee()
        {
            await _navigation.PushAsync(new EmployeesListPage());
        }


       

    }
}
