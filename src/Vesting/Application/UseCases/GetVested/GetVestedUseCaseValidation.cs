using Application.Commons.Extensions;
using Application.UseCases.GetVested.Abstractions;
using Application.UseCases.GetVested.Ports;
using FluentValidation;

namespace Application.UseCases.GetVested;

public class GetVestedUseCaseValidation : AbstractValidator<GetVestedInput>, IGetVestedUseCase
{
    private readonly IGetVestedUseCase _useCase;
    private IGetVestedOutputPort? _outputPort;

    public GetVestedUseCaseValidation(IGetVestedUseCase useCase)
    {
        _useCase = useCase;

        RuleFor(input => input.Digits)
            .InclusiveBetween(0, 6)
            .WithMessage("Digits should be between 0 and 6");
    }

    public void SetOutputPort(IGetVestedOutputPort outputPort)
    {
        _outputPort = outputPort;
        _useCase.SetOutputPort(outputPort);
    }

    public async Task ExecuteAsync(GetVestedInput input, CancellationToken cancellationToken)
    {
        var result = await ValidateAsync(input, cancellationToken);
        
        if (!result.IsValid)
        {
            _outputPort!.Invalid(result.ToDomainResult());
            return;
        }

        await _useCase.ExecuteAsync(input, cancellationToken);
    }
}