

using customer_request_accounting_system.ViewModels;

namespace customer_request_accounting_system.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Post {  get; set; }
        public DateTime? DateOfEmployment { get; set; }
    }
}
