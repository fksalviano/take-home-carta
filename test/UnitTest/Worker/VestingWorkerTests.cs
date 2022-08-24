using Application.UseCases.GetVested.Abstractions;
using Application.UseCases.ReadFile.Abstractions;
using Worker.Abstractions;
using Moq;
using Moq.AutoMock;
using Worker.Workers;
using Application.UseCases.ReadFile.Ports;
using AutoFixture;
using Application.UseCases.GetVested.Ports;
using Application.UseCases.GetVested.Domain;
using Application.Commons.Domain;

namespace UnitTest.Worker;

public class VestingWorkerTests
{
    private readonly VestingWorker _sut;
    private readonly Fixture _fixture = new Fixture();

    private readonly Mock<IReadFileUseCase> _readFileUseCase;
    private readonly Mock<IGetVestedUseCase> _getVestedUseCase;
    private readonly IGetVestedOutputPort _getVestedOutputPort;
    private readonly Mock<IWorkerOutputPort> _outputPort;

    public VestingWorkerTests()
    {
        var mocker = new AutoMocker();
        
        _outputPort = mocker.GetMock<IWorkerOutputPort>();
        _sut = mocker.CreateInstance<VestingWorker>();

        _readFileUseCase = mocker.GetMock<IReadFileUseCase>();
        _getVestedUseCase = mocker.GetMock<IGetVestedUseCase>();
        _getVestedOutputPort = _sut;
    }

    [Fact]
    public async Task ShouldExecuteSuccessfully()
    {
        // Arrange
        var args = new string[]{ "test.csv", "2020-01-01", "1" };

        var fileOutput = _fixture.Create<ReadFileOutput>();
        var getVestedOutput = _fixture.Create<GetVestedOutput>();

        _readFileUseCase.Setup(useCase => 
            useCase.ExecuteAsync(It.IsAny<ReadFileInput>(), CancellationToken.None))
                .ReturnsAsync(fileOutput);

        _getVestedUseCase.Setup(useCase => 
            useCase.ExecuteAsync(It.IsAny<GetVestedInput>(), CancellationToken.None))
                .Callback(() => _getVestedOutputPort.Ok(getVestedOutput));

        // Act
        await _sut.ExecuteAsync(args, CancellationToken.None);

        // Assert
        _outputPort.Verify(output => output.Ok(It.IsAny<IEnumerable<string>>()), Times.Once);

        _outputPort.Verify(output => output.NotFound(), Times.Never);
    }

    [Fact]
    public async Task ShouldExecuteAndReturnNotFound()
    {
        // Arrange
        var args = new string[]{ "test.csv", "2020-01-01", "1" };
        
        var fileOutput = new ReadFileOutput(_fixture.Build<VestingEvent>().CreateMany(0));

        _readFileUseCase.Setup(useCase => 
            useCase.ExecuteAsync(It.IsAny<ReadFileInput>(), CancellationToken.None))
                .ReturnsAsync(fileOutput);

        _getVestedUseCase.Setup(useCase => 
            useCase.ExecuteAsync(It.IsAny<GetVestedInput>(), CancellationToken.None))
                .Callback(() => _getVestedOutputPort.NotFound());

        // Act
        await _sut.ExecuteAsync(args, CancellationToken.None);

        // Assert
        _outputPort.Verify(output => output.NotFound(), Times.Once);

        _outputPort.Verify(output => output.Ok(It.IsAny<IEnumerable<string>>()), Times.Never);    
    }

    [Fact]
    public async Task ShouldExecuteAndReturnInvalid()
    {
        // Arrange
        var args = new string[]{ "not-found.csv", "2020-01-01", "1" };

        // Act
        await _sut.ExecuteAsync(args, CancellationToken.None);

        // Assert
        _outputPort.Verify(output => 
            output.Invalid(It.IsAny<string>()), Times.Once);

        _outputPort.Verify(output => 
            output.NotFound(), Times.Never);

        _outputPort.Verify(output => 
            output.Ok(It.IsAny<IEnumerable<string>>()), Times.Never);    
    }
}
