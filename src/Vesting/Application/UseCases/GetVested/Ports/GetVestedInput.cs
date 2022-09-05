using Application.Commons.Domain;

namespace Application.UseCases.GetVested.Ports;

public record GetVestedInput
(
    IEnumerable<VestingEvent> VestingEvents, 
    DateTime Date, 
    int Digits
);