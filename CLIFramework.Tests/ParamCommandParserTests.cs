using Moq;
using System.Collections.Generic;
using Xunit;

namespace CLIFramework.Tests;

//todo:params are not part of equal
public class ParamCommandParserTests
{
    [Theory]
    [InlineData("help", "help", new string[] { })]
    [InlineData("help category", "help category", new string[] { })]
    [InlineData("log", "log", new string[] { })]
    [InlineData("log 9.11.2021", "log", new string[] { "9.11.2021" })]
    [InlineData("insert log", "insert log", new string[] { })]
    [InlineData("update log", "update log", new string[] { })]
    public void Test_parsing_result(
        string input
        , string command
        , string[] args)
    {
        var parser = new ParamCommandParser(GetCommands());
        var expected = GetMock(command, args).TextCommand;

        var actual = parser.Parse(input).TextCommand;

        Assert.Equal(expected, actual);
    }

    private static List<IAppCommand> GetCommands()
    {
        return new List<IAppCommand>
        {
            GetMock("help")
            , GetMock("clear")
            , GetMock("exit")
            , GetMock("category")
            , GetMock("place")
            , GetMock("task")
            , GetMock("log")
            , GetMock("help category")
            , GetMock("help place")
            , GetMock("help task")
            , GetMock("help log")
            , GetMock("insert category")
            , GetMock("insert place")
            , GetMock("insert task")
            , GetMock("insert log")
            , GetMock("update category")
            , GetMock("update place")
            , GetMock("update task")
            , GetMock("update log")
        };
    }

    private static IAppCommand GetMock(
        string command)
    {
        var mock = new Mock<IAppCommand>();
        mock.SetupGet(x => x.TextCommand).Returns(
            new TextCommand(command));
        return mock.Object;
    }

    private static IAppCommand GetMock(
        string command
        , string[] args)
    {
        var mock = new Mock<IAppCommand>();
        mock.SetupGet(x => x.TextCommand).Returns(
            new TextCommand(command, args));
        return mock.Object;
    }
}