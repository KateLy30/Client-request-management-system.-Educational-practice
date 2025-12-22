using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using customer_request_accounting_system.EntityFramework;
using customer_request_accounting_system.Models;

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
        public int? idClient;
        [ObservableProperty]
        public string? nameClient;
        [ObservableProperty]
        public int idEmployee;
        [ObservableProperty]
        public string? nameEmployee;
        [ObservableProperty]
        public string? status;
        [ObservableProperty]
        public string? description;
        [ObservableProperty]
        public string? priorityRequest;
        [ObservableProperty]
        public string? categoryRequest;

        [RelayCommand]
        async Task ClickOkButton()
        {
            AssignValues(); //HACK 
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
                        Status = Status,
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
        private void AssignValues() //HACK
        {
            switch (Status)
            {
                case "0":
                    Status = "Новая";
                    break;
                case "1":
                    Status = "В процессе";
                    break;
                case "2":
                    Status = "Решена";
                    break;
                case "3":
                    Status = "Закрыта";
                    break;
            }

            switch (PriorityRequest)
            {
                case "0":
                    PriorityRequest = "Низкий";
                    break;
                case "1":
                    PriorityRequest = "Средний";
                    break;
                case "2":
                    PriorityRequest = "Высокий";
                    break;
            }

            switch (CategoryRequest)
            {
                case "0":
                    CategoryRequest = "Технические неисправнеости";
                    break;
                case "1":
                    CategoryRequest = "Программные сбои";
                    break;
                case "2":
                    CategoryRequest = "Запросы информации";
                    break;
                case "3":
                    CategoryRequest = "Конфигурационные запросы";
                    break;
                case "4":
                    CategoryRequest = "Поддержка клиентов";
                    break;
                case "5":
                    CategoryRequest = "Обучение и консультации";
                    break;
                case "6":
                    CategoryRequest = "Финансово-юридические вопросы";
                    break;
                case "7":
                    CategoryRequest = "Дополнительные услуги";
                    break;
            }
        }

    }
}
