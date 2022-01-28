using System;
using Xunit;

namespace CLIFramework.Tests;

public class TextCommandTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("    ")]
    [InlineData("	")]
    public void DependencyA_Should_Throw_When(
        string commandName)
    {
        Assert.Throws<ArgumentNullException>(
            "commandText"
            , () => 
            { 
                var sut = new TextCommand(commandName); 
            });
    }

    [Theory]
    [InlineData(null)]
    public void DependencyB_Should_Throw_When(
        string typeName)
    {
        Assert.Throws<ArgumentNullException>(
            "typeName"
            , () => 
            { 
                var sut = new TextCommand("commandName", typeName);
            });
    }

    [Theory]
    [InlineData(null)]
    public void DependencyC_Should_Throw_When(
        string[] paramArray)
    {
        Assert.Throws<ArgumentNullException>(
            "paramArray"
            , () => 
            { 
                var sut = new TextCommand("commandName", "typeName", paramArray);
            });
    }

    [Theory]
    [InlineData("CommandName", "CommandName", true)]
    [InlineData("CommandName", "commandname", false)]
    public void When_commands_are_equal(
        string command1Name
        , string command2Name
        , bool result)
    {
        var command1 = new TextCommand(command1Name);
        var command2 = new TextCommand(command2Name);

        Assert.Equal(result, command1.Equals(command2));
    }

    [Fact]
    public void Other_type_is_not_equal_to_Command()
    {
        var command1 = new TextCommand("CommandName");

        Assert.False(command1.Equals(11));
    }

    [Theory]
    [InlineData("CommandName", "CommandName")]
    [InlineData("Commandname", "Commandname")]
    public void What_ToString_should_output(
        string commandName
        , string result)
    {
        var command = new TextCommand(commandName);

        Assert.Equal(result, command.ToString());
    }
}