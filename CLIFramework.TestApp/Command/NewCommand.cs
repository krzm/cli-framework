using CLIHelper;

namespace CLIFramework.TestApp;

public class NewCommand : ConsoleCommand
{
    private readonly IOutput output;

	public NewCommand(
		TextCommand textCommand
		, IOutput consoleIO)
		: base(textCommand)
	{
		output = consoleIO;
	}

	public override void Execute(object? parameter)
	{
		output.WriteLine("New command");
	}
}