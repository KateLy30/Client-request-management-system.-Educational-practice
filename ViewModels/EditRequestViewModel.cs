using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using customer_request_accounting_system.EntityFramework;
using customer_request_accounting_system.Models;

namespace customer_request_accounting_system.ViewModels
{
    public partial class EditRequestViewModel : ObservableObject
    {
        private readonly INavigation _navigation;

        [ObservableProperty]
        private Request request;
        public EditRequestViewModel(Request request, INavigation navigation)
        {
            Request = request;
            _navigation = navigation;
        }

        [RelayCommand]
        async Task ClickOkButton()
        {
            AssignValues(); //HACK
            using (var dbcontext = new AppDbContext())
            {
                var transaction = dbcontext.Database.BeginTransaction();
                try
                {
                    if (Request.Status == "Закрыта")
                        Request.ResolutionDate = DateTime.Now;

                    dbcontext.Requests.Update(Request);
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
            switch (Request.Status)
            {
                case "0":
                    Request.Status = "Новая";
                    break;
                case "1":
                    Request.Status = "В процессе";
                    break;
                case "2":
                    Request.Status = "Решена";
                    break;
                case "3":
                    Request.Status = "Закрыта";
                    break;
            }

            switch (Request.Priority)
            {
                case "0":
                    Request.Priority = "Низкий";
                    break;
                case "1":
                    Request.Priority = "Средний";
                    break;
                case "2":
                    Request.Priority = "Высокий";
                    break;
            }

            switch (Request.Category)
            {
                case "0":
                    Request.Category = "Технические неисправнеости";
                    break;
                case "1":
                    Request.Category = "Программные сбои";
                    break;
                case "2":
                    Request.Category = "Запросы информации";
                    break;
                case "3":
                    Request.Category = "Конфигурационные запросы";
                    break;
                case "4":
                    Request.Category = "Поддержка клиентов";
                    break;
                case "5":
                    Request.Category = "Обучение и консультации";
                    break;
                case "6":
                    Request.Category = "Финансово-юридические вопросы";
                    break;
                case "7":
                    Request.Category = "Дополнительные услуги";
                    break;
            }
        }
    }
}
