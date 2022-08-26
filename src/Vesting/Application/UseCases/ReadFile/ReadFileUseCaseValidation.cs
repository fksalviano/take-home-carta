using Application.Commons.Extensions;
using Application.UseCases.ReadFile.Abstractions;
using Application.UseCases.ReadFile.Ports;
using FluentValidation;

namespace Application.UseCases.ReadFile;

public class ReadFileUseCaseValidation : AbstractValidator<ReadFileInput>, IReadFileUseCase
{
    private readonly IReadFileUseCase _useCase;

    public ReadFileUseCaseValidation(IReadFileUseCase useCase)
    {
        _useCase = useCase;

        RuleFor(input => input.FilePath)
            .NotNull().NotEmpty().WithMessage("File name is null or empty");

        RuleFor(input => input)
            .Must(FileExists).WithMessage("File not exists");

        RuleFor(input => input.Digits)
            .InclusiveBetween(0, 6)
            .WithMessage("Digits should be between 0 and 6");     
    }
    
    private bool FileExists(ReadFileInput input) => 
        File.Exists(input.FilePath);

    public async Task<ReadFileOutput> ExecuteAsync(ReadFileInput input, CancellationToken cancellationToken)
    {
        var result = Validate(input);

        if (!result.IsValid)
            return new ReadFileOutput(result.ToDomainResult());

        return await _useCase.ExecuteAsync(input, cancellationToken);
    }
}
