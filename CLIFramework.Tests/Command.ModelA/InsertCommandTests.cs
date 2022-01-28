using System;
using CLIHelper;
using CLIReader;
using CLIWizardHelper;
using ModelHelper;
using Moq;
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
			"this.output"
			, ()=> 
			{ 
				IInsertWizard<ModelA> sut = 
					new ModelAInsertCommand(
						new Mock<IModelAUnitOfWork>().Object
						, new Mock<IReader<string>>().Object
						, null); 
			});
	}

	[Fact]
	public void Should_Invoke_Method()
	{
		var uowMock =  new Mock<IModelAUnitOfWork>();
		var crMock = new Mock<ICommandRunner>();
		var oMock = new Mock<IOutput>();
		IInsertWizard<ModelA> sut = 
			new ModelAInsertCommand(
				uowMock.Object
				, new Mock<IReader<string>>().Object
				, oMock.Object);

		sut.Insert();

		uowMock.Verify(m => m.Insert(It.IsAny<ModelA>()), Times.Once());
		uowMock.Verify(m => m.Save(), Times.Once());
	}
}