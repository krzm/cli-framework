using System;
using CLIHelper;
using ModelHelper;
using Moq;
using Xunit;

namespace CLIFramework.Tests;

public class EntityHelpCommandTests
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
				IAppCommand sut = new HelpCommand<ModelA>(
					null
					, null
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
				IAppCommand sut = new HelpCommand<ModelA>(
					new TextCommand(CommandName, nameof(ModelA))
					, null
					, null); 
			});
	}

	[Fact]
	public void DependencyC_Should_Throw_When_Null()
	{
		Assert.Throws<ArgumentNullException>(
			"this.props"
			, ()=> 
			{ 
				IAppCommand sut = new HelpCommand<ModelA>(
					new TextCommand(CommandName, nameof(ModelA))
					, new Mock<IOutput>().Object
					, null); 
				#pragma warning restore CS8625
			});
	}

    [Fact]
	public void Should_Invoke_Method()
    {
		var props = new string[]
			{
				nameof(IModelA.Name)
				, nameof(IModelA.Description)
			};
        var oMock = new Mock<IOutput>();
        IAppCommand sut = new HelpCommand<ModelA>(
            new TextCommand(
                CommandName
                , nameof(ModelA))
            , oMock.Object
			, props);

        sut.Execute(new object());

        oMock.Verify(m => m.Clear(), Times.Once());
		oMock.Verify(m => m.WriteLine(It.Is<string>((s) => s == $"Help {nameof(ModelA)}:")), Times.Once());
		oMock.Verify(m => m.WriteLine(It.Is<string>((s) => s == $"Properties: {string.Join(',', props)}")), Times.Once());
    }
}