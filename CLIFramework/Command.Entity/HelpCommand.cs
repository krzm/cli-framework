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
		this.output = output;
		this.props = props;
		ArgumentNullException.ThrowIfNull(this.output);
		ArgumentNullException.ThrowIfNull(this.props);
	}

	public override void Execute(object? parameter)
	{
		output.Clear();
		output.WriteLine($"Help {TextCommand.TypeName}:");
		output.WriteLine($"Properties: {string.Join(',', props)}");
	}
}