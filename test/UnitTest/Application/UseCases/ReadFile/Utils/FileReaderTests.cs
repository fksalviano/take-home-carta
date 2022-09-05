using Application.UseCases.ReadFile.Utils;
using Moq;
using Moq.AutoMock;

namespace UnitTest.Application.UseCases.ReadFile.Utils;

public class FileReaderTests
{
    private readonly Mock<Func<string[], object>> _parseValuesFunc;
    private readonly Mock<Action<int, Exception>> _exceptionHandler;

    public FileReaderTests()
    {
        var mocker = new AutoMocker();

        _parseValuesFunc = new Mock<Func<string[], object>>();
        _exceptionHandler = new Mock<Action<int, Exception>>();
    }

    [Fact]
    public void ShouldReadAllLinesSuccessfully()
    {
        // Arrange
        _parseValuesFunc.Setup(func => func.Invoke(It.IsAny<string[]>()))
            .Returns(new Object());

        // Act
        var result = FileReader.GetAsEnumerable("test.csv", 
            _parseValuesFunc.Object, _exceptionHandler.Object).ToList();

        // Assert
        _parseValuesFunc.Verify(func => func.Invoke(It.IsAny<string[]>()), 
            Times.AtLeastOnce);

        _exceptionHandler.Verify(action => action.Invoke(It.IsAny<int>(), It.IsAny<Exception>()), 
            Times.Never);
    }

    [Fact]
    public void ShouldReadAllLinesHandleException()
    {
        // Arrange
        _parseValuesFunc.Setup(func => func.Invoke(It.IsAny<string[]>()))
            .Throws<Exception>();

        // Act
        var result = FileReader.GetAsEnumerable("test.csv",
            _parseValuesFunc.Object, _exceptionHandler.Object).ToList();

        // Assert
        _exceptionHandler.Verify(action => action.Invoke(It.IsAny<int>(), It.IsAny<Exception>()), 
            Times.Once);
    }
    
}