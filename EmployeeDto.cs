namespace Konkov_prakt7
{
    public class EmployeeDto
    {
        public int? Id { get; set; } = null;
        public string? FullName { get; set; } = null;
        public string? Position { get; set; } = null;
        public decimal? Salary { get; set; } = null;
        public DateOnly? HireDate { get; set; } = null;
        public bool? IsActive { get; set; } = null;
    }
}
