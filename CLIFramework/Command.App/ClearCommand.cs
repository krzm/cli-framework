using CLIHelper;

namespace CLIFramework;

public class ClearCommand : ConsoleCommand
{
	private readonly IOutput output;

	public ClearCommand(
		TextCommand textCommand
		, IOutput output) : base(textCommand)
	{
		this.output = output;

		ArgumentNullException.ThrowIfNull(this.output);
	}

	public override void Execute(object? parameter)
	{
		output.Clear();
	}
}