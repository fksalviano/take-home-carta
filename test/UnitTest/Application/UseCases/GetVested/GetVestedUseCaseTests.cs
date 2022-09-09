using Application.Commons.Domain;
using Application.UseCases.GetVested;
using Application.UseCases.GetVested.Abstractions;
using Application.UseCases.GetVested.Ports;
using AutoFixture;
using FluentAssertions;
using Moq;
using Moq.AutoMock;

namespace UnitTest.Application.UseCases.GetVested;

public class GetVestedUseCaseTests
{
    private readonly IGetVestedUseCase _sut;
    private readonly Mock<IGetVestedOutputPort> _outputPort;
    private readonly Fixture _fixture;

    public GetVestedUseCaseTests()
    {
        var mocker = new AutoMocker();
        _outputPort = mocker.GetMock<IGetVestedOutputPort>();

        _sut = mocker.CreateInstance<GetVestedUseCase>();
        _sut.SetOutputPort(_outputPort.Object);

        _fixture = new Fixture();
    }

    [Fact]
    public async Task ShouldExecuteAndOutputOK()
    {
        // Arrange
        var input = new GetVestedInput("test.csv", DateTime.MaxValue, 0);

        // Act
        await _sut.ExecuteAsync(input, CancellationToken.None);

        // Assert 
        _outputPort.Verify(output => 
            output.Ok(It.IsAny<GetVestedOutput>()), Times.Once);

        _outputPort.Verify(output => output.NotFound(), Times.Never);
    }

    [Fact]
    public async Task ShouldExecuteAndOutputNotFound()
    {
        // Arrange
        var input = new GetVestedInput("test-empty.csv", DateTime.MaxValue, 0);

        // Act
        await _sut.ExecuteAsync(input, CancellationToken.None);

        // Assert 
        _outputPort.Verify(output => output.NotFound(), Times.Once);

        _outputPort.Verify(output => 
            output.Ok(It.IsAny<GetVestedOutput>()), Times.Never);
    }

    [Fact]
    public async Task ShouldExecuteAndOutputInvalid()
    {
        // Arrange
        var input = new GetVestedInput("test-invalid.csv", DateTime.MaxValue, 0);

        // Act        
        await _sut.ExecuteAsync(input, CancellationToken.None);
        
        // Assert
        _outputPort.Verify(output => 
            output.Invalid(It.IsAny<ValidationResult>()), Times.Once);        
    }
}