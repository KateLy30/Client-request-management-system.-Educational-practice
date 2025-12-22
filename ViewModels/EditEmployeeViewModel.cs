using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using customer_request_accounting_system.EntityFramework;
using customer_request_accounting_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace customer_request_accounting_system.ViewModels
{
    public partial class EditEmployeeViewModel : ObservableObject 
    {
        private readonly INavigation _navigation;

        [ObservableProperty]
        private Employee employee;

        public EditEmployeeViewModel(INavigation navigation, Employee employee)
        {
            _navigation = navigation;
            Employee = employee;
        }


        [RelayCommand]
        async Task ClickOkButton()
        {
            switch (Employee.Post) //HACK
            {
                case "0":
                    Employee.Post = "Младший специалист тех. поддержки";
                    break;
                case "1":
                    Employee.Post = "Старший специалист тех. поддержки";
                    break;
                case "2":
                    Employee.Post = "Администратор системы";
                    break;
                case "3":
                    Employee.Post = "Инженер тех. поддержки";
                    break;
                case "4":
                    Employee.Post = "Руководитель группы тех. поддержки";
                    break;
            }
            using (var dbcontext = new AppDbContext())
            {
                var transaction = dbcontext.Database.BeginTransaction();
                try
                {
                    dbcontext.Employees.Update(Employee);
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
