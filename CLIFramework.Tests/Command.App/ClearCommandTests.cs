using System;
using CLIHelper;
using Moq;
using Xunit;

namespace CLIFramework.Tests;

public class ClearCommandTests
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
				IAppCommand sut = new ClearCommand(null, null);
			});
	}

    [Fact]
    public void DependencyB_Should_Throw_When_Null()
	{
		Assert.Throws<ArgumentNullException>(
            "this.output"
            , ()=> 
            {
                IAppCommand sut = new ClearCommand(new TextCommand(CommandName), null);
				#pragma warning restore CS8625
            });
	}

    [Fact]
	public void Should_Invoke_Method()
	{
		var mock = new Mock<IOutput>();
		mock.Setup(m => m.Clear());
		IAppCommand sut = new ClearCommand(new TextCommand(CommandName), mock.Object); 
			
		sut.Execute(new object());

		mock.Verify(m => m.Clear(), Times.Once());
	}
}