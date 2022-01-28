using CLIHelper;

namespace CLIFramework;

public class HelpCommand<TEntity> : DataCommand<TEntity>
{
	private readonly IOutput output;
	private readonly string[] props;

	public HelpCommand(
		TextCommand textCommand
		, IOutput output
		, string[] props)
		: base(textCommand)
	{
		ArgumentNullException.ThrowIfNull(output);
		ArgumentNullException.ThrowIfNull(props);

		this.output = output;
		this.props = props;
	}

	public override void Execute(object parameter)
	{
		output.Clear();
		output.WriteLine($"Help {TextCommand.TypeName}:");
		output.WriteLine($"Properties: {string.Join(',', props)}");
	}
}