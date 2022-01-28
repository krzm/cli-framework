namespace CLIFramework;

public class ExitCommand : ConsoleCommand
{
	private readonly ISwitcher switcher;

	public ExitCommand(
		TextCommand textCommand
		, ISwitcher switcher)
		: base(textCommand)
	{
		ArgumentNullException.ThrowIfNull(switcher);

		this.switcher = switcher;
	}

	public override void Execute(object parameter)
	{
		switcher.Switch = false;
	}
}
