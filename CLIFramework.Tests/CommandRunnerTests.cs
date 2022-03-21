using System;
using CLIHelper;
using Moq;
using Xunit;

namespace CLIFramework.Tests;

public class CommandRunnerTests
{
    [Fact]
	public void DependencyA_Should_Throw_When_Null()
	{
		Assert.Throws<ArgumentNullException>(
            "this.commandParser"
			, ()=> 
			{
				#pragma warning disable CS8625
				ICommandRunner sut = new CommandRunner(
					null
					, null); 
			});
	}

    [Fact]
	public void DependencyB_Should_Throw_When_Null()
	{
		Assert.Throws<ArgumentNullException>(
            "this.output"
			, ()=> 
			{ 
				ICommandRunner sut = new CommandRunner(
					new Mock<ICommandParser>().Object
					, null);
				#pragma warning restore CS8625
			});
	}

    [Fact]
	public void Should_Invoke_Command()
	{
		var parserMock =  new Mock<ICommandParser>();
		var cmdMock = new Mock<IAppCommand>();
		parserMock.Setup(m => m.Parse(
			It.Is<string>(s => s == "test")))
			.Returns(cmdMock.Object);
		cmdMock.Setup(m => m.CanExecute(It.IsAny<object>())).Returns(true);
		cmdMock.Setup(m => m.Execute(It.IsAny<object>()));
		cmdMock.SetupProperty(m => m.TextCommand)
			.SetupGet((c) => c.TextCommand)
			.Returns(new TextCommand("test"));
		ICommandRunner sut = new CommandRunner(
			parserMock.Object
			, new Mock<IOutput>().Object);

		sut.RunCommand("test");

		parserMock.Verify(m => m.Parse(It.IsAny<string>()), Times.Once());
		cmdMock.Verify(m => m.CanExecute(It.IsAny<object>()), Times.Once());
		cmdMock.Verify(m => m.Execute(It.IsAny<object>()), Times.Once());
	}
}