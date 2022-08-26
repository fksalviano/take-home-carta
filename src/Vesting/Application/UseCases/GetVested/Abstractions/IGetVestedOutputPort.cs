using Application.Commons.Domain;
using Application.UseCases.GetVested.Ports;

namespace Application.UseCases.GetVested.Abstractions;

public interface IGetVestedOutputPort
{
    void Ok(GetVestedOutput output);
    void Invalid(ValidationResult result);
    void NotFound();
}

