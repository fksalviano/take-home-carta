using Application.Commons.Domain;
using Application.UseCases.GetVested;
using Application.UseCases.GetVested.Abstractions;
using Application.UseCases.GetVested.Ports;
using Application.UseCases.GetVestedByAward.Abstractions;
using AutoFixture;
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
        var vestingEvents = _fixture.Build<VestingEvent>().CreateMany(1);
        var date = vestingEvents.First().Date;

        var input = new GetVestedInput(vestingEvents, date, 0);

        // Act
        await _sut.Execute(input);

        // Assert 
        _outputPort.Verify(output => 
            output.Ok(It.IsAny<GetVestedOutput>()), Times.Once);

        _outputPort.Verify(output => output.NotFound(), Times.Never);
    }

    [Fact]
    public async Task ShouldExecuteAndOutputNotFound()
    {
        // Arrange
        var vestingEvents = _fixture.Build<VestingEvent>().CreateMany(0);

        var input = new GetVestedInput(vestingEvents, DateTime.MinValue, 0);

        // Act
        await _sut.Execute(input);

        // Assert 
        _outputPort.Verify(output => output.NotFound(), Times.Once);

        _outputPort.Verify(output => 
            output.Ok(It.IsAny<GetVestedOutput>()), Times.Never);
    }
}
