using System.Reflection;
using Application.Commons.Domain;
using Application.UseCases.ReadFile;
using Application.UseCases.ReadFile.Abstractions;
using Application.UseCases.ReadFile.Ports;
using AutoFixture;
using FluentAssertions;
using Moq;
using Moq.AutoMock;

namespace UnitTest.Application.UseCases.ReadFile;

public class ReadFileUseCaseValidationTests
{
    private readonly IReadFileUseCase _sut;
    private readonly Mock<IReadFileUseCase> _useCase;
    private readonly Fixture _fixture;

    public ReadFileUseCaseValidationTests()
    {
        var mocker = new AutoMocker();
        _useCase = mocker.GetMock<IReadFileUseCase>();

        _sut = mocker.CreateInstance<ReadFileUseCaseValidation>();

        _fixture = new Fixture();
    }

    [Fact]
    public async Task ShouldValidateSuccessfully()
    {
        // Arrange
        var events = _fixture.Build<VestingEvent>().CreateMany(1);
        var input = new ReadFileInput("test.csv", 1);
        var output = new ReadFileOutput(_fixture.Build<VestingEvent>().CreateMany(1));

        _useCase.Setup(useCase => useCase.ExecuteAsync(input, CancellationToken.None))
            .ReturnsAsync(output);
        
        // Act
        var result = await _sut.ExecuteAsync(input, CancellationToken.None);
        
        // Assert
        result.IsValid.Should().BeTrue();

        _useCase.Verify(useCase => 
            useCase.ExecuteAsync(input, CancellationToken.None), Times.Once);
    }

    [Theory]
    [InlineData("", 1)]
    [InlineData("not-found.csv", 1)]
    [InlineData("test.csv", 7)]
    [InlineData("test.csv", -1)]
    public async Task ShouldValidateReturnInvalid(string fileName, int digits)
    {
        // Arrange
        var events = _fixture.Build<VestingEvent>().CreateMany(1);
        var input = new ReadFileInput(fileName, digits);
        var output = new ReadFileOutput(_fixture.Build<VestingEvent>().CreateMany(1));

        _useCase.Setup(useCase => useCase.ExecuteAsync(input, CancellationToken.None))
            .ReturnsAsync(output);        
        
        // Act
        var result = await _sut.ExecuteAsync(input, CancellationToken.None);
        
        // Assert
        result.IsValid.Should().BeFalse();

        _useCase.Verify(useCase => 
            useCase.ExecuteAsync(input, CancellationToken.None), Times.Never);
    }
}
