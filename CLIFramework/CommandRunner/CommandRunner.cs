using CLIHelper;

namespace CLIFramework;

public class CommandRunner : ICommandRunner
{
    private readonly ICommandParser commandParser;
    private readonly IOutput output;

    public CommandRunner(
        ICommandParser commandParser
        , IOutput output)
    {
        this.commandParser = commandParser;
        this.output = output;

        ArgumentNullException.ThrowIfNull(this.commandParser);
        ArgumentNullException.ThrowIfNull(this.output);
    }

    public void RunCommand(string input)
    {
        try
        {
            var command = commandParser.Parse(input);
            if (command.CanExecute(command.TextCommand.Params) == false) 
                throw new Exception("CanExecute is false");
            command.Execute(command.TextCommand.Params);
        }
        catch (ArgumentException ex)
        {
            output.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            output.WriteLine(ex.Message);
        }
    }
}