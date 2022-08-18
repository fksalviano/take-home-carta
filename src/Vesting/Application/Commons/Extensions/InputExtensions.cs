using Application.Commons.Domain;
using Application.UseCases.GetVested.Ports;
using Application.UseCases.ReadFile.Ports;

namespace Application.Commons.Extensions;

public static class InputExtensions
{
    public static ReadFileInput ToReadFileInput(this Input input) =>
        new ReadFileInput(
            input.GetFilePath(), 
            input.Digits);

    public static GetVestedInput ToGetVestedInput(this Input input, ReadFileOutput fileOutput) =>
        new GetVestedInput(
            fileOutput.VestingEvents, 
            input.TargetDate, 
            input.Digits);

    public static string GetFilePath(this Input input) =>
        Path.Combine(Directory.GetCurrentDirectory(), input.FileName);
}
