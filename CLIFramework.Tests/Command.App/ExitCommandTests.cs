using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace CLIFramework.Tests;

public class ExitCommandTests
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
				IAppCommand sut = new ExitCommand(
					null
					, null);
			});
	}

	[Fact]
	public void DependencyB_Should_Throw_When_Null()
	{
		Assert.Throws<ArgumentNullException>(
			"this.switcher"
			, ()=> 
			{
				IAppCommand sut = new ExitCommand(
					new TextCommand(CommandName)
					, null);
				#pragma warning restore CS8625
			});
	}

	[Fact]
	public void Should_Invoke_Method()
	{
		var mock = new Mock<ISwitcher>();
		mock.SetupProperty(m => m.Switch, true);
		IAppCommand sut = new ExitCommand(
			new TextCommand(CommandName), mock.Object); 
		
		sut.Execute(new object());

		mock.Object.Switch.Should().Be(false);
	}
}