using Application.Commons.Domain;
using Application.UseCases.GetVested;
using Application.UseCases.GetVested.Abstractions;
using Application.UseCases.GetVested.Ports;
using AutoFixture;
using Moq;
using Moq.AutoMock;

namespace UnitTest.Application.UseCases.GetVested;

public class ReadFileUseCaseValidationTests
{
    private readonly IGetVestedUseCase _sut;
    private readonly Mock<IGetVestedUseCase> _useCase;
    private readonly Mock<IGetVestedOutputPort> _outputPort;
    private readonly Fixture _fixture;

    public ReadFileUseCaseValidationTests()
    {
        var mocker = new AutoMocker();
        _outputPort = mocker.GetMock<IGetVestedOutputPort>();
        _useCase = mocker.GetMock<IGetVestedUseCase>();

        _sut = mocker.CreateInstance<GetVestedUseCaseValidation>();
        _sut.SetOutputPort(_outputPort.Object);

        _fixture = new Fixture();
    }

    [Fact]
    public async Task ShouldValidateSuccessfully()
    {
        // Arrange
        var input = new GetVestedInput("test.csv", DateTime.MaxValue, 0);
        
        // Act
        await _sut.ExecuteAsync(input, CancellationToken.None);
        
        // Assert
        _useCase.Verify(useCase => 
            useCase.ExecuteAsync(input, CancellationToken.None), Times.Once);

        _outputPort.Verify(output => 
            output.Invalid(It.IsAny<Result>()), Times.Never);
    }    

    [Theory]
    [InlineData("", 1)]
    [InlineData("not-found.csv", 1)]
    [InlineData("test.csv", 99)]
    [InlineData("test.csv", -1)]
    public async Task ShouldValidateReturnInvalid(string fileName, int digits)
    {
        // Arrange        
        var input = new GetVestedInput(fileName, DateTime.MaxValue, digits);                
        
        // Act
        await _sut.ExecuteAsync(input, CancellationToken.None);
        
        // Assert
        _outputPort.Verify(output => 
            output.Invalid(It.IsAny<Result>()), Times.Once);

        _useCase.Verify(useCase => 
            useCase.ExecuteAsync(input, CancellationToken.None), Times.Never);
    }
}
