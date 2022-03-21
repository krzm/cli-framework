using System;
using System.Collections.Generic;
using CLIHelper;
using CLIReader;
using CRUDCommandHelper;
using DataToTable;
using ModelHelper;
using Moq;
using Serilog;
using Xunit;

namespace CLIFramework.Tests;

public class ModelAReadCommandTests
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
				IReadCommand<ModelA> sut = new ModelAReadCommand(
					null
					, null
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
				IReadCommand<ModelA> sut = new ModelAReadCommand(
					new Mock<IModelAUnitOfWork>().Object
					, null
					, null
					, null); 
			});
	}

	[Fact]
	public void DependencyC_Should_Throw_When_Null()
	{
		Assert.Throws<ArgumentNullException>(
			"this.Log"
			, ()=> 
			{ 
				IReadCommand<ModelA> sut = new ModelAReadCommand(
					new Mock<IModelAUnitOfWork>().Object
					, new Mock<IOutput>().Object
					, null
					, null); 
			});
	}

	[Fact]
	public void DependencyD_Should_Throw_When_Null()
	{
		Assert.Throws<ArgumentNullException>(
			"this.textProvider"
			, ()=> 
			{ 
				IReadCommand<ModelA> sut = new ModelAReadCommand(
					new Mock<IModelAUnitOfWork>().Object
					, new Mock<IOutput>().Object
					, new Mock<ILogger>().Object
					, null); 
			});
	}

	[Fact]
	public void Should_Invoke_Method()
	{
		var uowMock = new Mock<IModelAUnitOfWork>();
		var oMock = new Mock<IOutput>();
		var logMock = new Mock<ILogger>();
		var tpMock = new Mock<IDataToText<ModelA>>();
		var rcMock = new Mock<IDictionary<string, ReadConfig>>();
		IReadCommand<ModelA> sut = new ModelAReadCommand(
			uowMock.Object
			, oMock.Object
			, logMock.Object
			, tpMock.Object);

		sut.Read(default);
		#pragma warning restore CS8625

		oMock.Verify(m => m.Clear(), Times.Once());
		logMock.Verify(m => m.Information(
			It.Is<string>((s) => s == "{0} {1}")
			, It.Is<string>((s) => s == "Read")
			, It.Is<string>((s) => s == nameof(ModelA)))
			, Times.Once());
		uowMock.Verify(m => m.Get(), Times.Once());
		tpMock.Verify(m => m.GetText(It.IsAny<List<ModelA>>()), Times.Once());
		oMock.Verify(m => m.Write(It.IsAny<string>()), Times.Once());
	}
}