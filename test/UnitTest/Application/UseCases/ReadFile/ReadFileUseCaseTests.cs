using Application.UseCases.ReadFile;
using Application.UseCases.ReadFile.Abstractions;
using Application.UseCases.ReadFile.Ports;
using AutoFixture;
using FluentAssertions;
using Moq.AutoMock;

namespace UnitTest.Application.UseCases.ReadFile;

public class ReadFileUseCaseTests
{
    private readonly IReadFileUseCase _sut;
    private readonly Fixture _fixture;

    public ReadFileUseCaseTests()
    {
        var mocker = new AutoMocker();
        _sut = mocker.CreateInstance<ReadFileUseCase>();
        _fixture = new Fixture();
    }

    [Fact]
    public async Task ShouldExecuteSuccessfully()
    {
        // Arrange
        var input = new ReadFileInput("test.csv", 1);

        // Act
        var result = await _sut.Execute(input);

        // Assert
        result.Should().NotBeNull();
        result.VestingEvents.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task ShouldExecuteThrowException()
    {
        // Arrange
        var invalidDigits = 999; 
        var input = new ReadFileInput("test.csv", invalidDigits);

        // Act
        var action = () => _sut.Execute(input);

        // Assert
        await action.Should().ThrowAsync<InvalidDataException>();
    }
}
