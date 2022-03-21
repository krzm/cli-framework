using CLIHelper;

namespace CLIFramework;

public class HelpCommand 
	: ConsoleCommand
{
	private readonly IOutput output;
	private List<TextCommand>? commands;

	public HelpCommand(
		TextCommand textCommand
		, IOutput output)
		: base(textCommand)
	{
		this.output = output;
		ArgumentNullException.ThrowIfNull(this.output);
	}

	public void SetCommands(List<TextCommand> commands)
	{
		ValidateCommands(commands);

		this.commands = commands;
	}

	private static void ValidateCommands(List<TextCommand> commands)
	{
		ArgumentNullException.ThrowIfNull(commands);

		if (commands.Count == 0)
			throw new ArgumentException("Empty list", nameof(commands));
	}

	public override void Execute(object? parameter)
	{
		ArgumentNullException.ThrowIfNull(commands);
		ValidateCommands(commands);

		output.Clear();

		output.WriteLine(
			$"Commands: {string.Join(',', commands.Select(c => c.CommandText))}");
	}
}