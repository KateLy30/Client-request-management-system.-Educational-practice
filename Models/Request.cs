

namespace customer_request_accounting_system.Models
{
    public class Request
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string? ClientName { get; set; }
        public string?  Status { get; set; }
        public string? Description { get; set; }
        public int AssignedEmployee { get; set; }
        public string? EmployeeName { get; set; }
        public DateTime? ResolutionDate { get; set; }
        public string? Priority { get; set; }
        public string? Category { get; set; }
        public DateTime CreateRequestDate { get; set; }
    }
}
