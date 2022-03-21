using System;
using System.Collections.Generic;
using CLIHelper;
using Moq;
using Xunit;

namespace CLIFramework.Tests;

public class HelpCommandTests
{
    private const string CommandName = nameof(CommandName);

    [Fact]
	public void DependencyA_Should_Throw_When_Null()
	{
		Assert.Throws<ArgumentNullException>(
            "textCommand"
			, ()=> 
			{
				#pragma warning disable CS8625
				IAppCommand sut = new HelpCommand(
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
                IAppCommand sut = new HelpCommand(
					new TextCommand(CommandName)
					, null);
            });
	}

	[Fact]
	public void Param_Should_Throw_When_Null()
	{
		Assert.Throws<ArgumentNullException>(
			"commands"
			, ()=> 
			{ 
				new HelpCommand(
					new TextCommand(CommandName)
					, new Mock<IOutput>().Object)
				.SetCommands(null);
				#pragma warning restore CS8625
			});
	}

	[Fact]
	public void Param_Should_Throw_When_Empty()
	{
		Assert.Throws<ArgumentException>(
			"commands"
			, ()=> 
			{ 
				new HelpCommand(
					new TextCommand(CommandName)
					, new Mock<IOutput>().Object)
				.SetCommands(new List<TextCommand>());
			});
	}

    [Fact]
	public void Should_Invoke_Method()
	{
		var mock = new Mock<IOutput>();
		mock.Setup(m => m.WriteLine(It.IsAny<string>()));
		var sut = new HelpCommand(new TextCommand(CommandName), mock.Object); 
		sut.SetCommands(new List<TextCommand>(){ new TextCommand(CommandName) });

		sut.Execute(new object());

		mock.Verify(m => m.Clear(), Times.Once());
		mock.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Once());
	}
}