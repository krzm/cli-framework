using System;
using CLIHelper;
using CLIReader;
using CLIWizardHelper;
using ModelHelper;
using Moq;
using Xunit;

namespace CLIFramework.Tests;

public class ModelAUpdateCommandTests
{
	private const string CommandName = nameof(CommandName);

	[Fact]
	public void DependencyA_Should_Throw_When_Null()
	{
		Assert.Throws<ArgumentNullException>(
			"UnitOfWork"
			, ()=> 
			{ 
				IUpdateWizard<ModelA> sut = new ModelAUpdateCommand(
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
				IUpdateWizard<ModelA> sut = new ModelAUpdateCommand(
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
				IUpdateWizard<ModelA> sut = new ModelAUpdateCommand(
					new Mock<IModelAUnitOfWork>().Object
					, new Mock<IReader<string>>().Object
					, null); 
			});
	}

	[Fact]
	public void Should_Invoke_Method()
	{
		var rMock = new Mock<IReader<string>>();
		var uowMock = new Mock<IModelAUnitOfWork>();
		var crMock = new Mock<ICommandRunner>();
		var oMock = new Mock<IOutput>();
		rMock.Setup(m => m.Read(GetIdReadConfig()))
			.Returns("99");
		uowMock.Setup(m => m.GetById(It.Is<int>((i) => i == 99)))
			.Returns(new ModelA());
		rMock.Setup(m => m.Read(GetNrReadConfig()))
			.Returns("1");
		IUpdateWizard<ModelA> sut = new ModelAUpdateCommand(
			uowMock.Object
			, rMock.Object
			, oMock.Object);

		sut.Update();

		rMock.Verify(m => m.Read(GetIdReadConfig()), Times.Once());
		uowMock.Verify(m => m.GetById(It.IsAny<int>()), Times.Once());
		rMock.Verify(m => m.Read(GetNrReadConfig()), Times.Once);
		uowMock.Verify(m => m.Save(), Times.Once());
	}

	private static ReadConfig GetNrReadConfig() => It.Is<ReadConfig>(
		(rc) => rc.Max == 1
		&& rc.Message == $"Select property number. 1-{nameof(IModelA.Name)}, 2-{nameof(IModelA.Description)}");

	private static ReadConfig GetIdReadConfig() => It.Is<ReadConfig>(
		(rc) => rc.Max == 6
		&& rc.Message == $"Select {nameof(ModelA)} Id");
}