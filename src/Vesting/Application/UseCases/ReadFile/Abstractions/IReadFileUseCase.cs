using Application.UseCases.ReadFile.Ports;

namespace Application.UseCases.ReadFile.Abstractions;

public interface IReadFileUseCase
{
    Task<ReadFileOutput> Execute(ReadFileInput input);    
}