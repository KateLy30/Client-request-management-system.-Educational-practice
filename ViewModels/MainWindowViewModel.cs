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
    //TODO: удалить поля ФИО при создании нового объекта
    //TODO: реализовать кнопку редактировать 
    //TODO: добавить функции поиска и фильтрации
    //TODO: добавить обработку ошибок и валидацию данных
    //TODO: в списке заявок исправить индекс списка на текст
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
        private ObservableCollection<Request> requests;
        [ObservableProperty]
        public Request? selectedRequest;

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
        private void EditButton()
        {
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

        [RelayCommand]
        async Task OpenListEmployee()
        {
            await _navigation.PushAsync(new EmployeesListPage());
        }

    }
}
