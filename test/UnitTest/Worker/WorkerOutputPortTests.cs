using AutoFixture;
using Moq.AutoMock;
using Worker.Abstractions;
using Worker.Ports;

namespace UnitTest.Worker;

public class WorkerOutputPortTests
{
    private readonly IWorkerOutputPort _sut;
    private readonly Fixture _fixture;

    public WorkerOutputPortTests()
    {
        var mocker = new AutoMocker();
        _sut = mocker.CreateInstance<WorkerOutputPort>();

        _fixture = new Fixture();
    }

    [Fact]
    public void ShouldOutputOk()
    {
        // Arrange
        var output = _fixture.Build<string>().CreateMany(1);

        // Act
        _sut.Ok(output);
    }

    [Fact]
    public void ShouldOutputInvalid()
    {
        // Arrange
        var error = _fixture.Create<string>();

        // Act
        _sut.Invalid(error);
    }

    [Fact]
    public void ShouldOutputNotFound()
    {
        // Act
        _sut.NotFound();
    }
}
