using Application.UseCases.GetVested.Domain;

namespace Application.UseCases.GetVested.Ports;

public record GetVestedOutput
(
    IEnumerable<VestedSchedule> VestedSchedules, 
    int Digits
);