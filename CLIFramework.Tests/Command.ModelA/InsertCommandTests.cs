using System;
using CLIReader;
using CLIWizardHelper;
using ModelHelper;
using Moq;
using Serilog;
using Xunit;

namespace CLIFramework.Tests;

public class InsertCommandTests
{
	private const string CommandName = nameof(CommandName);

	[Fact]
	public void DependencyA_Should_Throw_When_Null()
	{
		Assert.Throws<ArgumentNullException>(
			"UnitOfWork"
			, ()=> 
			{
				#pragma warning disable CS8625
				IInsertWizard<ModelA> sut = 
					new ModelAInsertCommand(
						null
						, null
						, null); 
			});
	}

	[Fact]
	public void DependencyB_Should_Throw_When_Null()
	{
		Assert.Throws<ArgumentNullException>(
			"RequiredTextReader"
			, ()=> 
			{ 
				IInsertWizard<ModelA> sut = 
					new ModelAInsertCommand(
						new Mock<IModelAUnitOfWork>().Object
						, null
						, null); 
			});
	}

	[Fact]
	public void DependencyC_Should_Throw_When_Null()
	{
		Assert.Throws<ArgumentNullException>(
			"this.log"
			, ()=> 
			{ 
				IInsertWizard<ModelA> sut = 
					new ModelAInsertCommand(
						new Mock<IModelAUnitOfWork>().Object
						, new Mock<IReader<string>>().Object
						, null);
				#pragma warning restore CS8625
			});
	}

	[Fact]
	public void Should_Invoke_Method()
	{
		var uowMock =  new Mock<IModelAUnitOfWork>();
		var crMock = new Mock<ICommandRunner>();
		var logMock = new Mock<ILogger>();
		IInsertWizard<ModelA> sut = 
			new ModelAInsertCommand(
				uowMock.Object
				, new Mock<IReader<string>>().Object
				, logMock.Object);

		sut.Insert();

		uowMock.Verify(m => m.Insert(It.IsAny<ModelA>()), Times.Once());
		uowMock.Verify(m => m.Save(), Times.Once());
	}
}