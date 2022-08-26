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
        var events = _fixture.Build<VestingEvent>().CreateMany(1);
        var input = new GetVestedInput(events, events.First().Date, 1);
        
        // Act
        await _sut.ExecuteAsync(input, CancellationToken.None);
        
        // Assert
        _outputPort.Verify(output => 
            output.Invalid(It.IsAny<ValidationResult>()), Times.Never);

        _useCase.Verify(useCase => 
            useCase.ExecuteAsync(input, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task ShouldValidateReturnInvalid()
    {
        // Arrange
        var events = _fixture.Build<VestingEvent>().CreateMany(1);
        var input = new GetVestedInput(events, events.First().Date, 99);
        
        // Act
        await _sut.ExecuteAsync(input, CancellationToken.None);
        
        // Assert
        _useCase.Verify(useCase => 
            useCase.ExecuteAsync(input, CancellationToken.None), Times.Never);

        _outputPort.Verify(output => 
            output.Invalid(It.IsAny<ValidationResult>()), Times.Once);
    }
}
