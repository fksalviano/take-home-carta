using Application.Commons.Domain;
using Application.UseCases.GetVested.Ports;
using Application.UseCases.ReadFile.Ports;

namespace Application.Commons.Extensions;

public static class InputExtensions
{
    public static ReadFileInput ToReadFileInput(this InputArguments input) =>
        new ReadFileInput(
            input.GetFilePath(), 
            input.Digits);

    public static GetVestedInput ToGetVestedInput(this InputArguments input, ReadFileOutput fileOutput) =>
        new GetVestedInput(
            fileOutput, 
            input.TargetDate, 
            input.Digits);

    public static string GetFilePath(this InputArguments input) =>
        Path.Combine(Directory.GetCurrentDirectory(), input.FileName);
}
