using Application.Commons.Domain;
using Application.UseCases.GetVested.Ports;
using AutoFixture;
using FluentAssertions;
using Moq.AutoMock;
using Worker;

namespace UnitTest.Worker;

public class WorkerOutputPortTests
{
    private readonly OutputPort _sut;
    private readonly Fixture _fixture;

    public WorkerOutputPortTests()
    {
        var mocker = new AutoMocker();
        _sut = mocker.CreateInstance<OutputPort>();

        _fixture = new Fixture();
    }

    [Fact]
    public void ShouldOutputOk()
    {
        // Arrange
        var output = _fixture.Create<GetVestedOutput>();

        // Act
        _sut.Ok(output);
    }

    [Fact]
    public void ShouldOutputInvalid()
    {
        // Arrange
        var error = _fixture.Create<ValidationResult>();

        // Act
        _sut.Invalid(error);
    }

    [Fact]
    public void ShouldOutputNotFound()
    {
        // Act
        _sut.NotFound();
    }

    [Fact]
    public void ShouldCreateSuccessfully()
    {
        // Act
        var result = OutputPort.Create();

        // Assert
        result.Should().NotBeNull(); 
    }
}
