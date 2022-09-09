using Application.Commons.Domain;

namespace Application.UseCases.GetVested.Ports;

public record GetVestedInput
(
    string FileName,
    DateTime Date, 
    int Digits
);