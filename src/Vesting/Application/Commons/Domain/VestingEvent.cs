namespace Application.Commons.Domain;

public class VestingEvent
{
    public VestingType Type { get; set; }
    public string? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public string? AwardId { get; set; }
    public DateTime Date { get; set; }
    public decimal Quantity { get; set; }
}