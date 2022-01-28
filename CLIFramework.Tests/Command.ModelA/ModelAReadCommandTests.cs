using System;
using System.Collections.Generic;
using CLIHelper;
using CRUDCommandHelper;
using DataToTable;
using ModelHelper;
using Moq;
using Xunit;

namespace CLIFramework.Tests;

public class ModelAReadCommandTests
{
	private const string CommandName = nameof(CommandName);

	[Fact]
	public void DependencyA_Should_Throw_When_Null()
	{
		Assert.Throws<ArgumentNullException>(
			"unitOfWork"
			, ()=> 
			{ 
				IReadCommand<ModelA> sut = new ModelAReadCommand(
					null
					, null
					, null); 
			});
	}

	[Fact]
	public void DependencyB_Should_Throw_When_Null()
	{
		Assert.Throws<ArgumentNullException>(
			"output"
			, ()=> 
			{ 
				IReadCommand<ModelA> sut = new ModelAReadCommand(
					new Mock<IModelAUnitOfWork>().Object
					, null
					, null); 
			});
	}

	[Fact]
	public void DependencyC_Should_Throw_When_Null()
	{
		Assert.Throws<ArgumentNullException>(
			"textProvider"
			, ()=> 
			{ 
				IReadCommand<ModelA> sut = new ModelAReadCommand(
					new Mock<IModelAUnitOfWork>().Object
					, new Mock<IOutput>().Object
					, null); 
			});
	}

	[Fact]
	public void Should_Invoke_Method()
	{
		var uowMock = new Mock<IModelAUnitOfWork>();
		var oMock = new Mock<IOutput>();
		var tpMock = new Mock<IDataToText<ModelA>>();
		var rcMock = new Mock<IDictionary<string, ReadConfig>>();
		IReadCommand<ModelA> sut = new ModelAReadCommand(
			uowMock.Object
			, oMock.Object
			, tpMock.Object);

		sut.Read(default);

		oMock.Verify(m => m.Clear(), Times.Once());
		oMock.Verify(m => m.WriteLine(It.Is<string>((s) => s==$"Read {nameof(ModelA)}:")), Times.Once());
		uowMock.Verify(m => m.Get(), Times.Once());
		tpMock.Verify(m => m.GetText(It.IsAny<List<ModelA>>()), Times.Once());
		oMock.Verify(m => m.Write(It.IsAny<string>()), Times.Once());
	}
}