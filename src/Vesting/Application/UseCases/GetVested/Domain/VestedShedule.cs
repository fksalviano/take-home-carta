namespace Application.UseCases.GetVested.Domain;

public class VestedSchedule
{
    public string? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public string? AwardId { get; set; }
    public decimal Quantity { get; set; }
}
